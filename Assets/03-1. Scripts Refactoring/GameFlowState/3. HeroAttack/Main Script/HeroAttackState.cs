using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : GameFlowState
{
    [Header("���� �帧 ��ü�� ���� �׸�")]
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
