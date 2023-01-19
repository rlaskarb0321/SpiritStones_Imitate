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
    private HeroDmgText _txt;

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

    public virtual void Attack(CombatSceneMgr enemyFormation, int targetRound)
    {
        // 라운드마다 몬스터의 진형
        GameObject targetForm = enemyFormation._monsterFormationByStage[targetRound];
        for (int i = 0; i < targetForm.transform.childCount; i++)
        {
            Transform pos = targetForm.transform.GetChild(i);
            EnemyBase enemy = pos.transform.GetChild(0).GetComponent<EnemyBase>();

            if (enemy._state != EnemyBase.eState.Die)
            {
                enemy.DecreaseMonsterHP(_loadedDamage);
            }
            else
            {
                continue;
            }
        }

        _loadedDamage = 0.0f;
        _txt.UpdateText(0);
    }

    #endregion CombatMethod
}
