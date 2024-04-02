using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowMgr_Refact : MonoBehaviour
{
    public static GameFlowMgr_Refact _instance;

    [SerializeField] private GameFlowState _firstGameFlow;
    [SerializeField] private eGameFlow_Refact _gameFlow; // ���� �帧 Ȯ�ο� & ����� enum
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

    /// <summary>
    /// ������ ó�� ������ ��, ���� ��� �ڸ����������� ��ٸ��� ���� ���ʸ� �ѱ��.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckBlockReady()
    {
        yield return new WaitUntil(() => _dockedCount == 63);
        yield return new WaitForSeconds(_gameStartDelay);

        ChangeGameFlow(_firstGameFlow);
    }

    /// <summary>
    /// �� �޼��带 ȣ���ϴ� ��ü�� �Ű������� ���� ���� �帧 ��ü�� �Ѱ��־� �� ��ü�� ���� �� �� �ְ� ���ش�.
    /// </summary>
    /// <param name="flow">���� ���� ��ü�� �־�����</param>
    public void ChangeGameFlow(GameFlowState flow)
    {
        print(flow.gameObject.name + " Turn!");
        StartCoroutine(flow.Handle());
    }
}