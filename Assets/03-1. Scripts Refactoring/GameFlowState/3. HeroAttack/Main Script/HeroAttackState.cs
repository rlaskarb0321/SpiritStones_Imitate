using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    // 히어로 공격 구현
    [Header("게임 흐름 객체의 관리 항목")]
    [SerializeField] private HeroTeamMgr_Refact _heroTeamMgr;

    public override IEnumerator Handle()
    {
        yield return null;
        // GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
