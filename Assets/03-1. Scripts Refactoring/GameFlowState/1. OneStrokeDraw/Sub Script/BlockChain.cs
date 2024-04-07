using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    private LineRenderer _lr;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    public void DrawBlockLine(Vector3 pos, int lineCount)
    {
        _lr.positionCount = lineCount;
        _lr.SetPosition(lineCount - 1, pos);
    }

    public void InitBlockLine()
    {
        _lr.positionCount = 0;
    }
}