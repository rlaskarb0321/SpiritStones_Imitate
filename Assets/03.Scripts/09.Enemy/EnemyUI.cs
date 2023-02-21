using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private EnemyBase _enemyBase;

    [Header("=== UI ===")]
    public Image _img;
    public GameObject _focusTarget;
    public Text _waitCountTxt;
    public GameObject[] _hitEffect;
    public GameObject _hitDmgTxt;
    private DmgTxt _dmgTxt;
    [HideInInspector] public BoxCollider2D _dmgTxtSpawnRectRange;

    [Header("=== Hp ===")]
    public Image _hpBar;
    public Text _hpTxt;

    public void SetInitValue(EnemyBase enemyBase)
    {
        _dmgTxtSpawnRectRange = GetComponent<BoxCollider2D>();
        _dmgTxt = _hitDmgTxt.GetComponent<DmgTxt>();
        _enemyBase = enemyBase;
        UpdateAttackWaitTxt(_enemyBase._maxAttackWaitTurn);
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

    public Vector3 ReturnRandomPos()
    {
        Vector3 originalPos
            = new Vector3(transform.position.x, _dmgTxtSpawnRectRange.offset.y, 0.0f);
        float rangeX = this._dmgTxtSpawnRectRange.bounds.size.x;
        float rangeY = this._dmgTxtSpawnRectRange.bounds.size.y;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeY = Random.Range((rangeY / 2) * -1, rangeY / 2);
        Vector3 randomPos = new Vector3(rangeX, rangeY, 0.0f);

        return randomPos + originalPos;
    }

    Color SetColor(eNormalBlockType job)
    {
        switch (job)
        {
            case eNormalBlockType.Warrior:
                return Color.red;
            case eNormalBlockType.Archer:
                return Color.green;
            case eNormalBlockType.Thief:
                return Color.yellow;
            case eNormalBlockType.Magician:
                return Color.blue;
            case eNormalBlockType.None:
                return Color.white;
            default:
                return Color.white;
        }
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
