using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer_Refact : GameFlowState
{
    private Queue<BlockBase_Refact> _breakableBlockQueue;
    private CanvasRayCaster _canvasRayCaster;
    private BlockDestructionChecker _blockDestructionChecker;
    private DamageLoadState _damageLoadState;

    private void Awake()
    {
        _breakableBlockQueue = new Queue<BlockBase_Refact>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();
        _blockDestructionChecker = new BlockDestructionChecker();
        _damageLoadState = _nextGameFlow.GetComponent<DamageLoadState>();
    }

    private void Update()
    {
        if (GameFlowMgr_Refact._instance.GameFlow != eGameFlow_Refact.OneStrokeDraw)
            return;

        if (Input.GetMouseButton(0))
        {
            EnqueueBlocksForDestroy();
        }

        if (Input.GetMouseButtonUp(0))
        {
            // ���� �����ڿ��� �ѱ��
            _damageLoadState.SetDestroyQueue(_breakableBlockQueue);
            _blockDestructionChecker.ResetCondition();
            GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
        }
    }

    public override IEnumerator Handle()
    {
        // �� �����϶� �� ���� �ϱ�
        GameFlowMgr_Refact._instance.GameFlow++;
        yield return null;
    }

    /// <summary>
    /// �ı��� ť�� ���� �� �ִ� ������ �־��ش�.
    /// </summary>
    private void EnqueueBlocksForDestroy()
    {
        GameObject rayResult = _canvasRayCaster.ReturnRayResult_Refact();
        if (rayResult == null)
            return;

        BlockBase_Refact block = rayResult.GetComponent<BlockBase_Refact>();
        if (block == null)
            return;

        bool isDestructible = _blockDestructionChecker.IsDestructibleBlock(block.tag, block.BlockType);
        bool isInsertable = _blockDestructionChecker.IsInsertableBlock(block, _breakableBlockQueue);
        if (isDestructible && isInsertable)
            _breakableBlockQueue.Enqueue(block);
    }
}
