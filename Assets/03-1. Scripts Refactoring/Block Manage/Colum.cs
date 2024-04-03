using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colum : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPos;
    [SerializeField] private BlockGenerator_Refact _blockGeneratorRefact;

    private void Start()
    {
        StartCoroutine(GenerateBlock());
    }

    private IEnumerator GenerateBlock()
    {
        while (true)
        {
            yield return new WaitUntil(() => this.transform.childCount < 5);

            int needBlockCount = 5 - this.transform.childCount;
            for (int i = 0; i < needBlockCount; i++)
            {
                Vector2 spawnPos = _spawnPos.transform.GetChild(5 - (i + 1)).position;
                _blockGeneratorRefact.GenerateBlock(spawnPos, this.transform);

                yield return new WaitForSeconds(0.35f);
            }
        }
    }
}
