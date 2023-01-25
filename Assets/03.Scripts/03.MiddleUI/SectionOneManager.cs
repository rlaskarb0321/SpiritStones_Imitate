using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionOneManager : MonoBehaviour
{
    [Header("=== Entrance Button ===")]
    public Button _bossStage_EntranceBtn;
    public Button _stage4_EntranceBtn;
    public Button _stage3_EntranceBtn;
    public Button _stage2_EntranceBtn;
    public Button _stage1_EntranceBtn;

    void OnEnable()
    {
        MiddleSceneTree._middleUITreeInstance._currSceneName = "Section1";
    }

    void Start()
    {
        if (_stage1_EntranceBtn != null)
        {
            _stage1_EntranceBtn.onClick.AddListener(OnClickStageOneEntranceBtn);
        }
    }

    void OnClickStageOneEntranceBtn()
    {
        LoadingSceneManager.LoadScene("CombatScene");
    }
}
