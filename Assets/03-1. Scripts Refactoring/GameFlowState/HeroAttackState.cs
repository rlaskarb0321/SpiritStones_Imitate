using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    // ����� ���� ����

    public override IEnumerator Handle()
    {
        yield return null;
        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
