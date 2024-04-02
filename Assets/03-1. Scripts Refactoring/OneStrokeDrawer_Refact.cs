using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer_Refact : GameFlowState
{
    //[SerializeField]
    //private GameObject _nextGameFlowObj;

    //// private IGameFlowState _nextGameFlow;
    //private CanvasRayCaster _canvasRayCaster;
    //private Queue<IBreakableBlock> _breakeBlockQueue;

    //private void Awake()
    //{
    //    _canvasRayCaster = GetComponent<CanvasRayCaster>();
    //    _breakeBlockQueue = new Queue<IBreakableBlock>();
    //    //if (_nextGameFlowObj != null)
    //    //    _nextGameFlow = _nextGameFlowObj.GetComponent<IGameFlowState>();
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        GameObject rayResult = _canvasRayCaster.ReturnRayResult_Refact();
    //        if (rayResult == null)
    //            return;

    //        IBreakableBlock breakableBlock = rayResult.GetComponent<IBreakableBlock>();
    //        if (breakableBlock == null)
    //            return;

    //        breakableBlock.DoBlockBreak();
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {

    //    }
    //}

    private Queue<BlockBase_Refact> _breakableBlockQueue;
    private CanvasRayCaster _canvasRayCaster;
    private BlockDestructionChecker _blockDestructionChecker;

    private void Awake()
    {
        _breakableBlockQueue = new Queue<BlockBase_Refact>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();
        _blockDestructionChecker = new BlockDestructionChecker();
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

        }

        print(_breakableBlockQueue.Count);
    }

    public override IEnumerator Handle()
    {
        GameFlowMgr_Refact._instance.GameFlow++;

        yield return new WaitForSeconds(100.0f);
        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }

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
