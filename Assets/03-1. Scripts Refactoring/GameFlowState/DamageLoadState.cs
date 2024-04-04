using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    [SerializeField] private float _gameFlowDelay;
    private Stack<BlockBase_Refact> _destroyStack;
    private WaitUntil _waitDocked;
    private WaitForSeconds _delayWait;

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
        while (_destroyStack.Count != 0)
        {
            int stackCount = _destroyStack.Count;

            // ���ÿ� ���� ��ϵ� �ı��۾�
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();
                ItemBlock_Refact itemBlock = null;

                // ������ ����� �ִ°�� ��ȭ��Ų ��, ���� ���ÿ� �߰�
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlock = block.GetComponent<ItemBlock_Refact>();
                if (itemBlock != null && !itemBlock.IsIgnited)
                    itemBlockStack.Push(itemBlock);

                block.DoBreakAction();
            }

            // ��ϵ� ���ڸ� ã�������� ���
            yield return _waitDocked;
            yield return _delayWait;

            // ��ȭ�� ������ ����� Ȱ��ȭ
            while (itemBlockStack.Count != 0)
            {
                ItemBlock_Refact popBlock = itemBlockStack.Pop();
                switch (popBlock.DestroyType)
                {
                    case ItemDestroyType.Destory:
                        yield return StartCoroutine(popBlock.GetComponent<IBlockDestroyerItem>().FillDestroyStack(_destroyStack));
                        break;

                    case ItemDestroyType.None:
                        break;
                }
            }
        }

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}