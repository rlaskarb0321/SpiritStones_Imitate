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
    [SerializeField] private float _accumulatedDamage;

    public float HP { get { return _hp; } }
    public float Damage { get { return _damage; } }
    public float AccumulatedDamage
    { 
        get { return _accumulatedDamage; } 
        set
        {
            if (value < _accumulatedDamage)
                return;

            _accumulatedDamage = value;
        }
    }
    public eBlockHeroType_Refact[] HeroTypes { get { return _heroType; } }

}