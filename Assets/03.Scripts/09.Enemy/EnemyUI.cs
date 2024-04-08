using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private EnemyBase _enemyBase;

    [Header("=== UI ===")]
    [HideInInspector] public Image _img;
    public GameObject _focusTarget;
    public Text _waitCountTxt;
    public GameObject[] _hitEffect;
    public GameObject _hitDmgTxt;
    public GameObject _dmgSpawnPos;
    [HideInInspector] public AudioSource _audioSource;
    public AudioClip _hitAudioClip;

    [Header("=== Hp ===")]
    public Image _hpBar;
    public Text _hpTxt;

    private void Start()
    {
        
    }

    public void SetInitValue(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
        _img = GetComponent<Image>();
        UpdateAttackWaitTxt(_enemyBase._maxAttackWaitTurn);
        _audioSource = GetComponent<AudioSource>();
    }

    public void SpawnHitEffect()
    {
        GameObject effect = Instantiate(_hitEffect[0], this.transform.position, Quaternion.identity, this.transform);
    }

    public void UpdateAttackWaitTxt(int value)
    {
        #region 23/02/17 적 공격턴ui 수정
        //Color useColor = color.HasValue ? color.Value : Color.black;
        //_waitCountTxt.text = value.ToString();
        //_waitCountTxt.color = useColor;
        #endregion
        if (value == 0)
            return;

        if (value == 1)
        {
            Color color = Color.red;
            _waitCountTxt.text = value.ToString();
            _waitCountTxt.color = color;
        }
        else
        {
            Color color = Color.black;
            _waitCountTxt.text = value.ToString();
            _waitCountTxt.color = color;
        }
    }

    public void UpdateHp(float value)
    {
        _hpTxt.text = $"{value} / {_enemyBase._maxHp}";
        _hpBar.fillAmount = value / _enemyBase._maxHp;
    }

    #region FocusTarget UI
    public void CtrlFocusTargetActive()
    {
        if (GameManager._instance._gameFlow != eGameFlow.Idle)
            return;

        if (_focusTarget.gameObject.activeSelf)
        {
            _focusTarget.SetActive(false);
            return;
        }
        else
        {
            _focusTarget.SetActive(true);
            return;
        }
    }

    public void ActiveFocusTarget()
    {
        if (GameManager._instance._gameFlow != eGameFlow.Idle)
            return;

        _focusTarget.SetActive(true);
    }

    public void DisableFocusTarget()
    {
        if (GameManager._instance._gameFlow != eGameFlow.Idle)
            return;

        _focusTarget.SetActive(false);
    }
    #endregion FocusTarget UI
}
