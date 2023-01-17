using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Hero ===")]
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroesTypeCountArr = new List<HeroBase>[4];
    [HideInInspector] public float _totalHp;

    [Header("=== Target ===")]
    public GameObject _enemyGroup;

    private void Awake()
    {
        InitHeroInformation();
    }

    void InitHeroInformation()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            _heroesTypeCountArr[i] = new List<HeroBase>();

        // 아군파티의 직업종류를 Count
        foreach (GameObject pos in _heroPos)
        {
            HeroBase heroType = pos.transform.GetChild(0).GetComponent<HeroBase>();
            _totalHp += heroType._hp;
            for (int i = 0; i < heroType._job.Length; i++)
            {
                _heroesTypeCountArr[(int)heroType._job[i]].Add(heroType);
            }
        }
    }

    public void DoGameFlowAction()
    {
        // eGameState.HeroAttack 일때
        Attack();
        Debug.Log("공격이 끝났습니다");

        GameManager._instance._gameFlow++;
    }

    void Attack()
    {
        foreach (GameObject pos in _heroPos)
        {
            HeroBase hero = pos.transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup);
        }
    }
}
