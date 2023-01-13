using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 흐름을 담당
public enum eGameFlow
{
    Idle,
    LoadDamage,
    GenerateSpecialItemBlock,
    HeroAttack,
    EnemyTurn,
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // 블럭들을 쉽게 관리하기 위한 메모리 리스트
    public List<BlockBase> _breakList; // 블럭들 파괴 전용 리스트

    [Header("=== Game ===")]
    public Queue<eGameFlow> _gameFlowQueue;
    [SerializeField] private float _delayTime;
    public int _playerComboCount;
    private float _initDelayTimeValue;
    public GameObject _blockGen;

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

        _gameFlowQueue = new Queue<eGameFlow>();
        ReFillQueue();

        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        StartCoroutine(ManagePlayerCombo());
    }

    IEnumerator ManagePlayerCombo()
    {
        yield return new WaitUntil(() => _gameFlowQueue.Peek() == eGameFlow.LoadDamage && _dockedCount == 63);

        _delayTime -= Time.deltaTime;
        if (_delayTime <= 0.0f)
        {
            if (_gameFlowQueue.Peek() == eGameFlow.LoadDamage)
                _gameFlowQueue.Dequeue();

            BlockGenerator blockGen = _blockGen.GetComponent<BlockGenerator>();
            blockGen.GenerateSpecialItemBlock(_playerComboCount);

            _delayTime = _initDelayTimeValue;
            _playerComboCount = 0;
        }

        if (_breakList.Count != 0)
        {
            _delayTime = _initDelayTimeValue;

            yield return new WaitUntil(() => _breakList.Count == 0);
            _playerComboCount++;
        }
        StartCoroutine(ManagePlayerCombo());
    }

    public void ReFillQueue()
    {
        foreach (eGameFlow state in Enum.GetValues(typeof(eGameFlow)))
        {
            _gameFlowQueue.Enqueue(state);
        }
    }
}