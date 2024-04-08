using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void SetDamageText(int value)
    {
        _text.text = $"- {value}";
    }

    /// <summary>
    /// 애니메이션 마지막에 할당하는 델리게이트 메서드
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
