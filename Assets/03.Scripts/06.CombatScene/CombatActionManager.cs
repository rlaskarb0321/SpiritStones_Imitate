using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionManager : MonoBehaviour
{
    public Queue<eGameFlow> _gameFlowQueue;
    public List<GameObject> _queueSubscriber;

    private void Start()
    {
        _gameFlowQueue = new Queue<eGameFlow>();
        ReFillQueue();
    }

    public void ReFillQueue()
    {
        foreach (eGameFlow state in Enum.GetValues(typeof(eGameFlow)))
        {
            _gameFlowQueue.Enqueue(state);
        }
    }
}
