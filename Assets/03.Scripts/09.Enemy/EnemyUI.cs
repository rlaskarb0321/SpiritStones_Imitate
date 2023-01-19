using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private EnemyBase _enemyBase;

    public Image _img;
    public Text _waitCountTxt;

    [Header("=== Hp ===")]
    public Image _hpBar;
    public Text _hpTxt;

    public void SetInitValue(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
        UpdateAttackWaitTxt(_enemyBase._maxAttackWaitTurn, Color.black);
    }

    public void UpdateAttackWaitTxt(int value, Color? color = null)
    {
        Color useColor = color.HasValue ? color.Value : Color.black;
        _waitCountTxt.text = value.ToString();
        _waitCountTxt.color = useColor;
    }

    public void UpdateHp(float value)
    {
        _hpTxt.text = $"{value} / {_enemyBase._maxHp}";
        _hpBar.fillAmount = value / _enemyBase._maxHp;
    }
}
