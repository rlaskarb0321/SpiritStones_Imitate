using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase
{
    [Header("=== Attack Pattern ===")]
    public GameObject[] _obstacleBlockList;
    public int _blockGenerateCount;
    [HideInInspector] public AggressiveBossPattern _aggressiveBoss;
    private BossWeightedRandomPattern _weightRandomPattern;
    public float _stepBackDelaySpeed;

    private void Start()
    {
        _aggressiveBoss = GetComponent<AggressiveBossPattern>();
        _weightRandomPattern = GetComponent<BossWeightedRandomPattern>();
        _weightRandomPattern.SetWeightData();
    }

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
        #region 23/02/18 보스 공격패턴 수정
        //--_currAttackWaitTurn;
        //if (_currAttackWaitTurn == 0)
        //{
        //    // 가중치랜덤값에의해 보스의 공격방식을 결정
        //    string attackType = _weightRandomPattern.ReturnRandomPattern();
        //    HeroTeamMgr heroTeam = heroGroup.GetComponent<HeroTeamMgr>();
        //    _state = eState.Attack;

        //    switch (attackType)
        //    {
        //        case "Normal Pattern":
        //            AttackNormally(heroTeam);

        //            _currAttackWaitTurn = _maxAttackWaitTurn;
        //            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        //            _state = eState.EndTurn;
        //            return;

        //        case "ObstacleBlock Pattern":
        //            GenerateObstacleBlock(_obstacleBlockGenerateCount);

        //            _currAttackWaitTurn = _maxAttackWaitTurn;
        //            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        //            _state = eState.EndTurn;
        //            return;

        //        case "Type By Pattern":
        //            _aggressiveBoss.ChooseAggressiveAttack(heroTeam, this);

        //            _currAttackWaitTurn = _maxAttackWaitTurn;
        //            _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        //            _state = eState.EndTurn;
        //            return;
        //    } 
        //}

        //_ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        //_state = eState.EndTurn;
        #endregion
    }

    public override IEnumerator EnemyRoutine(GameObject heroGroup)
    {
        string attackType = _weightRandomPattern.ReturnRandomPattern();
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
                    switch (attackType)
                    {
                        case "Normal Pattern":
                            yield return StartCoroutine(ActAttackMotion(heroTeam));
                            _currAttackWaitTurn = _maxAttackWaitTurn;
                            break;

                        case "ObstacleBlock Pattern":
                            GenerateObstacleBlock(_blockGenerateCount);
                            _currAttackWaitTurn = _maxAttackWaitTurn;
                            _state = eState.EndTurn;
                            break;
                        case "Type By Pattern":
                            yield return StartCoroutine(ActTypeAttackMotion(heroTeam));
                            _currAttackWaitTurn = _maxAttackWaitTurn;
                            break;
                    }
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

    void GenerateObstacleBlock(int generateCount)
    {
        int randObstacleBlock = Random.Range(0, _obstacleBlockList.Length);
        GameObject obstacleBlockPrefab = _obstacleBlockList[randObstacleBlock];
        
        for (int i = 0; i < generateCount; i++)
        {
            int preyBlockIdx = Random.Range(0, GameManager._instance._blockMgrList.Capacity);
            //while (GameManager._instance._blockMgrList[preyBlockIdx].tag == "ObstacleBlock")
            //    preyBlockIdx = Random.Range(0, GameManager._instance._blockMgrList.Capacity);

            BlockBase preyBlock = GameManager._instance._blockMgrList[preyBlockIdx].GetComponent<BlockBase>();
            Transform preyBlockParentTr = preyBlock.transform.parent;

            preyBlock.ConvertBlockType(obstacleBlockPrefab.name, obstacleBlockPrefab.GetComponent<Image>().sprite);
        }
    }

    IEnumerator ActAttackMotion(HeroTeamMgr target)
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
        yield return new WaitForSeconds(1.5f);

        while (Mathf.Abs((transform.position - transform.parent.position).magnitude) >= 0.03f)
        {
            transform.position += -dir * (_movSpeed - _stepBackDelaySpeed) * Time.deltaTime;
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        _state = eState.EndTurn;
    }

    IEnumerator ActTypeAttackMotion(HeroTeamMgr target)
    {
        Vector3 targetPos = target.transform.position; // 아군 Hero들의 위치
        Vector3 myPos = transform.position; // 내 위치
        Vector3 dist = targetPos - myPos; // 내 위치에서 아군 Hero들의 위치까지의 방향을 나타내는 벡터
        Vector3 dir = dist.normalized; // 내위치~아군hero들의 위치를 갖는 방향벡터

        int frameCount = 15;
        while (frameCount >= 0)
        {
            transform.position += dir * (_movSpeed) * Time.deltaTime;
            frameCount--;
            yield return null;
        }

        _aggressiveBoss.ChooseAggressiveAttack(target, this);

        int frameCount2 = 10;
        while (frameCount2 >= 0)
        {
            transform.position -= dir * (_movSpeed - _stepBackDelaySpeed) * Time.deltaTime;
            frameCount2--;
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        _state = eState.EndTurn;
    }
}
