using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : EnemyBase
{
    public override void DoMonsterAction(GameObject heroGroup)
    {
        if (!_isActive)
            StartCoroutine(EnemyRoutine(heroGroup)); 

        --_currAttackWaitTurn;
        if (_currAttackWaitTurn == 0)
        {
            _state = eState.Attack;
            return;
        }

        _state = eState.EndTurn;
    }

    public override IEnumerator EnemyRoutine(GameObject heroGroup)
    {
        _isActive = true;
        HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();
        while (_state != eState.Die)
        {
            switch (_state)
            {
                case eState.Alive:
                    _enemyUI.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;
                case eState.Attack:
                    _state = eState.Acting;
                    yield return StartCoroutine(Attack(heroTeam));
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    break;
            }

            yield return _ws;
        }
    }

    public override void DecreaseMonsterHP(float amount, HeroBase hero)
    {
        base.DecreaseMonsterHP(amount, hero);
        StartCoroutine(_shakeEffect.ShakeTeam());

        _enemyUI.SpawnHitEffect();
        _currHp -= amount;
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            _enemyUI.UpdateHp(_currHp);

            if (_state != eState.Die)
                DieMonster(); 
            return;
        }
        _enemyUI.UpdateHp(_currHp);
    }

    public override void DieMonster()
    {
        if (_enemyUI._focusTarget.activeSelf)
            _enemyUI._focusTarget.SetActive(false); 

        _state = eState.Die;
        _enemyUI._img.raycastTarget = false;
        MonsterFormation monsterFormMgr = transform.parent.parent.GetComponent<MonsterFormation>();
        monsterFormMgr.UpdateDieCount();
        monsterFormMgr.UpdateFocusTargetInfo(this.gameObject);

        StartCoroutine(FadeEnemyImage(_fadeValue));
    }

    public override IEnumerator FadeEnemyImage(float fadeValue)
    {
        return base.FadeEnemyImage(fadeValue);
    }

    #region 23/02/17 잡몹의 공격기능 수정시작
    //IEnumerator AttackToHero(HeroTeamMgr heroTeam, RectTransform targetPos, RectTransform myPos)
    //{

    //    RectTransform rect = this.GetComponent<RectTransform>();
    //    while (Vector2.Distance(targetPos.position, myPos.position) > 0.1f)
    //    {
    //        Debug.Log(myPos.position);
    //        rect.position = Vector2.MoveTowards(targetPos.position, myPos.position, 0.01f);
    //        yield return null;
    //    }

    //    heroTeam.DecreaseHp(_atkPower);
    //    while (Vector2.Distance(targetPos.position, rect.position) > 0.1f)
    //    {
    //        Debug.Log(targetPos.position + " : " + rect.position);
    //        rect.position = Vector2.MoveTowards(targetPos.position, rect.position, 0.03f);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.15f);
    //    _currAttackWaitTurn = _maxAttackWaitTurn;
    //    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);

    //    yield return null;
    //}
    #endregion
    IEnumerator Attack(HeroTeamMgr target)
    {
        Vector3 targetPos = target.transform.position; // 아군 Hero들의 위치
        Vector3 myPos = transform.position; // 내 위치
        Vector3 dist = targetPos - myPos; // 내 위치에서 아군 Hero들의 위치까지의 방향을 나타내는 벡터
        Vector3 dir = dist.normalized; // 내위치~아군hero들의 위치를 갖는 방향벡터

        while (Mathf.Abs((transform.position - targetPos).magnitude) >= 0.1f)
        {
            transform.position += dir * _movSpeed * Time.deltaTime;
            yield return null;
        }
        
        target.GetComponent<HeroTeamUI>().DecreaseHp(_atkPower);
        yield return new WaitForSeconds(1.3f);

        while (Mathf.Abs((transform.position - transform.parent.position).magnitude) >= 0.1f)
        {
            Debug.DrawLine(transform.position, transform.parent.position, Color.red);
            transform.position += -dir * _movSpeed * Time.deltaTime;
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        _state = eState.EndTurn;
    }
}
