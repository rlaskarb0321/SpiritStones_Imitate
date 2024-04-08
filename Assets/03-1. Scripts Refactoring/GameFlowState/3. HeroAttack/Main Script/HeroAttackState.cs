using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    [Header("게임 흐름 객체의 관리 항목")]
    [SerializeField] private float _gameFlowDelay;
    [SerializeField] private HeroTeamMgr_Refact _heroTeamMgr;
    [SerializeField] private EnemyBase_Refact _enemy;

    private WaitForSeconds _ws;

    private void Awake()
    {
        _ws = new WaitForSeconds(_gameFlowDelay);
    }

    public override IEnumerator Handle()
    {
        yield return _ws;

        _heroTeamMgr.AttackEnemy(_enemy);
        // GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }

    public void SetPriorityTarget()
    {

    }
}
