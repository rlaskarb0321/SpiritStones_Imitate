using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFlowState : MonoBehaviour
{
    [SerializeField] protected GameFlowState _nextGameFlow;

    /// <summary>
    /// 게임 흐름에 관여하는 객체들의 임무를 수행
    /// </summary>
    public abstract IEnumerator Handle();
}

public enum eGameFlow_Refact
{
    GameStart,
    OneStrokeDraw,
    DamageLoad,
    HeroAttack,
    EnemyAttack,
    FinishCycle,
}