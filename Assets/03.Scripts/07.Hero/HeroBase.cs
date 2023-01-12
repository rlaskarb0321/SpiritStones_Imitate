using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass
{
    Warrior,
    Archer,
    Magician,
    Thief,
}

public abstract class HeroBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected string _rank;
    [SerializeField] protected float _atkPower;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _maxMp;
    [HideInInspector] public float _currMp;
    [SerializeField] protected HeroClass[] _job;

    [Header("=== Combat ===")]
    [HideInInspector] public float _loadedDamage;

    #region CombatMethod
    public virtual void DevelopLoadedDamage()
    {
        _loadedDamage += _atkPower;
    }

    public virtual void Attack(GameObject enemyFormation)
    {
        // 적에대한 정보가 필요함
    }
    #endregion CombatMethod
}
