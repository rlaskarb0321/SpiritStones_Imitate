using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowMgr_Refact : MonoBehaviour
{
    public static GameFlowMgr_Refact _instance;

    [Header("게임 흐름")]
    [SerializeField] private GameFlowState _firstGameFlow;
    [SerializeField] private eGameFlow_Refact _currGameFlow; // 게임 흐름 확인용 & 제어용 enum
    [SerializeField] private float _gameStartDelay;

    [Header("게임 흐름 객체의 관리 항목")]
    [SerializeField] private int _dockedCount;

    public int DockedCount 
    { 
        get { return _dockedCount; }
        set
        {
            _dockedCount = value;
        }
    }
    public eGameFlow_Refact GameFlow { get { return _currGameFlow; } set { _currGameFlow = value; } }

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

    /// <summary>
    /// 게임이 처음 시작한 후, 블럭이 모두 자리잡을때까지 기다리고 다음 차례를 넘긴다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckBlockReady()
    {
        yield return new WaitUntil(() => _dockedCount == 63);
        yield return new WaitForSeconds(_gameStartDelay);

        ChangeGameFlow(_firstGameFlow);
    }

    /// <summary>
    /// 이 메서드를 호출하는 객체는 매개변수로 다음 게임 흐름 객체를 넘겨주어 그 객체가 일을 할 수 있게 해준다.
    /// </summary>
    /// <param name="flow">다음 차례 객체를 넣어주자</param>
    public void ChangeGameFlow(GameFlowState flow)
    {
        print(flow.gameObject.name + " Turn!");
        StartCoroutine(flow.Handle());
    }
}