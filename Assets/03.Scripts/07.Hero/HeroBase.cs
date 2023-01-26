using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HeroBase : MonoBehaviour
{
    [Header("=== Stat ===")]
    [SerializeField] protected string _rank;
    [SerializeField] protected float _atkPower;

    [SerializeField] public float _hp;

    [SerializeField] protected float _maxMp;
    [HideInInspector] public float _currMp;
    [SerializeField] public eNormalBlockType[] _job;

    [Header("=== Combat ===")]
    public float _loadedDamage;
    public GameObject _txtObj;
    [HideInInspector] public HeroDmgText _txt;

    private void Start()
    {
        _txt = _txtObj.GetComponent<HeroDmgText>();
    }

    #region CombatMethod
    public virtual void DevelopLoadedDamage()
    {
        _loadedDamage += _atkPower;
        _txt.UpdateText(_loadedDamage);
    }

    public virtual void Attack(CombatSceneMgr combatSceneMgr, int targetRound)
    {
        // 라운드마다 몬스터의 진형을 선택 => 영웅이 적 진형을 인식함
        MonsterFormation targetForm =
            combatSceneMgr._monsterFormationByStage[targetRound].GetComponent<MonsterFormation>();

        bool isFocusSet = false;
        for (int targetMonster = 0; targetMonster < targetForm._monsterCount.Count; targetMonster++)
        {
            if (_loadedDamage == 0.0f)
                break;

            if (!isFocusSet)
            {
                for (int i = 0; i < targetForm._isFocusTargetOn.Length; i++)
                    if (targetForm._isFocusTargetOn[i])
                        targetMonster = i;

                isFocusSet = true;
            }

            GameObject pos = targetForm._monsterCount[targetMonster];
            EnemyBase enemy = pos.transform.GetChild(0).GetComponent<EnemyBase>();
            if (enemy._state == EnemyBase.eState.Die)
                continue;

            enemy.DecreaseMonsterHP(_loadedDamage, this);
            LoseLoadedDmg();
        }
    }

    public void LoseLoadedDmg()
    {
        _loadedDamage = 0.0f;
        _txt.UpdateText(_loadedDamage);
    }
    #endregion CombatMethod
}
