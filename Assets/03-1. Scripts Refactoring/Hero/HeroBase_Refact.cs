using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase_Refact : MonoBehaviour
{
    [Header("���� �⺻ ����")]
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private eBlockHeroType_Refact[] _heroType;
    [SerializeField] private GameObject _attackEffect;

    [Header("���� ���� ���� ������")]
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
    /// �ִϸ��̼� �̺�Ʈ �޼��带 ���� ���� ���� ����Ʈ�� ���� Ÿ�̹� ����
    /// </summary>
    public void SetEffectTiming() => _isAttacked = true;

    private void GenerateAttackEffect(Transform pos)
    {
        Instantiate(_attackEffect, pos.position, Quaternion.identity, pos);
    }
}