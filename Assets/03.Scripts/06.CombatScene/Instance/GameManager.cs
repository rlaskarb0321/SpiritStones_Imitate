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
    public bool _canGenerateBlock;
    public GameObject _spiritGenerator;

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

        // canGenerateBlock 의 값을 정해보자

        /*
         * case 1: 노말블럭만 이었을때
         * case 2: 노말블럭 + 아이템블럭
         * case 3: 아이템블럭만 3개이상 이었을때
         */
    }
}
