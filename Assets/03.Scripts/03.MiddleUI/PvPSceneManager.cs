using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvPSceneManager : MonoBehaviour
{
    void OnEnable()
    {
        MiddleSceneTree._middleUITreeInstance._currSceneName = SceneName.BattleWaiting.ToString();
    }
}
