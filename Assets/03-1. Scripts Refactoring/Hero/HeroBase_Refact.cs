using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase_Refact : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private eBlockHeroType_Refact[] _heroType;

    public float HP { get { return _hp; } }
    public eBlockHeroType_Refact[] HeroTypes { get { return _heroType; } }

}