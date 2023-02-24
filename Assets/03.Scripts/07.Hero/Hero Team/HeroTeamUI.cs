using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroTeamUI : MonoBehaviour
{
    [Header("=== Hp Stat ===")]
    public float _currHp;
    public float _totalHp;

    [Header("=== Hp Bar ===")]
    public GameObject _hpBar;
    public Image _redHpFill;
    public Image _mainHpFill;
    public Text _hpTxt;
    private HpBarEffect _hpBarEffect;

    [Header("=== Prefabs ===")]
    public GameObject _hitDmgTxt;
    public GameObject _hitEffect;

    private void Awake()
    {
        _hpBarEffect = _hpBar.GetComponent<HpBarEffect>();
        UpdateMainHp();
    }

    public void SpawnHitEffect()
    {
        GameObject effect = Instantiate(_hitEffect, this.transform.position, Quaternion.identity, this.transform);
    }

    // 몬스터측에서 영웅에게 데미지를 주기 위한함수
    public void DecreaseHp(float amount)
    {
        amount = Mathf.Floor(amount);
        _currHp -= amount;

        SpawnDmgTxt(amount);
        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
        }
        UpdateMainHp();
    }

    // 물약등으로 영웅들의 HP를 회복시키는 함수
    public void IncreaseHp(float amount)
    {
        amount = Mathf.Floor(amount);
        _currHp += amount;

        if (_currHp >= _totalHp)
        {
            _currHp = _totalHp;
        }
        UpdateMainHp();
        _hpBarEffect.FillRedHPFill(_currHp / _totalHp);
    }

    void SpawnDmgTxt(float amount)
    {
        GameObject txt =
            Instantiate(_hitDmgTxt, _mainHpFill.transform.position, Quaternion.identity, _mainHpFill.transform) as GameObject;

        Text useTxt = txt.GetComponent<Text>();
        useTxt.text = $"- {amount}";
        useTxt.fontSize = 95;
    }

    void UpdateMainHp()
    {
        _mainHpFill.fillAmount = _currHp / _totalHp;
        _hpTxt.text = $"{_currHp} / {_totalHp}";
    }
}
