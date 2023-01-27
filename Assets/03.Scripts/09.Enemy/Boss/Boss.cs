using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase, IBossType
{
    public enum eBossType { Aggressive, Defensive, }
    public eBossType _eBossType;
    public IBossType _bossType;

    [Header("=== Weight Random Boss AttackPattern ===")]
    public int _normalPattern;
    public int _patternByBossType;
    public int _interruptPattern;

    private void Start()
    {
        switch (_eBossType)
        {
            case eBossType.Aggressive:
                _bossType = this.GetComponent<AggressiveBoss>();
                break;
            case eBossType.Defensive:
                _bossType = this.GetComponent<DefensiveBoss>();
                break;
        }

        if (_bossType == null)
        {
            Debug.Log("null");
        }
    }

    public override void DoMonsterAction(GameObject heroGroup)
    {
        // 가중치 랜덤을 활용해서 보스의 공격패턴을 다양하게 해보자
        // 보스타입이 공격적인지 방어적인지에 따라 다른패턴
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
