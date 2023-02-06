using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : EnemyBase // Creep이 잡몹이라는 뜻인거 같아서 이름지음
{
    public override void DoMonsterAction(GameObject heroGroup)
    {
        --_currAttackWaitTurn;
        if (_currAttackWaitTurn == 0)
        {
            _state = eState.Attack;
            HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();
            StartCoroutine(AttackToHero(heroTeam, heroTeam.GetComponent<RectTransform>(), this.GetComponent<RectTransform>()));
        }

        if (_currAttackWaitTurn == 1)
        {
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn, Color.red);
        }
        else
        {
            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        }

        _state = eState.EndTurn;
    }

    public override void DecreaseMonsterHP(float amount, HeroBase hero)
    {
        if (amount == 0)
            return;

        _currHp -= amount;
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            _ui.UpdateHp(_currHp);

            if (_state != eState.Die)
                DieMonster(); 
            return;
        }
        _ui.UpdateHp(_currHp);
    }

    public override void DieMonster()
    {
        if (_ui._focusTarget.activeSelf)
            _ui._focusTarget.SetActive(false); 

        _state = eState.Die;
        MonsterFormation monsterFormMgr = transform.parent.parent.GetComponent<MonsterFormation>();
        monsterFormMgr.UpdateDieCount();
        monsterFormMgr.UpdateFocusTargetInfo(this.gameObject);

        gameObject.SetActive(false);
    }

    IEnumerator AttackToHero(HeroTeamMgr heroTeam, RectTransform targetPos, RectTransform myPos)
    {
        RectTransform initPos = myPos;
        RectTransform rect = this.GetComponent<RectTransform>();
        while (Vector2.Distance(targetPos.position, myPos.position) > 0.1f)
        {
            rect.position = Vector2.MoveTowards(targetPos.position, myPos.position, 0.01f);
            yield return null;
        }

        heroTeam.DecreaseHp(_atkPower);
        while (Vector2.Distance(initPos.position, myPos.position) > 0.1f)
        {
            rect.position = Vector2.MoveTowards(initPos.position, myPos.position, 0.03f);
            yield return null;
        }

        yield return new WaitForSeconds(0.15f);
        _currAttackWaitTurn = _maxAttackWaitTurn;
        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
    }
}
