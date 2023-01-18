using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : EnemyBase // Creep이 잡몹이라는 뜻인거 같아서 이름지음
{
    public override void DoMonsterAction(GameObject heroGroup)
    {
        --_currAttackWaitTurn;

        if (_currAttackWaitTurn == 0)
        {
            HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();
            heroTeam.DecreaseHp(_atkPower);

            _currAttackWaitTurn = _maxAttackWaitTurn;
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
            return;
        }

        if (_currAttackWaitTurn == 1)
        {
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn, Color.red);
            return;
        }

        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
    }

    public override void DecreaseMonsterHP(float amount)
    {
        _currHp -= amount;

        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            _ui.UpdateHp(_currHp);
            DieMonster();
            return;
        }
        _ui.UpdateHp(_currHp);
    }

    public override void DieMonster()
    {
        _state = eState.Die;
    }
}
