using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeamMgr_Refact : MonoBehaviour
{
    [SerializeField] private HeroBase_Refact[] _heroesTeam;
    [SerializeField] private float _teamHP;

    Dictionary<eBlockHeroType_Refact, List<HeroBase_Refact>> _teamHeroDict;

    public Dictionary<eBlockHeroType_Refact, List<HeroBase_Refact>> TeamHeroDict { get { return _teamHeroDict; } }
    public float TeamHP { get { return _teamHP; } }

    private void Awake()
    {
        InitTeamHP();
        InitTeamHeroDict();
    }

    /// <summary>
    /// ∆¿ √÷¥Î HP º≥¡§
    /// </summary>
    private void InitTeamHP()
    {
        for (int i = 0; i < _heroesTeam.Length; i++)
        {
            _teamHP += _heroesTeam[i].HP;
        }
    }

    private void InitTeamHeroDict()
    {
        _teamHeroDict = new Dictionary<eBlockHeroType_Refact, List<HeroBase_Refact>>();
        for (int i = 0; i < _heroesTeam.Length; i++)
        {
            eBlockHeroType_Refact[] keyHeroTypes = _heroesTeam[i].HeroTypes;
            HeroBase_Refact valueHero = _heroesTeam[i];
            for (int j = 0; j < keyHeroTypes.Length; j++)
            {
                eBlockHeroType_Refact key = keyHeroTypes[j];
                if (!_teamHeroDict.ContainsKey(key))
                    _teamHeroDict.Add(key, new List<HeroBase_Refact>());

                _teamHeroDict[key].Add(valueHero);
            }
        }
    }
}
