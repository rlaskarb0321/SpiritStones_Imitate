using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer_Refact : GameFlowState
{
    private Stack<BlockBase_Refact> _breakableBlockStack;
    private CanvasRayCaster _canvasRayCaster;
    private BlockDestructionChecker _blockDestructionChecker;
    private DamageLoadState _damageLoadState;

    private void Awake()
    {
        _breakableBlockStack = new Stack<BlockBase_Refact>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();
        _blockDestructionChecker = new BlockDestructionChecker();
        _damageLoadState = _nextGameFlow.GetComponent<DamageLoadState>();
    }

    #region Update�� Coroutine���� ����
    //private void Update()
    //{
    //    if (GameFlowMgr_Refact._instance.GameFlow != eGameFlow_Refact.OneStrokeDraw)
    //        return;

    //    if (Input.GetMouseButton(0))
    //    {
    //        EnqueueBlocksForDestroy();
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        // ���� �����ڿ��� �ѱ��
    //        _damageLoadState.SetDestroyQueue(_breakableBlockQueue);
    //        _blockDestructionChecker.ResetCondition();
    //        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    //    }
    //}
    #endregion

    public override IEnumerator Handle()
    {
        // OneStrokeDraw ���϶�, �Ѻױ׸��� �� �Է� & ���� ó���ϱ�
        GameFlowMgr_Refact._instance.GameFlow = eGameFlow_Refact.OneStrokeDraw;
        while (GameFlowMgr_Refact._instance.GameFlow == eGameFlow_Refact.OneStrokeDraw)
        {
            if (Input.GetMouseButton(0))
            {
                PushBlocksForDestroy();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _damageLoadState.SetDestroyQueue(_breakableBlockStack);
                _blockDestructionChecker.ResetCondition();
                GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
            }

            yield return null;
        }
    }

    /// <summary>
    /// �ı��� ť�� ���� �� �ִ� ������ �־��ش�.
    /// </summary>
    private void PushBlocksForDestroy()
    {
        GameObject rayResult = _canvasRayCaster.ReturnRayResult_Refact();
        if (rayResult == null)
            return;

        BlockBase_Refact block = rayResult.GetComponent<BlockBase_Refact>();
        if (block == null)
            return;

        bool isDestructible = _blockDestructionChecker.IsDestructibleBlock(block.tag, block.BlockType);
        bool isInsertable = _blockDestructionChecker.IsInsertableBlock(block, _breakableBlockStack);

        // �Ѻױ׸��� ��� ����
        if (IsUndoingStroke(block, _breakableBlockStack))
        {
            _breakableBlockStack.Pop().ActivateBlockSeletionUI(false);
            return;
        }
        // �������ְ� �ı������� ����� ���ÿ��ִ´�.
        if (isDestructible && isInsertable)
        {
            block.ActivateBlockSeletionUI(true);
            _breakableBlockStack.Push(block);
        }
    }

    /// <summary>
    /// �÷��̾ �Ѻױ׸��� ��� ������ �ϰ��ִ��� �Ǵ�
    /// </summary>
    private bool IsUndoingStroke(BlockBase_Refact block, Stack<BlockBase_Refact> stack)
    {
        return stack.Count != 0 && stack.Peek() != block && stack.Contains(block);
    }
}
