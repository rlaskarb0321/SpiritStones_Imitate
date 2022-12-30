using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colum : MonoBehaviour
{
    [SerializeField] private BlockGenerator _blockGenerator;
    [SerializeField] private GameObject _spawnPos;

    void Start()
    {
        StartCoroutine(GenerateBlock());
    }

    IEnumerator GenerateBlock()
    {
        yield return new WaitUntil(() => this.transform.childCount < 5 &&
        GameManager._instance._dockedCount != 63);

        yield return new WaitUntil(() => GameManager._instance._canGenerateBlock);

        int needBlockCount = 5 - this.transform.childCount;
        for (int i = 0; i < needBlockCount; i++)
        {
            Vector2 spawnPos = _spawnPos.transform.GetChild(5 - (i + 1)).position;
            _blockGenerator.GenerateBlock(spawnPos, this.transform);
        }

        if (GameManager._instance._canGenerateBlock != false)
        {
            GameManager._instance._canGenerateBlock = false; 
        }
        StartCoroutine(GenerateBlock());
    }
}
