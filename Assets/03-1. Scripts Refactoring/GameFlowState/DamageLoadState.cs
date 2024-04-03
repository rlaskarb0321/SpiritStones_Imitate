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
        int stackCount = _destroyStack.Count;
        Stack<ItemBlock_Refact> itemBlockStack = new Stack<ItemBlock_Refact>();

        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.DamageLoad;
        while (_destroyStack.Count != 0)
        {
            // ���ÿ� ���� ��ϵ� �ı��۾�
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();

                // ������ ����� �ִ°�� ��ȭ��Ų ��, ���� ���ÿ� �߰�
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlockStack.Push(block.GetComponent<ItemBlock_Refact>());

                block.DoBreakAction();
                yield return null;
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
                        popBlock.GetComponent<IBlockDestroyerItem>().FillDestroyStack(_destroyStack);
                        break;

                    case ItemDestroyType.None:
                        break;
                }

                yield return null;
            }

            // ��ϵ� ���ڸ� ã�������� ���
            yield return _waitDocked;
            yield return _delayWait;
            print(_destroyStack.GetHashCode());
            print("_destroyStack Count : " + _destroyStack.Count);
        }

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
        //else
        //{
        //    GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.DamageLoad;
        //    Stack<ItemBlock_Refact> itemBlockStack = new Stack<ItemBlock_Refact>();
        //    yield return StartCoroutine(DestroyBlock(itemBlockStack));

        //    // ��� ���ĵǰ��� ��� ���
        //    yield return _waitDocked;
        //    yield return _delayWait;

        //    // ������ ��� ť ũ�Ⱑ 1�̻��̸� �޺� �۾�
        //    while (itemBlockStack.Count != 0)
        //    {
        //        ItemBlock_Refact itemBlock = itemBlockStack.Pop();
        //        IBlockDestroyerItem destroyerItem = itemBlock.GetComponent<IBlockDestroyerItem>();
        //        if (destroyerItem != null)
        //            _destroyStack = destroyerItem.TargetBlock();

        //        yield return null;
        //    }

        //    // ���� �����帧���� ����
        //    GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
        //}
    }

    private IEnumerator DestroyBlock(Stack<ItemBlock_Refact> itemBlockStack)
    {
        while (_destroyStack.Count != 0)
        {
            BlockBase_Refact block = _destroyStack.Pop();
            block.DoBreakAction();

            // ������ ���� ���, ���ο� ť�� �־���
            if (block != null && block.BlockType == BlockType_Refact.Item)
                itemBlockStack.Push(block.GetComponent<ItemBlock_Refact>());

            yield return null;
        }
    }
}