using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;

    [Header("=== Block ===")]
    public int _itemBlockPercentage; // 아이템블록의 등장확률
    public int _obstacleBlockPercentage; // 마지막 레벨일때 장애물 블럭의 등장 확률
    public bool _isLastRound; // 현재가 마지막 레벨인지
    private int _currRound; // 현재 라운드를 확인
    public int _dockedCount;
    public List<GameObject> _blockMgrList; // 블럭들을 쉽게 관리하기 위한 메모리 리스트
    public List<BlockBase> _breakList; // 블럭들 파괴 전용 리스트

    [Header("=== Game ===")]
    [SerializeField] private float _delayTime;
    public bool _isPlayerAttackTurn;
    public int _playerComboCount;
    private float _initDelayTimeValue;

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

        _isLastRound = false;
        _blockMgrList = new List<GameObject>();
        _blockMgrList.Capacity = 35;
        _breakList = new List<BlockBase>();
        _breakList.Capacity = 35;
        _dockedCount = 0;
        _initDelayTimeValue = _delayTime;
        _playerComboCount = 0;

        StartCoroutine(ManagePlayerCombo());
    }

    IEnumerator ManagePlayerCombo()
    {
        yield return new WaitUntil(() => _isPlayerAttackTurn && _dockedCount == 63);

        _delayTime -= Time.deltaTime;
        if (_delayTime <= 0.0f)
        {
            _delayTime = _initDelayTimeValue;
            _isPlayerAttackTurn = false;
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
}