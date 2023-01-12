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
    [SerializeField] protected float _hp;
    [HideInInspector] public float _currHp;
    [SerializeField] protected int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;

    public Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();
        _state = eState.Waiting;
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
        
    }

    public virtual void DieMonster()
    {
        _state = eState.Die;
    }
}
