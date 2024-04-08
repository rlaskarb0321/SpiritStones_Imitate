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
    /// �ִϸ��̼� �������� �Ҵ��ϴ� ��������Ʈ �޼���
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
