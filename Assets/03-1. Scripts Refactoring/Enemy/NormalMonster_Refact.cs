using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster_Refact : EnemyBase_Refact
{
    private void Start()
    {
        _currHP = _maxHP;
        _currWaitingTurn = _maxWaitingTurn;
    }

    public override void Attack()
    {

    }

    public override void Dead()
    {

    }
}
