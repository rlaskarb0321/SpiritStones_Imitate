using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    [Header("���� �帧 ��ü�� ���� �׸�")]
    [SerializeField] private HeroTeamMgr_Refact _heroTeamMgr;

    public override IEnumerator Handle()
    {
        yield return null;
        // GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }

    public void SetPriorityTarget()
    {

    }
}
