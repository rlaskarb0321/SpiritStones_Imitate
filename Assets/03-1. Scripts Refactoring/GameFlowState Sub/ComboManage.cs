using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManage : MonoBehaviour
{
    [Header("표시할 텍스트 내용과 콤보 숫자")]
    [SerializeField] private Text _txt;
    [SerializeField] private string _context;
    [SerializeField] private int _comboCount;

    public int ComboCount
    {
        get { return _comboCount; }
        set { SetComboDisplay(value); }
    }

    private void SetComboDisplay(int value)
    {
        if (value != 0 && Mathf.Abs(value - _comboCount) > 1)
            return;
        if (value < 0)
            return;
        if (value == 0)
        {
            _comboCount = value;
            _txt.text = "";
            return;
        }

        _comboCount = value;
        _txt.text = $"{value} {_context}";
    }
}