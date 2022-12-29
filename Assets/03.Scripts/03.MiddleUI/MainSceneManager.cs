using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [Header("=== MainScene Button ===")]
    public Button _storyBtn;
    public Button _pvpBtn;
    public Button _mailBtn;
    public Button _myCardBtn;
    public Button _backpackBtn;

    [Header("=== PopUp Window ===")]
    public GameObject _mailPopUp;

    void OnEnable()
    {
        // Main씬으로 이동하게 된다면, 방문했던 Scene들을 쌓아놓는 스택을 초기화한 후 다른씬들과 마찬가지로 현재씬이 Main임을 알림
        MiddleSceneTree._middleUITreeInstance._currSceneName = SceneName.Main.ToString();
        MiddleSceneTree._middleUITreeInstance._sceneStack.Clear();
    }

    void Start()
    {
        if (_storyBtn != null)
        {
            _storyBtn.onClick.AddListener(OnStoryBtnClick);
        }

        if (_pvpBtn != null)
        {
            _pvpBtn.onClick.AddListener(OnPvPBtnClick);
        }

        if (_myCardBtn != null)
        {
            _myCardBtn.onClick.AddListener(OnMyCardBtnClick);
        }

        if (_mailBtn != null)
        {
            _mailBtn.onClick.AddListener(OnMailBtnClick);
        }
    }


    /// <summary>
    /// Story버튼 클릭시 Story버튼을 갖고있던 Scene인 Main씬을 스택에넣고, Story씬을 활성화 & Main씬을 비활성화
    /// </summary>
    void OnStoryBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._story.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    void OnPvPBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._battleWaiting.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    void OnMyCardBtnClick()
    {
        MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

        MiddleSceneTree._middleUITreeInstance._myCard.SetActive(true);
        MiddleSceneTree._middleUITreeInstance._main.SetActive(false);
    }

    // 팝업창 관련
    void OnMailBtnClick()
    {
        _mailPopUp.SetActive(true);
    }
}
