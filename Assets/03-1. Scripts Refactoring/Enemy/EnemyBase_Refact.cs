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
        if (_currHP <= 0.0f)
        {
            Dead();
            return;
        }
    }
}