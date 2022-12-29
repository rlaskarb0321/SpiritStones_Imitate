using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTouchObstacle_2 : IUserinterfaceObstacle
{
    public void SetObstacle(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetOffObstacle(GameObject obj)
    {
        obj.SetActive(false);
    }
}
