using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public enum eState
    {
        Alive,
        Attack,
        EndTurn,
        Die,
        Acting,
    }

    [Header("=== Stat ===")]
    public float _atkPower;
    public float _maxHp;
    public float _currHp;
    [SerializeField] public int _maxAttackWaitTurn;
    public int _currAttackWaitTurn;
    public eState _state;
    public float _movSpeed;
    [HideInInspector] public bool _isActive;
    [HideInInspector] public WaitForSeconds _ws;

    [HideInInspector] public EnemyUI _ui;
    public GameObject _hitDmgTxt;
    private DmgTxt _dmgTxt;
    private BoxCollider2D _dmgTxtSpawnRectRange;

    private void OnEnable()
    {
        _state = eState.Alive;
        _currHp = _maxHp;
        _currAttackWaitTurn = _maxAttackWaitTurn;

        _dmgTxtSpawnRectRange = GetComponent<BoxCollider2D>();
        _dmgTxt = _hitDmgTxt.GetComponent<DmgTxt>();
        _ui = this.GetComponent<EnemyUI>();
        _ui.SetInitValue(this);
        _ui.UpdateHp(_currHp);
        _ui.UpdateAttackWaitTxt(_currAttackWaitTurn);
        _ws = new WaitForSeconds(0.1f);
    }

    public virtual void DoMonsterAction(GameObject heroGroup)
    {
    }

    // 영웅쪽에서 몬스터에게 데미지입히기 전용 함수
    public virtual void DecreaseMonsterHP(float amount, HeroBase hero)
    {
        amount = Mathf.Floor(amount);
        if (amount == 0)
            return;

        GameObject txt = Instantiate(_hitDmgTxt, ReturnRandomPos(), Quaternion.identity,
            GameObject.Find("Canvas").transform) as GameObject;
        //_dmgTxt.SetColor(SetColor(hero._job[0]));
        txt.GetComponent<Text>().text = $"- {amount}";
    }

    public virtual void DieMonster()
    {
    }

    public virtual IEnumerator EnemyRoutine(GameObject heroGroup)
    {
        yield return null;
    }

    Vector3 ReturnRandomPos()
    {
        float rangeX = _dmgTxtSpawnRectRange.bounds.size.x;
        float rangeY = _dmgTxtSpawnRectRange.bounds.size.y;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeY = Random.Range((rangeY / 2) * -1, rangeY / 2);
        Vector3 randomPos = new Vector3(rangeX, rangeY, 0.0f);

        return randomPos;
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
}
