using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    [SerializeField] private BlockBase _blockBase;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform != this.transform &&
            other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            _blockBase._isDocked = true;
            GameManager._instance._dockedCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform != this.transform &&
            other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            _blockBase._isDocked = false;
            GameManager._instance._dockedCount--;
        }
    }
}
