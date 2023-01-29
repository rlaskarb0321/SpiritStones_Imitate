using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase
{
    [HideInInspector] public AggressiveBossPattern _aggressiveBoss;
    public BossWeightedRandomPattern _weightRandomPattern;

    private void Start()
    {
        _aggressiveBoss = GetComponent<AggressiveBossPattern>();
        _weightRandomPattern = GetComponent<BossWeightedRandomPattern>();
        _weightRandomPattern.SetWeightData();
    }

    public override void DoMonsterAction(GameObject heroGroup)
    {
        --_currAttackWaitTurn;

        if (_currAttackWaitTurn == 1)
        {
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn, Color.red);
            return;
        }

        if (_currAttackWaitTurn == 0)
        {
            // 가중치랜덤값에의해 보스의 공격방식을 결정
            string attackType = _weightRandomPattern.ReturnRandomPattern();
            HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();

            switch (attackType)
            {
                case "Normal Pattern":
                    Debug.Log("Normal");

                    heroTeam.DecreaseHp(_atkPower);
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;

                case "ObstacleBlock Pattern":
                    Debug.Log("Obstacle");

                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;

                case "Type By Pattern":
                    Debug.Log("Type");

                    _aggressiveBoss.TestAttack();
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;
            } 
        }

        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
    }

    public override void DecreaseMonsterHP(float amount, HeroBase hero)
    {

    }

    public override void DieMonster()
    {

    }

    public void NormalBossAttack()
    {

    }
}
