using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI_Refact : MonoBehaviour
{
    [Header("컴포지션")]
    [SerializeField] private DamageEffect _hitDamagePrefab;
    [SerializeField] private HpBar _hpBarUI;
    [Space(10.0f)] [SerializeField] private Transform _hitDamageGeneratePos;

    public void GenerateDamageEffect(float value)
    {
        DamageEffect damageEffect = Instantiate(_hitDamagePrefab, _hitDamageGeneratePos.position, Quaternion.identity, transform);
        damageEffect.SetDamageText((int)value);
    }

    public void SetHP(EnemyBase_Refact enemyBase)
    {
        _hpBarUI.SetHpValue(enemyBase.CurrHP, enemyBase.MaxHP);
    }
}
