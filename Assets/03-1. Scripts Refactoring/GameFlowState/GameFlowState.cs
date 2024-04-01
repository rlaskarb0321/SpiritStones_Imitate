using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFlowState : MonoBehaviour
{
    [SerializeField] protected GameFlowState _nextGameFlow;

    /// <summary>
    /// ���� �帧�� �����ϴ� ��ü���� �ӹ��� ����
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