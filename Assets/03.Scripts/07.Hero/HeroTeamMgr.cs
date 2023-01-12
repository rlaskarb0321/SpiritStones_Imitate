using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr : MonoBehaviour
{
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroes = new List<HeroBase>[4];

    private void Awake()
    {
        CountHeroType();
    }

    void CountHeroType()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            _heroes[i] = new List<HeroBase>();

        // 아군파티의 직업종류를 Count
        foreach (GameObject pos in _heroPos)
        {
            HeroBase heroType = pos.transform.GetChild(0).GetComponent<HeroBase>();
            for (int i = 0; i < heroType._job.Length; i++)
            {
                _heroes[(int)heroType._job[i]].Add(heroType);
            }
        }
    }
}
