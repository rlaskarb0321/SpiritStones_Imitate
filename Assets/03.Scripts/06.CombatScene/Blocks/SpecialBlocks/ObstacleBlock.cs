using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    [SerializeField] private BlockBase _blockBase;

    void Start()
    {
    }

    public void DoAction()
    {
        _blockBase.DoObstacleBlockAction();
    }

    public void MoveBlock()
    {
        _blockBase.MoveBlock(this.gameObject);
    }
}
