using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    // 히어로 공격 구현

    public override IEnumerator Handle()
    {
        yield return null;
        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
