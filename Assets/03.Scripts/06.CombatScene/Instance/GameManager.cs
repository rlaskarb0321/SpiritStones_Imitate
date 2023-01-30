using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    StageClear,
    BackToIdle,
    InProgress, // 무한호출을 방지하기위해 선언한 변수
    BossStageClear,
}

public class GameManager : MonoBehaviour, IGameFlow
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // 블럭들을 쉽게 관리하기 위한 메모리 리스트
    public List<BlockBase> _breakList; // 블럭들 파괴 전용 리스트
    public List<GameObject> _obstacleBlockList; // 방해물 블럭 전용 리스트

    [Header("=== Game ===")]
    [SerializeField] private float _delayTime;
    public int _playerComboCount;
    private float _initDelayTimeValue;
    public eGameFlow _gameFlow;
    private WaitForSeconds _ws;
    public Image _resultUI;

    [Header("=== Composition ===")]
    public GameObject _blockGeneratorObj;
    public GameObject _heroTeamMgrObj;
    public GameObject _combatSceneMgrObj;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }
        _gameFlow = eGameFlow.Idle;

        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        _obstacleBlockList = new List<GameObject>();
        _obstacleBlockList.Capacity = 35;
        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        _ws = new WaitForSeconds(1.3f);

        StartCoroutine(InGameMainFlow());
    }

    IEnumerator InGameMainFlow()
    {
        IGameFlow gameFlowSub;

        // 보스가 죽지 않아서 스테이지 진행중일때 까지 실행
        while (true)
        {
            switch (_gameFlow)
            {
                case eGameFlow.LoadDamage:
                    DoGameFlowAction();
                    break;
                case eGameFlow.GenerateSpecialItemBlock:
                    gameFlowSub = _blockGeneratorObj.GetComponent<BlockGenerator>();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.HeroAttack:
                    gameFlowSub = _heroTeamMgrObj.GetComponent<HeroTeamMgr>();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.StageClear:
                    gameFlowSub = _combatSceneMgrObj.GetComponent<CombatSceneMgr>();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.EnemyTurn:
                    gameFlowSub = _combatSceneMgrObj.GetComponent<CombatSceneMgr>();
                    gameFlowSub.DoGameFlowAction();
                    break;
                case eGameFlow.BackToIdle:
                    for (int i = 0; i < _obstacleBlockList.Count; i++)
                    {
                        ObstacleBlock obstacleBlock = _obstacleBlockList[i].GetComponent<ObstacleBlock>();
                        obstacleBlock.DoHarmfulAction();
                    }
                    _gameFlow = eGameFlow.Idle;
                    break;
                case eGameFlow.BossStageClear:
                    _resultUI.gameObject.SetActive(true);
                    break;
            }
            yield return null;
        }
    }

    public void DoGameFlowAction()
    {
        _gameFlow = eGameFlow.InProgress;
        StartCoroutine(ManagePlayerCombo());
    }

    IEnumerator ManagePlayerCombo()
    {
        while (_gameFlow == eGameFlow.InProgress)
        {
            yield return new WaitUntil(() => _dockedCount == 63);

            _delayTime -= Time.deltaTime;
            yield return null;

            if (_delayTime <= 0.0f)
            {
                _delayTime = _initDelayTimeValue;
                _gameFlow = eGameFlow.LoadDamage;
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
}