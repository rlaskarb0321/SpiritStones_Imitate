using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowMgr_Refact : MonoBehaviour
{
    public static GameFlowMgr_Refact _instance;

    [SerializeField] private GameFlowState _firstGameFlow;
    [SerializeField] private eGameFlow_Refact _gameFlow; // 게임 흐름 확인용 & 제어용 enum
    [SerializeField] private float _gameStartDelay;
    [SerializeField] private int _dockedCount;

    public int DockedCount 
    { 
        get { return _dockedCount; }
        set
        {
            _dockedCount = value;
        }
    }
    public eGameFlow_Refact GameFlow { get { return _gameFlow; } set { _gameFlow = value; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(CheckBlockReady());
    }

    private IEnumerator CheckBlockReady()
    {
        yield return new WaitUntil(() => _dockedCount == 63);
        yield return new WaitForSeconds(_gameStartDelay);

        ChangeGameFlow(_firstGameFlow);
    }

    public void ChangeGameFlow(GameFlowState flow)
    {
        StartCoroutine(flow.Handle());
    }
}