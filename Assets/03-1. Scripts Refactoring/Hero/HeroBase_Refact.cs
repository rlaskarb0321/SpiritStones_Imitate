using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase_Refact : MonoBehaviour
{
    [Header("영웅 기본 정보")]
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private eBlockHeroType_Refact[] _heroType;
    [SerializeField] private GameObject _attackEffect;

    [Header("영웅 전투 관련 데이터")]
    [SerializeField] private float _accumulatedDamage;

    private Animator _animator;
    private bool _isAttacked;

    // Animator Params
    private readonly int _hashAttack = Animator.StringToHash("isAttack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public float HP { get { return _hp; } }
    public float Damage { get { return _damage; } }
    public float AccumulatedDamage
    { 
        get { return _accumulatedDamage; } 
        set
        {
            if (value < _accumulatedDamage)
                return;

            _accumulatedDamage = value;
        }
    }
    public eBlockHeroType_Refact[] HeroTypes { get { return _heroType; } }

    public IEnumerator Attack(GameObject target)
    {
        _animator.SetTrigger(_hashAttack);

        yield return new WaitUntil(() => _isAttacked);
        GenerateAttackEffect(target.transform);
    }

    /// <summary>
    /// 애니메이션 이벤트 메서드를 통해 공격 적중 이펙트의 등장 타이밍 설정
    /// </summary>
    public void SetEffectTiming() => _isAttacked = true;

    private void GenerateAttackEffect(Transform pos)
    {
        Instantiate(_attackEffect, pos.position, Quaternion.identity, pos);
    }
}