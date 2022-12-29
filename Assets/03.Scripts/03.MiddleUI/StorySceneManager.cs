using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneManager : MonoBehaviour
{
    [Header("=== UI Button ===")]
    public Button _entranceBtn;
    public Button _lastQuestBtn;
    public Button _stageOneBtn;
    public Button _stageTwoBtn;
    public Button _stageThreeBtn;
    public Button _stageFourBtn;

    [Header("=== Flag ===")]
    public GameObject[] _flagGroup;
    private bool[] _isFlagSet;

    public int _selectedStageNum;

    void OnEnable()
    {
        MiddleSceneTree._middleUITreeInstance._currSceneName = SceneName.Story.ToString();
        _selectedStageNum = 1; // 기본으로 선택된 스테이지값
    }

    void Start()
    {
        if (_entranceBtn != null)
        {
            _entranceBtn.onClick.AddListener(OnClickEntranceBtn);
        }

        if (_lastQuestBtn != null)
        {
            _lastQuestBtn.onClick.AddListener(OnClickLastQuestBtn);
        }

        if (_stageOneBtn != null)
        {
            _stageOneBtn.onClick.AddListener(OnClickStageOneBtn);
        }

        if (_stageTwoBtn != null)
        {
            _stageTwoBtn.onClick.AddListener(OnClickStageTwoBtn);
        }

        if (_stageThreeBtn != null)
        {
            _stageThreeBtn.onClick.AddListener(OnClickStageThreeBtn);
        }

        if (_stageFourBtn != null)
        {
            _stageFourBtn.onClick.AddListener(OnClickStageFourBtn);
        }
    }

    void OnClickEntranceBtn()
    {
        switch (_selectedStageNum)
        {
            case 1:
                MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

                MiddleSceneTree._middleUITreeInstance._story.SetActive(false);
                MiddleSceneTree._middleUITreeInstance._sectionStage_1.SetActive(true);
                break;

            case 2:
                MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

                MiddleSceneTree._middleUITreeInstance._story.SetActive(false);
                MiddleSceneTree._middleUITreeInstance._sectionStage_2.SetActive(true);
                break;

            case 3:
                MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

                MiddleSceneTree._middleUITreeInstance._story.SetActive(false);
                MiddleSceneTree._middleUITreeInstance._sectionStage_3.SetActive(true);
                break;

            case 4:
                MiddleSceneTree._middleUITreeInstance._sceneStack.Push(this.gameObject.name);

                MiddleSceneTree._middleUITreeInstance._story.SetActive(false);
                MiddleSceneTree._middleUITreeInstance._sectionStage_4.SetActive(true);
                break;
        }
    }

    void OnClickLastQuestBtn()
    {

    }

    void OnClickStageOneBtn()
    {
        if (_selectedStageNum != 1)
        {
            _selectedStageNum = 1;
        }

        for (int i = 0; i < _flagGroup.Length; i++)
        {
            if (i == _selectedStageNum - 1)
            {
                _flagGroup[i].SetActive(true);
                continue;
            }
            _flagGroup[i].SetActive(false);
        }
    }

    void OnClickStageTwoBtn()
    {
        if (_selectedStageNum != 2)
        {
            _selectedStageNum = 2;
        }

        for (int i = 0; i < _flagGroup.Length; i++)
        {
            if (i == _selectedStageNum - 1)
            {
                _flagGroup[i].SetActive(true);
                continue;
            }
            _flagGroup[i].SetActive(false);
        }
    }

    void OnClickStageThreeBtn()
    {
        if (_selectedStageNum != 3)
        {
            _selectedStageNum = 3;
        }

        for (int i = 0; i < _flagGroup.Length; i++)
        {
            if (i == _selectedStageNum - 1)
            {
                _flagGroup[i].SetActive(true);
                continue;
            }
            _flagGroup[i].SetActive(false);
        }
    }

    void OnClickStageFourBtn()
    {
        if (_selectedStageNum != 4)
        {
            _selectedStageNum = 4;
        }

        for (int i = 0; i < _flagGroup.Length; i++)
        {
            if (i == _selectedStageNum - 1)
            {
                _flagGroup[i].SetActive(true);
                continue;
            }
            _flagGroup[i].SetActive(false);
        }
    }
}
