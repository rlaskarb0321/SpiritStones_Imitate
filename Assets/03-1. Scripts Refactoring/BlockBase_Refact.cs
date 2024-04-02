using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BlockBase_Refact : MonoBehaviour
{
    [SerializeField] protected GameObject _highlightParticle;
    [SerializeField] protected float _movSpeed;
    [SerializeField] private eBlockHeroType_Refact _eBlockType;
    private bool _isDocked;

    public eBlockHeroType_Refact BlockType { get { return _eBlockType; } }

    public abstract void DoBreakAction();

    protected virtual void FixedUpdate()
    {
        if (_isDocked)
            return;

        MoveBlock(Vector2.down);
    }

    protected virtual void MoveBlock(Vector2 dir)
    {
        transform.Translate(dir * _movSpeed * Time.fixedDeltaTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
            _isDocked = true;

        GameFlowMgr_Refact._instance.DockedCount++;
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
            _isDocked = false;

        GameFlowMgr_Refact._instance.DockedCount++;
    }
}