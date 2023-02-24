using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroTeamMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Hero ===")]
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroesTypeCountArr = new List<HeroBase>[4];
    [HideInInspector] public HeroTeamUI _heroTeamUI;

    [Header("=== Target ===")]
    public GameObject _enemyGroupObj;
    private CombatSceneMgr _enemyGroup;
    private MonsterFormation _monsterForm;

    private void Awake()
    {
        _enemyGroup = _enemyGroupObj.GetComponent<CombatSceneMgr>();
        _heroTeamUI = GetComponent<HeroTeamUI>();
        InitHeroInformation();
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        _monsterForm = _enemyGroup._monsterFormationByStage[_enemyGroup._stageMgr._currLevel - 1]
            .GetComponent<MonsterFormation>();

        StartCoroutine(Attack());
    }

    public void LooseHeroDmg()
    {
        for (int i = 0; i < _heroPos.Length; i++)
        {
            _heroPos[i].transform.GetChild(0).GetComponent<HeroBase>().LoseLoadedDmg();
        }
    }

    void InitHeroInformation()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            _heroesTypeCountArr[i] = new List<HeroBase>();

        // 아군파티의 직업종류를 Count
        foreach (GameObject pos in _heroPos)
        {
            HeroBase hero = pos.transform.GetChild(0).GetComponent<HeroBase>();
            _heroTeamUI._totalHp += hero._stat._hp;
            _heroTeamUI._currHp = _heroTeamUI._totalHp;

            for (int i = 0; i < hero._stat._job.Length; i++)
            {
                _heroesTypeCountArr[(int)hero._stat._job[i]].Add(hero);
            }
        }
    }

    public IEnumerator Attack()
    {
        int animEndCount = 0;
        int index = 0;
        while (animEndCount < _heroPos.Length)
        {
            HeroBase hero = _heroPos[index].transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup, _enemyGroup._stageMgr._currLevel - 1);

            yield return new WaitUntil(() => 
                hero._heroState == HeroBase.eState.EndAttack || hero._heroState == HeroBase.eState.Idle);

            // 영웅들 공격 사이사이 텀
            yield return new WaitForSeconds(0.35f);
            animEndCount++;
            index++;
            hero._heroState = HeroBase.eState.Idle;
        }

        // 몬스터가 전부 죽었는지 여부 검사할 텀
        yield return new WaitForSeconds(1.5f);
        if (GameManager._instance._gameFlow == eGameFlow.InStageClear)
            yield break;

        // 공격 애니메이션이 다 끝남
        yield return new WaitUntil(() => animEndCount == _heroPos.Length);
        yield return new WaitForSeconds(0.5f);

        GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
    }
}