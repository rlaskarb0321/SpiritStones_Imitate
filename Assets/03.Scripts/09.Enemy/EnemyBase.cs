using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public enum eState
    {
        Waiting,
        Attack,
        Die,
    }

    [Header("=== Stat ===")]
    [SerializeField] protected float _atkPower;
    public float _maxHp;
    [HideInInspector] public float _currHp;
    [SerializeField] protected int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;

    [HideInInspector] public EnemyUI _ui;

    private void Start()
    {
        _state = eState.Waiting;
        _ui = this.GetComponent<EnemyUI>();
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
        
    }

    // 영웅쪽에서 몬스터에게 데미지입히기 전용 함수
    public virtual void DecreaseMonsterHP(float amount)
    {
        
    }

    public virtual void DieMonster()
    {
        
    }
}
