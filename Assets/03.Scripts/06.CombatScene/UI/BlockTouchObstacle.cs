using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockTouchObstacle : MonoBehaviour
{
    Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();
    }

    private void Update()
    {
        if (GameManager._instance._gameFlowQueue.Peek() != eGameFlow.Idle)
            _img.enabled = true;
        else
            _img.enabled = false;
    }
}
