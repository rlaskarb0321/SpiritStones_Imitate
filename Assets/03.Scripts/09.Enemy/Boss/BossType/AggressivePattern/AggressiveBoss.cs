using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBoss : MonoBehaviour, IBossType
{
    public void Test()
    {
        Debug.Log(this.gameObject.name);
    }
}
