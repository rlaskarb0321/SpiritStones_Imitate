using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase_Refact : MonoBehaviour
{
    [Header("몬스터 기본 정보")]
    [SerializeField] protected int _maxWaitingTurn;
    [SerializeField] protected float _maxHP;
    [Space(10.0f)] [SerializeField] protected int _currWaitingTurn;
    [SerializeField] protected float _currHP;
    [Space(10.0f)] [SerializeField] protected float _damage;

    // Composition
    [Header("컴포지션")] [SerializeField] private EnemyUI_Refact _enemyUI;
    [SerializeField] private ShakeEffect _shakeEffect;

    // Property
    public float CurrHP
    {
        get { return _currHP; }
        set
        {
            if (value > MaxHP)
                return;

            _currHP = value;
        } 
    }
    public float MaxHP
    {
        get { return _maxHP; }
        set
        {
            if (value == 0.0f || value < CurrHP)
                return;

            _maxHP = value;
        } 
    }

    // Method
    protected void Start()
    {
        _currHP = _maxHP;
        _currWaitingTurn = _maxWaitingTurn;

        _enemyUI.SetHP(this);
    }

    /// <summary>
    /// 턴 깎기, 턴이 1인경우 공격하기
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// 몬스터 사망
    /// </summary>
    public abstract void Dead();

    /// <summary>
    /// 피해 받음
    /// </summary>
    public virtual void DecreaseHP(float damage)
    {
        _currHP -= damage;
        _enemyUI.GenerateDamageEffect(damage);
        _enemyUI.SetHP(this);

        StartCoroutine(_shakeEffect.ShakeTeam());
        if (_currHP <= 0.0f)
        {
            Dead();
            return;
        }
    }
}