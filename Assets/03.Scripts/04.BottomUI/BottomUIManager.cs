using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomUIManager : MonoBehaviour
{
    public Button _mainBtn;
    public Button _myCardBtn;
    public Button _summonBtn;
    public Button _socialBtn;
    public Button _settingBtn;

    void Start()
    {
        if (_mainBtn != null)
        {
            _mainBtn.onClick.AddListener(OnClickMainBtn);
        }
        if (_myCardBtn != null)
        {
            _myCardBtn.onClick.AddListener(OnClickMyCardBtn);
        }
        if (_summonBtn != null)
        {
            _summonBtn.onClick.AddListener(OnClickSummonBtn);
        }
        if (_socialBtn != null)
        {
            _socialBtn.onClick.AddListener(OnClickSocialBtn);
        }
        if (_settingBtn != null)
        {
            _settingBtn.onClick.AddListener(OnClickSettingBtn);
        }
    }

    /// <summary>
    /// 팝업창과 메인창을 제외한 하단 UI버튼들은 클릭시 열고있었던 화면을 스택에 넣어주고 해당 버튼에 맞는 화면을 띄워준다
    /// </summary>
    void OnClickMainBtn()
    {
        // 현재 켜진 씬이 메인씬이 아닐시 메인버튼을 클릭하면 메인화면으로 간 뒤, 현재 켜진 씬을 끄고 메인Obj를 켜줌
        string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        if (currSceneName != SceneName.Main.ToString())
        {
            GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
            GameObject.Find(currSceneName).SetActive(false); 
        }
        else
        {
            Debug.Log("현재 메인씬이라 메인버튼을 눌러도 반응이 없습니다");
        }
    }

    void OnClickMyCardBtn()
    {
        // 현재씬이 MyCard씬이 아니라면 MyCard로 이동해주고, MyCard라면 Main으로 이동한다
        // 하단UI에 내 카드버튼을 누르기 전 현재의 씬이름을 저장후 누르면 스택에 넣어줌
        //string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        //if (currSceneName != SceneName.MyCard.ToString())
        //{
        //    MiddleSceneTree._middleUITreeInstance._sceneStack.Push(currSceneName);

        //    // MiddleUIGroup들 중에서 누르기 전 씬을찾아 꺼주고, MyCard 게임Obj를 켜줌
        //    GameObject.Find("MiddleUIGroup").transform.Find(currSceneName).gameObject.SetActive(false);
        //    MiddleSceneTree._middleUITreeInstance._myCard.SetActive(true); 
        //}
        //else
        //{
        //    GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
        //    GameObject.Find(currSceneName).SetActive(false);
        //}
    }

    void OnClickSummonBtn()
    {
        // 현재씬이 Summon씬이 아니라면 Summon으로 이동해주고, Summon이라면 Main으로 이동한다
        // 하단UI에 내 카드버튼을 누르기전의 씬이름을 저장후 누르면 스택에 넣어줌
        //string currSceneName = MiddleSceneTree._middleUITreeInstance._currSceneName;
        //if (currSceneName != SceneName.Summon.ToString())
        //{
        //    MiddleSceneTree._middleUITreeInstance._sceneStack.Push(currSceneName);

        //    // MiddleUIGroup들 중에서 누르기 전 씬을찾아 꺼주고, Summon 게임Obj를 켜줌
        //    GameObject.Find("MiddleUIGroup").transform.Find(currSceneName).gameObject.SetActive(false);
        //    MiddleSceneTree._middleUITreeInstance._summon.SetActive(true); // Summon으로 이동
        //}
        //else
        //{
        //    GameObject.Find("MiddleUIGroup").transform.Find(SceneName.Main.ToString()).gameObject.SetActive(true);
        //    GameObject.Find(currSceneName).SetActive(false);
        //}
    }

    void OnClickSocialBtn()
    {
        // 팝업창
    }

    void OnClickSettingBtn()
    {
        // 팝업창
    }

}
