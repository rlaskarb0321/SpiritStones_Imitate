using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBlock : BlockBase
{
    [SerializeField] public eObstacleBlockType _obstacleType;

    public virtual void AddToHarmfulBlockList()
    {
        GameManager._instance._obstacleBlockList.Add(this.gameObject);
    }

    public virtual void RemoveFromHarmfulBlockList()
    {
        GameManager._instance._obstacleBlockList.Remove(this.gameObject);
    }

    public virtual void DoHarmfulAction() { }
}
