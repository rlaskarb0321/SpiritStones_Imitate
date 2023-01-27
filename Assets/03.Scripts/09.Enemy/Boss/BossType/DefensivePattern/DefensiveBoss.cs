using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveBoss : MonoBehaviour, IBossType
{
    public void Test()
    {
        Debug.Log(this.gameObject.name);
    }
}
