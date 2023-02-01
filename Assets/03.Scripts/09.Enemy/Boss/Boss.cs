using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : EnemyBase
{
    [Header("=== Attack Pattern ===")]
    public GameObject[] _obstacleBlockList;
    public int _obstacleBlockGenerateCount;
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
                    AttackNormally(heroTeam);
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;

                case "ObstacleBlock Pattern":
                    GenerateObstacleBlock(_obstacleBlockGenerateCount);
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;

                case "Type By Pattern":
                    _aggressiveBoss.ChooseAggressiveAttack(heroTeam, this);
                    _currAttackWaitTurn = _maxAttackWaitTurn;
                    _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
                    break;
            } 
        }

        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
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

    void AttackNormally(HeroTeamMgr heroTeam)
    {
        heroTeam.DecreaseHp(_atkPower);
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
}
