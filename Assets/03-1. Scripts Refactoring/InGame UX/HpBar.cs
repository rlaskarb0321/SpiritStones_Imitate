using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Image _hpFill;

    public void SetHpValue(float currHP, float maxHP)
    {
        _hpFill.fillAmount = currHP / maxHP;
        _hpText.text = $"{currHP} / {maxHP}";
    }
}
