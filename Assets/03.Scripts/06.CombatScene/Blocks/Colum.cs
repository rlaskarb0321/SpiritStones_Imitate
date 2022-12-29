using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colum : MonoBehaviour
{
    [SerializeField] private BlockGenerator _blockGenerator;
    [SerializeField] private GameObject _spiritGenerator;
    [SerializeField] private GameObject _spawnPos;

    void Start()
    {
        StartCoroutine(GenerateBlock());
    }

    IEnumerator GenerateBlock()
    {
        // Colum의 childCount < 5 이고, 영혼생성기에 영혼이 생성되면 밑에 구문을 실행
        yield return new WaitUntil(() => (this.transform.childCount < 5) && (_spiritGenerator.transform.childCount >= 1));

        // 그 후, 영혼의 흡수가 모두 끝나면 밑에 구문을 실행
        yield return new WaitUntil(() => _spiritGenerator.transform.childCount == 0);

        int needBlockCount = 5 - this.transform.childCount;
        for (int i = 0; i < needBlockCount; i++)
        {
            Vector2 spawnPos = _spawnPos.transform.GetChild(5 - (i + 1)).position;
            _blockGenerator.GenerateBlock(spawnPos, this.transform);
        }

        StartCoroutine(GenerateBlock());
    }
}
