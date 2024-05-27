using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태 패턴으로 턴제 시스템 구현
public class OneStrokeDrawerState : GameFlowState
{
    private Stack<BlockBase_Refact> _breakableBlockStack;
    private CanvasRayCaster _canvasRayCaster; // Composition - 1
    private BlockDestructionChecker _blockDestructionChecker; // Composition - 2
    private BlockChain _blockChain; // Composition - 3

    // NextGameFlow
    private DamageLoadState _damageLoadState;

    private void Awake()
    {
        _breakableBlockStack = new Stack<BlockBase_Refact>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();
        _blockDestructionChecker = new BlockDestructionChecker();
        _damageLoadState = _nextGameFlow.GetComponent<DamageLoadState>();
        _blockChain = GetComponent<BlockChain>();
    }

    public override IEnumerator Handle()
    {
        // OneStrokeDraw 턴일때, 한붓그리기 블럭 입력 & 해제 처리하기
        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.OneStrokeDraw;
        while (GameFlowMgr_Refact._instance.GameFlow == eGameFlow_Refact.OneStrokeDraw)
        {
            if (Input.GetMouseButton(0))
            {
                // 조건에맞는 블럭들을 넣어줌
                PushBlocksForDestroy();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _damageLoadState.SetDestroyQueue(_breakableBlockStack);
                _blockDestructionChecker.ResetCondition();
                GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
                _blockChain.InitBlockLine();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 파괴용 큐에 넣을 수 있는 블럭들을 넣어준다.
    /// </summary>
    private void PushBlocksForDestroy()
    {
        GameObject rayResult = _canvasRayCaster.ReturnRayResult();
        if (rayResult == null)
            return;

        BlockBase_Refact block = rayResult.GetComponent<BlockBase_Refact>();
        if (block == null)
            return;

        bool isDestructible = _blockDestructionChecker.IsDestructibleBlock(block.tag, block.BlockHeroType);
        bool isInsertable = _blockDestructionChecker.IsInsertableBlock(block, _breakableBlockStack);

        // 한붓그리기 취소 동작
        if (IsUndoingStroke(block, _breakableBlockStack))
        {
            BlockBase_Refact popBlock = _breakableBlockStack.Pop();
            popBlock.ActivateBlockSeletionUI(false);

            _blockChain.DrawBlockLine(block.gameObject.transform.position, _breakableBlockStack.Count);
            return;
        }
        // 넣을수있고 파괴가능한 블록을 스택에넣는다.
        if (isDestructible && isInsertable)
        {
            block.ActivateBlockSeletionUI(true);
            _breakableBlockStack.Push(block);
            _blockChain.DrawBlockLine(block.gameObject.transform.position, _breakableBlockStack.Count);
        }
    }

    /// <summary>
    /// 플레이어가 한붓그리기 취소 동작을 하고있는지 판단
    /// </summary>
    private bool IsUndoingStroke(BlockBase_Refact block, Stack<BlockBase_Refact> stack)
    {
        return stack.Count != 0 && stack.Peek() != block && stack.Contains(block);
    }
}
