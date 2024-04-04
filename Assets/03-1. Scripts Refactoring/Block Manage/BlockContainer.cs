using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockContainer : MonoBehaviour
{
    [SerializeField] private Transform[] _colum;

    public GameObject GetRandomBlock()
    {
        int randomColum = Random.Range(0, _colum.Length);
        int randomIndex = Random.Range(0, 5);

        return _colum[randomColum].GetChild(randomIndex).gameObject;
    }
}
