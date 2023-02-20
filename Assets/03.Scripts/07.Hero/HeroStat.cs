using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStat : MonoBehaviour
{
    [SerializeField] public string _rank;
    [SerializeField] public float _atkPower;

    [SerializeField] public float _hp;

    [SerializeField] public float _maxMp;
    [HideInInspector] public float _currMp;
    [SerializeField] public eNormalBlockType[] _job;
}
