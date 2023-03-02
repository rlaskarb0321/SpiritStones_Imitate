using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HeroBase : MonoBehaviour
{
    public enum eState
    {
        Idle,
        Attack,
        EndAttack,
    }

    [Header("=== Combat ===")]
    public float _loadedDamage;
    public GameObject _txtObj;
    [HideInInspector] public HeroDmgText _txt;
    public eState _heroState;

    [Header("=== Anim ===")]
    [HideInInspector]public Animator _animator;
    protected int _hashAttack = Animator.StringToHash("isAttack");
    [HideInInspector] public int _hashHitted = Animator.StringToHash("isHitted");
    [HideInInspector] public int _hashDead = Animator.StringToHash("isDead");

    [Header("=== Composition ===")]
    [HideInInspector] public HeroStat _stat;
    [HideInInspector] public HeroSound _sound;

    private void Awake()
    {
        _heroState = eState.Idle;
        _stat = GetComponent<HeroStat>();
        _sound = GetComponent<HeroSound>();
        _txt = _txtObj.GetComponent<HeroDmgText>();
        _animator = GetComponent<Animator>();
    }

    public virtual void DevelopLoadedDamage()
    {
        _loadedDamage += _stat._atkPower;
        _txt.UpdateText(_loadedDamage);
    }

    public virtual void Attack(CombatSceneMgr combatSceneMgr, int targetRound)
    {
        if (_loadedDamage == 0)
        {
            return;
        }

        _animator.SetBool(_hashAttack, true);
        StartCoroutine(AttackEnemy(combatSceneMgr, targetRound));
    }

    // 애니메이션 델리게이트
    public virtual void SetAttackTiming()
    {
        _heroState = eState.Attack;
        _sound.PlayAttackSound(_stat._job[0]);
    }

    // 애니메이션 델리게이트
    public virtual void GoToIdle()
    {
        _animator.SetBool(_hashAttack, false);
        _heroState = eState.EndAttack;
    }

    public virtual void SetHurtFalse()
    {
        _animator.SetBool(_hashHitted, false);
    }

    public void LoseLoadedDmg()
    {
        _loadedDamage = 0.0f;
        _txt.UpdateText(_loadedDamage);
    }

    IEnumerator AttackEnemy(CombatSceneMgr combatScene, int targetRound)
    {
        yield return new WaitUntil(() => _heroState == eState.Attack);

        MonsterFormation targetForm =
            combatScene._monsterFormationByStage[targetRound].GetComponent<MonsterFormation>();

        bool isFocusSet = false; // 몬스터 우선 공격기능
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
        yield return null;
    }
}
