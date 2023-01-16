using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFlow
{
    public void DoGameFlowAction();
}

// 게임의 흐름을 담당
public enum eGameFlow
{
    Idle,
    LoadDamage,
    GenerateSpecialItemBlock,
    HeroAttack,
    EnemyTurn,
    BackToIdle,
}

public class GameManager : MonoBehaviour, IGameFlow
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // 블럭들을 쉽게 관리하기 위한 메모리 리스트
    public List<BlockBase> _breakList; // 블럭들 파괴 전용 리스트

    [Header("=== Game ===")]
    [SerializeField] private float _delayTime;
    public int _playerComboCount;
    private float _initDelayTimeValue;
    public GameObject _blockGen;
    public eGameFlow _gameFlow;
    private WaitForSeconds _ws;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }

        // ReFillQueue();
        _gameFlow = eGameFlow.Idle;

        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        _ws = new WaitForSeconds(1.3f);

        StartCoroutine(Test());
    }

    IEnumerator ManagePlayerCombo()
    {
        // yield return new WaitUntil(() => _gameFlowQueue.Peek() == eGameFlow.LoadDamage && _dockedCount == 63);
        yield return new WaitUntil(() => _dockedCount == 63);

        while (_gameFlow == eGameFlow.LoadDamage)
        {
            Debug.Log("LoadDamage");
            _delayTime -= Time.deltaTime;
            if (_delayTime <= 0.0f)
            {
                //if (_gameFlowQueue.Peek() == eGameFlow.LoadDamage)
                //    _gameFlowQueue.Dequeue();

                //BlockGenerator blockGen = _blockGen.GetComponent<BlockGenerator>();
                //blockGen.GenerateSpecialItemBlock(_playerComboCount);

                _delayTime = _initDelayTimeValue;
                _gameFlow++;
            }

            if (_breakList.Count != 0)
            {
                _delayTime = _initDelayTimeValue;

                yield return new WaitUntil(() => _breakList.Count == 0);
                _playerComboCount++;
            } 
        }
    }

    public void ReFillQueue()
    {
        foreach (eGameFlow state in Enum.GetValues(typeof(eGameFlow)))
        {
            // _gameFlowQueue.Enqueue(state);
        }
    }

    IEnumerator Test()
    {
        IGameFlow gameFlowSub;
        while (true)
        {
            yield return _ws;

            switch (_gameFlow)
            {
                case eGameFlow.Idle:
                    Debug.Log("Idle");
                    break;
                case eGameFlow.LoadDamage:
                    DoGameFlowAction();
                    break;
                case eGameFlow.GenerateSpecialItemBlock:
                    gameFlowSub = new BlockGenerator();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.HeroAttack:
                    gameFlowSub = new HeroTeamMgr();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.EnemyTurn:
                    gameFlowSub = new CombatSceneMgr();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.BackToIdle:
                    _gameFlow = eGameFlow.Idle;
                    break;
            }
        }
    }

    public void DoGameFlowAction()
    {
        // eGameState.LoadDamage 일때
        StartCoroutine(ManagePlayerCombo());
    }
}