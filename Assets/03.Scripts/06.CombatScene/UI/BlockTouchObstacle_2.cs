using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockTouchObstacle_2 : MonoBehaviour
{
    Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();
    }

    private void Update()
    {
        if (GameManager._instance._isPlayerAttackTurn)
            _img.enabled = true;
        else
            _img.enabled = false;
    }
}
