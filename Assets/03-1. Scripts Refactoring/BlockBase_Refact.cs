using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBase_Refact : MonoBehaviour
{
    [SerializeField] protected GameObject _highlightParticle;
    [SerializeField] protected float _movSpeed;
    [SerializeField] private eBlockType_Refact _blockType;
    private bool _isDocked;

    public eBlockType_Refact BlockType { get { return _blockType; } }

    private void FixedUpdate()
    {
        if (_isDocked)
            return;

        MoveBlock(Vector2.down);
    }

    public void MoveBlock(Vector2 dir)
    {
        transform.Translate(dir * _movSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
            _isDocked = true;

        GameFlowMgr_Refact._instance.DockedCount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
            _isDocked = false;

        GameFlowMgr_Refact._instance.DockedCount++;
    }
}