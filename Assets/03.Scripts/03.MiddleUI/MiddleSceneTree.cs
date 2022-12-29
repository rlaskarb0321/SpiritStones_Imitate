using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneName { Main, Story, Section1, Section2, Section3, Section4, MyCard, BattleWaiting, Summon, Count }

/// <summary>
/// MiddleUI 그룹에 있는 씬들의 정보를 관리할 클래스
/// </summary>
public class MiddleSceneTree : MonoBehaviour
{
    public static MiddleSceneTree _middleUITreeInstance = null;

    [Header("=== BackGround ===")]
    public GameObject _main;
    public GameObject _story;
    public GameObject _myCard;
    public GameObject _battleWaiting;
    public GameObject _summon;

    [Header("=== Section Number ===")]
    public GameObject _sectionStage_1;
    public GameObject _sectionStage_2;
    public GameObject _sectionStage_3;
    public GameObject _sectionStage_4;

    public string _currSceneName; // 현재 보고있는 씬의 게임오브젝트 이름을 저장해주는 변수
    public Stack<string> _sceneStack; // 여태 다녀간 씬의 정보를 관리할 스택

    private void Awake()
    {
        if (_middleUITreeInstance == null)
        {
            _middleUITreeInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_middleUITreeInstance != this)
                Destroy(this.gameObject);
        }
        _sceneStack = new Stack<string>();
    }

    void Update()
    {
    }
}
