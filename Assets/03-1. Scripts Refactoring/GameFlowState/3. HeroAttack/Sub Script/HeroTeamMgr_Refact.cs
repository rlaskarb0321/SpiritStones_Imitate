using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr_Refact : MonoBehaviour
{
    [SerializeField] private HeroBase_Refact[] _heroesTeam;
    [SerializeField] private float _totalHP;
    [SerializeField] private int[] _teamHeroType; // 총 히어로 타입의 수를 저장

    public float TotalHp { get { return _totalHP; } }
    public int[] TeamHeroType { get { return _teamHeroType; } }

    private void Awake()
    {
        InitTeamHP();
        InitHeroType();
    }

    /// <summary>
    /// 팀 최대 HP 설정
    /// </summary>
    private void InitTeamHP()
    {
        for (int i = 0; i < _heroesTeam.Length; i++)
        {
            _totalHP += _heroesTeam[i].HP;
        }
    }

    /// <summary>
    /// 팀 영웅 타입 수 설정
    /// </summary>
    private void InitHeroType()
    {
        for (int i = 0; i < _heroesTeam.Length; i++)
        {
            eBlockHeroType_Refact[] heroTypes = _heroesTeam[i].HeroTypes;
            for (int j = 0; j < heroTypes.Length; j++)
            {
                eBlockHeroType_Refact type = heroTypes[j];
                _teamHeroType[(int)type]++;
            }
        }
    }
}
