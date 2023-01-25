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

    public virtual void Attack(CombatSceneMgr enemyFormation, int targetRound)
    {
        // 라운드마다 몬스터의 진형을 선택 => 영웅이 적 진형을 인식함
        GameObject targetForm = enemyFormation._monsterFormationByStage[targetRound];
        int targetMonster = 0; // 타겟이 될 몬스터의 진형 인덱스 : 만일 점사공격이 활성화되면 해당 인덱스로 지정
        for (int i = 0; i < targetForm.transform.childCount; i++)
        {
            if (targetMonster >= targetForm.transform.childCount)
                break;

            Transform pos = targetForm.transform.GetChild(targetMonster);
            EnemyBase enemy = pos.transform.GetChild(0).GetComponent<EnemyBase>();
            if (enemy._state == EnemyBase.eState.Die)
            {
                targetMonster++;
                continue;
            }

            enemy.DecreaseMonsterHP(_loadedDamage, this);
        }
    }

    #endregion CombatMethod
}
