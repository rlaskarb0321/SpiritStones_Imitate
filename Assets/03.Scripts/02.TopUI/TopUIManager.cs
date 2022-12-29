using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUIManager : MonoBehaviour
{
    [Header("=== Back Button ===")]
    public Button _backBtn;

    [Header("=== Plus Button ===")]
    public Button _energyPlusBtn;
    public Button _battleEnergyPlusBtn;
    public Button _goldPlusBtn;
    public Button _gemPlusBtn;


    void Start()
    {
        if (_backBtn != null)
        {
            _backBtn.onClick.AddListener(OnClickBackBtn);
        }
    }

    /// <summary>
    /// Main화면 외의 화면에서 해당 버튼클릭시 직전에 방문했던 화면으로 이동함, Main화면에선 아무일도 일어나지 않음
    /// </summary>
    void OnClickBackBtn()
    {
        // 현재 유저가 위치하고있는 씬의 이름을 저장하는 변수
        string currScene = MiddleSceneTree._middleUITreeInstance._currSceneName;
        if (currScene != SceneName.Main.ToString())
        {
            GameObject.Find(currScene).gameObject.SetActive(false);

            string lastSceneName = MiddleSceneTree._middleUITreeInstance._sceneStack.Pop();
            GameObject.Find("MiddleUIGroup").transform.Find(lastSceneName).gameObject.SetActive(true); 
        }
        else
        {
            Debug.Log("현재 메인씬이라 뒤로 갈 수가 없습니다.");
        }
    }
}
