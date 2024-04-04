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
        int comboCount = 0;
        while (_destroyStack.Count != 0)
        {
            bool isContainItemBlock = false;
            int stackCount = _destroyStack.Count;

            // 스택에 들어온 블록들(노말 블록, 아이템 블록, 기타 등등) 파괴작업
            for (int i = 0; i < stackCount; i++)
            {
                BlockBase_Refact block = _destroyStack.Pop();
                ItemBlock_Refact itemBlock = null;

                // 아이템 블록이 있는경우 점화시킨 뒤, 따로 스택에 추가
                if (block.BlockType == BlockType_Refact.Item)
                    itemBlock = block.GetComponent<ItemBlock_Refact>();
                if (itemBlock != null && !itemBlock.IsIgnited)
                {
                    itemBlockStack.Push(itemBlock);
                    isContainItemBlock = true;
                }

                block.DoBreakAction();
            }

            // 블록들 제자리 찾을때까지 대기
            yield return _waitDocked;
            yield return _delayWait;

            // 스택속에 있던 아이템 블럭들 처리, 점화된 아이템 블록을 활성화
            _itemCount = itemBlockStack.Count;
            while (itemBlockStack.Count != 0)
            {
                ItemBlock_Refact popBlock = itemBlockStack.Pop();
                switch (popBlock.DestroyType)
                {
                    case ItemDestroyType.Destory:
                        IBlockDestroyerItem destroyerBlock = popBlock.GetComponent<IBlockDestroyerItem>();

                        // 여기서 yield return을 쓰면 한번에 넣은 아이템들이 하나하나 터짐
                        //yield return StartCoroutine(destroyerBlock.FillDestroyStack(_destroyStack));

                        // 이 주석을 해제하면 한번에 넣은 아이템들이 한꺼번에 터짐
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
                // 여기서 아이템 블록들 모두 효과가 끝날때까지 기다려줘야한다.
                yield return new WaitUntil(() => _itemCount == 0);
                comboCount++;
            }
        }

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}