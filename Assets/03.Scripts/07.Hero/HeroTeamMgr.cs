using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroTeamMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Hero ===")]
    public GameObject[] _heroPos;
    [SerializeField] public List<HeroBase>[] _heroesTypeCountArr = new List<HeroBase>[4];
    public GameObject _hitDmgTxt;

    [Header("=== Hp ===")]
    public float _currHp;
    public float _totalHp;
    public Image _hpBarFill;
    public Text _hpTxt;
    private BoxCollider2D _dmgTxtSpawnRectRange;

    [Header("=== Target ===")]
    public GameObject _enemyGroupObj;
    private CombatSceneMgr _enemyGroup;
    private MonsterFormation _monsterForm;

    private void Awake()
    {
        _enemyGroup = _enemyGroupObj.GetComponent<CombatSceneMgr>();
        _dmgTxtSpawnRectRange = GetComponent<BoxCollider2D>();
        InitHeroInformation();
        UpdateHp(_currHp);
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        _monsterForm = _enemyGroup._monsterFormationByStage[_enemyGroup._stageMgr._currLevel - 1]
            .GetComponent<MonsterFormation>();

        StartCoroutine(Attack());
    }

    // 몬스터 측에서 영웅파티에 데미지를 주기위한 전용 함수
    public void DecreaseHp(float amount)
    {
        amount = Mathf.Floor(amount);
        _currHp -= amount;

        GameObject txt =
            Instantiate(_hitDmgTxt, ReturnRandomPos(), Quaternion.identity, this.transform) 
            as GameObject;
        Text useTxt = txt.GetComponent<Text>();
        useTxt.text = $"- {amount}";
        useTxt.fontSize = 100;

        if (_currHp <= 0.0f)
        {
            _currHp = 0.0f;
        }
        UpdateHp(_currHp);
    }

    // 물약 아이템을 사용했을 때 회복하기 위한 함수
    public void IncreaseHp(float amount)
    {
        _currHp += Mathf.Floor(amount);
        if (_currHp >= _totalHp)
        {
            _currHp = _totalHp;
        }
        UpdateHp(_currHp);
    }

    public void LooseHeroDmg()
    {
        for (int i = 0; i < _heroPos.Length; i++)
        {
            _heroPos[i].transform.GetChild(0).GetComponent<HeroBase>().LoseLoadedDmg();
        }
    }

    void InitHeroInformation()
    {
        for (int i = 0; i < this.transform.childCount; i++)
            _heroesTypeCountArr[i] = new List<HeroBase>();

        // 아군파티의 직업종류를 Count
        foreach (GameObject pos in _heroPos)
        {
            HeroBase hero = pos.transform.GetChild(0).GetComponent<HeroBase>();
            _totalHp += hero._stat._hp;
            _currHp = _totalHp;

            for (int i = 0; i < hero._stat._job.Length; i++)
            {
                _heroesTypeCountArr[(int)hero._stat._job[i]].Add(hero);
            }
        }
    }

    public IEnumerator Attack()
    {
        int animEndCount = 0;
        int index = 0;
        while (animEndCount < _heroPos.Length)
        {
            HeroBase hero = _heroPos[index].transform.GetChild(0).GetComponent<HeroBase>();
            hero.Attack(_enemyGroup, _enemyGroup._stageMgr._currLevel - 1);

            yield return new WaitUntil(() => 
                hero._heroState == HeroBase.eState.EndAttack || hero._heroState == HeroBase.eState.Idle);

            // 영웅들 공격 사이사이 텀
            yield return new WaitForSeconds(0.35f);
            animEndCount++;
            index++;
            hero._heroState = HeroBase.eState.Idle;
        }

        // 몬스터가 전부 죽었는지 여부 검사할 텀
        yield return new WaitForSeconds(1.5f);
        if (GameManager._instance._gameFlow == eGameFlow.InStageClear)
            yield break;

        // 공격 애니메이션이 다 끝남
        yield return new WaitUntil(() => animEndCount == _heroPos.Length);
        yield return new WaitForSeconds(0.5f);

        GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
    }

    void UpdateHp(float amount)
    {
        _hpBarFill.fillAmount = amount / _totalHp;
        _hpTxt.text = $"{amount} / {_totalHp}";
    }

    Vector3 ReturnRandomPos()
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
}