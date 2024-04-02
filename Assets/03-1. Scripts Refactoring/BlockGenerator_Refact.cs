using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator_Refact : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int _itemBlockSpawnPercentage;
    [SerializeField] private GameObject[] _normalBlockPrefabArr;
    [SerializeField] private GameObject[] _itemBlockPrefabArr;

    public void GenerateBlock(Vector2 pos, Transform parent)
    {
        int randomValue = Random.Range(0, (int)BlockHeroType_Refact.Count);
        Instantiate(_normalBlockPrefabArr[randomValue], pos, Quaternion.identity, parent);
    }
}
