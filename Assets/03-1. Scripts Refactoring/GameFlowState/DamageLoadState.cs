using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    [SerializeField] private float _gameFlowDelay;
    private Stack<BlockBase_Refact> _destroyStack;
    private WaitUntil _waitDocked;
    private WaitForSeconds _delayWait;
    private int _itemCount;

    public int ItemCount 
    { 
        get { return _itemCount; }
        set 
        {
            if (Mathf.Abs(value - _itemCount) > 1)
                return;
            if (value < 0)
                return;

            _itemCount = value;
        }
    }

    private void Awake()
    {
        _waitDocked = new WaitUntil(() => GameFlowMgr_Refact._instance.DockedCount == 63);
        _delayWait = new WaitForSeconds(_gameFlowDelay);
    }

    public void SetDestroyQueue(Stack<BlockBase_Refact> stack)
    {
        _destroyStack = stack;
    }

    public override IEnumerator Handle()
    {
        // ���ÿ� ����� 3�� �̸��� ���, �ƹ��ϵ� �Ͼ������
        if (_destroyStack.Count < 3)
        {
            while (_destroyStack.Count != 0)
            {
                _destroyStack.Pop().ActivateBlockSeletionUI(false);
                yield return null;
            }

            yield break;
        }

        // 3�� �̻��� ���
        Stack<ItemBlock_Refact> itemBlockStack = new Stack<ItemBlock_Refact>();
        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.DamageLoad;
        int comboCount = 0;
        while (_destroyStack.Count != 0)
        {
            bool isContainItemBlock = false;
            int stackCount = _destroyStack.Count;

            // ���ÿ� ���� ��ϵ�(�븻 ���, ������ ���, ��Ÿ ���) �ı��۾�
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();
                ItemBlock_Refact itemBlock = null;

                // ������ ����� �ִ°�� ��ȭ��Ų ��, ���� ���ÿ� �߰�
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlock = block.GetComponent<ItemBlock_Refact>();
                if (itemBlock != null && !itemBlock.IsIgnited)
                {
                    itemBlockStack.Push(itemBlock);
                    isContainItemBlock = true;
                }

                block.DoBreakAction();
            }

            // ��ϵ� ���ڸ� ã�������� ���
            yield return _waitDocked;
            yield return _delayWait;

            // ���üӿ� �ִ� ������ ���� ó��, ��ȭ�� ������ ����� Ȱ��ȭ
            _itemCount = itemBlockStack.Count;
            while (itemBlockStack.Count != 0)
            {
                ItemBlock_Refact popBlock = itemBlockStack.Pop();
                switch (popBlock.DestroyType)
                {
                    case ItemDestroyType.Destory:
                        IBlockDestroyerItem destroyerBlock = popBlock.GetComponent<IBlockDestroyerItem>();

                        // ���⼭ yield return�� ���� �ѹ��� ���� �����۵��� �ϳ��ϳ� ����
                        //yield return StartCoroutine(destroyerBlock.FillDestroyStack(_destroyStack));

                        // �� �ּ��� �����ϸ� �ѹ��� ���� �����۵��� �Ѳ����� ����
                        StartCoroutine(destroyerBlock.FillDestroyStack(_destroyStack, this));
                        break;

                    case ItemDestroyType.None:
                        IBlockNoneDestroyerItem noneDestroyerBlock = popBlock.GetComponent<IBlockNoneDestroyerItem>();
                        break;
                }

                yield return null;
            }

            if (isContainItemBlock)
            {
                // ���⼭ ������ ��ϵ� ��� ȿ���� ���������� ��ٷ�����Ѵ�.
                yield return new WaitUntil(() => _itemCount == 0);
                comboCount++;
            }
        }

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}