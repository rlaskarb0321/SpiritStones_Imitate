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
    public Image _mainHpFill;
    public Text _hpTxt;
    private HpBarEffect _hpBarEffect;

    [Header("=== Prefabs ===")]
    public GameObject _hitDmgTxt;
    public GameObject _hitEffect;

    private ShakeEffect _shake;
    private HeroTeamMgr _heroTeamMgr;

    private void Awake()
    {
        _shake = GetComponent<ShakeEffect>();
        _heroTeamMgr = GetComponent<HeroTeamMgr>();
        _hpBarEffect = _hpBar.GetComponent<HpBarEffect>();
        UpdateMainHp();
    }

    // 몬스터측에서 영웅에게 데미지를 주기 위한함수
    public void DecreaseHp(float amount)
    {
        amount = Mathf.Floor(amount);
        _currHp -= amount;

        SpawnDmgTxt(amount);
        Instantiate(_hitEffect, this.transform.position, Quaternion.identity, this.transform);

        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
            UpdateMainHp();
            GameManager._instance._gameOverMgr._heroLifeState = eHeroLife.Dead;
            _heroTeamMgr.SetHeroDead();
            return;
        }
        UpdateMainHp();
        StartCoroutine(_shake.ShakeTeam());
        _heroTeamMgr.SetHeroHurt();
    }

    // 물약등으로 영웅들의 HP를 회복시키는 함수, 부활할때도 쓰임
    public void IncreaseHp(float amount, bool isRevive = false)
    {
        if (isRevive)
        {
            _heroTeamMgr.SetHeroRevive();
        }

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

        if (0.7f < _mainHpFill.fillAmount) 
        {
            Color color;
            ColorUtility.TryParseHtmlString("#74FF00", out color);
            _mainHpFill.color = color;
        }
        else if (0.3f < _mainHpFill.fillAmount)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#FFF900", out color);
            _mainHpFill.color = color;
        }
        else
        {
            Color color;
            ColorUtility.TryParseHtmlString("#FF6F25", out color);
            _mainHpFill.color = color;
        }
    }
}
