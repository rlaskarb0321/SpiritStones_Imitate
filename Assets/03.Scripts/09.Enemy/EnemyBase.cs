using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected float _atkPower;
    [SerializeField] protected float _hp;
    [HideInInspector] public float _currHp;
    [SerializeField] protected int _maxAttackWaitTurn;
    [HideInInspector] public int _currAttackWaitTurn;

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
        // 공격턴이 0이되었을때 델리게이트를 사용해서 동작구현을 생각중
    }

    public virtual void DieMonster()
    {

    }
}
