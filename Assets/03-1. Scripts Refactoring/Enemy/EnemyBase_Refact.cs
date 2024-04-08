using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase_Refact : MonoBehaviour
{
    [Header("���� �⺻ ����")]
    [SerializeField] protected int _maxWaitingTurn;
    [SerializeField] protected float _maxHP;
    [Space(10.0f)] [SerializeField] protected int _currWaitingTurn;
    [SerializeField] protected float _currHP;
    [Space(10.0f)] [SerializeField] protected float _damage;

    /// <summary>
    /// �� ���, ���� 1�ΰ�� �����ϱ�
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// ���� ���
    /// </summary>
    public abstract void Dead();

    /// <summary>
    /// ���� ����
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