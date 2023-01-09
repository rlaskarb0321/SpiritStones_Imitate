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
        yield return new WaitForSeconds(0.3f);

        int needBlockCount = 5 - this.transform.childCount;
        for (int i = 0; i < needBlockCount; i++)
        {
            Vector2 spawnPos = _spawnPos.transform.GetChild(5 - (i + 1)).position;
            _blockGenerator.GenerateBlock(spawnPos, this.transform);
        }

        StartCoroutine(GenerateBlock());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ItemBlock")
        {
            ItemBlock itemBlock = collision.gameObject.GetComponent<ItemBlock>();
            if (itemBlock._specialType == eSpecialBlockType.Sword_Warrior)
            {
                SwordItem swordItem = itemBlock.GetComponent<SwordItem>();
                swordItem._isIn_YequalX_Zone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ItemBlock")
        {
            ItemBlock itemBlock = collision.gameObject.GetComponent<ItemBlock>();
            if (itemBlock._specialType == eSpecialBlockType.Sword_Warrior)
            {
                SwordItem swordItem = itemBlock.GetComponent<SwordItem>();
                swordItem._isIn_YequalX_Zone = false;
            }
        }
    }
}
