using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase_Refact : MonoBehaviour
{
    [Header("���� �⺻ ����")]
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private eBlockHeroType_Refact[] _heroType;

    [Header("���� ���� ���� ������")]
    [SerializeField] private float _loadedDamage;

    public float HP { get { return _hp; } }
    public float LoadedDamage 
    { 
        get { return _loadedDamage; } 
        set
        {

        }
    }
    public eBlockHeroType_Refact[] HeroTypes { get { return _heroType; } }

}