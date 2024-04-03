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
        // 스택에 블록이 3개 미만일 경우, 아무일도 일어나지않음
        if (_destroyStack.Count < 3)
        {
            while (_destroyStack.Count != 0)
            {
                _destroyStack.Pop().ActivateBlockSeletionUI(false);
                yield return null;
            }

            yield break;
        }

        // 3개 이상일 경우
        int stackCount = _destroyStack.Count;
        Stack<ItemBlock_Refact> itemBlockStack = new Stack<ItemBlock_Refact>();

        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.DamageLoad;
        while (_destroyStack.Count != 0)
        {
            // 스택에 들어온 블록들 파괴작업
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();

                // 아이템 블록이 있는경우 점화시킨 뒤, 따로 스택에 추가
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlockStack.Push(block.GetComponent<ItemBlock_Refact>());

                block.DoBreakAction();
                yield return null;
            }

            // 블록들 제자리 찾을때까지 대기
            yield return _waitDocked;
            yield return _delayWait;

            // 점화된 아이템 블록을 활성화
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

            // 블록들 제자리 찾을때까지 대기
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

        //    // 블록 정렬되고나서 잠시 대기
        //    yield return _waitDocked;
        //    yield return _delayWait;

        //    // 아이템 블록 큐 크기가 1이상이면 콤보 작업
        //    while (itemBlockStack.Count != 0)
        //    {
        //        ItemBlock_Refact itemBlock = itemBlockStack.Pop();
        //        IBlockDestroyerItem destroyerItem = itemBlock.GetComponent<IBlockDestroyerItem>();
        //        if (destroyerItem != null)
        //            _destroyStack = destroyerItem.TargetBlock();

        //        yield return null;
        //    }

        //    // 다음 게임흐름으로 진행
        //    GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
        //}
    }

    private IEnumerator DestroyBlock(Stack<ItemBlock_Refact> itemBlockStack)
    {
        while (_destroyStack.Count != 0)
        {
            BlockBase_Refact block = _destroyStack.Pop();
            block.DoBreakAction();

            // 아이템 블럭인 경우, 새로운 큐에 넣어줌
            if (block != null && block.BlockType == BlockType_Refact.Item)
                itemBlockStack.Push(block.GetComponent<ItemBlock_Refact>());

            yield return null;
        }
    }
}