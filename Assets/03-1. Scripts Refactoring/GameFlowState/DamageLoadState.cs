using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    // �� �ı� & ���� ������ �ױ� ����

    private Queue<BlockBase_Refact> _destroyQueue;

    public void SetDestroyQueue(Queue<BlockBase_Refact> queue)
    {
        _destroyQueue = queue;
    }

    public override IEnumerator Handle()
    {
        yield return null;
        // GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
