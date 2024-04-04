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
        Stack<ItemBlock_Refact> itemBlockStack = new Stack<ItemBlock_Refact>();
        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.DamageLoad;
        while (_destroyStack.Count != 0)
        {
            int stackCount = _destroyStack.Count;

            // 스택에 들어온 블록들 파괴작업
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();
                ItemBlock_Refact itemBlock = null;

                // 아이템 블록이 있는경우 점화시킨 뒤, 따로 스택에 추가
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlock = block.GetComponent<ItemBlock_Refact>();
                if (itemBlock != null && !itemBlock.IsIgnited)
                    itemBlockStack.Push(itemBlock);

                block.DoBreakAction();
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