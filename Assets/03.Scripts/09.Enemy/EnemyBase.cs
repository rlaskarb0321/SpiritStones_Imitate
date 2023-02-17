using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public enum eState
    {
        Alive,
        Attack,
        EndTurn,
        Die,
    }

    [Header("=== Stat ===")]
    public float _atkPower;
    public float _maxHp;
    public float _currHp;
    [SerializeField] public int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;
    public float _movSpeed;

    [HideInInspector] public EnemyUI _ui;

    private void OnEnable()
    {
        _state = eState.Alive;
        _currHp = _maxHp;
        _currAttackWaitTurn = _maxAttackWaitTurn;

        _ui = this.GetComponent<EnemyUI>();
        _ui.SetInitValue(this);
        _ui.UpdateHp(_currHp);
        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
    }

    // 영웅쪽에서 몬스터에게 데미지입히기 전용 함수
    public virtual void DecreaseMonsterHP(float amount, HeroBase hero)
    {
    }

    public virtual void DieMonster()
    {
    }

}
