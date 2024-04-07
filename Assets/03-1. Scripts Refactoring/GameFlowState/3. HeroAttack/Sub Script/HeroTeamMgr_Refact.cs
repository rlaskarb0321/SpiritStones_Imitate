using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr_Refact : MonoBehaviour
{
    [SerializeField] private HeroBase_Refact[] _heroesTeam;
    [SerializeField] private float _totalHP;
    [SerializeField] private int[] _teamHeroType; // �� ����� Ÿ���� ���� ����

    public float TotalHp { get { return _totalHP; } }
    public int[] TeamHeroType { get { return _teamHeroType; } }

    private void Awake()
    {
        InitTeamHP();
        InitHeroType();
    }

    /// <summary>
    /// �� �ִ� HP ����
    /// </summary>
    private void InitTeamHP()
    {
        for (int i = 0; i < _heroesTeam.Length; i++)
        {
            _totalHP += _heroesTeam[i].HP;
        }
    }

    /// <summary>
    /// �� ���� Ÿ�� �� ����
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
