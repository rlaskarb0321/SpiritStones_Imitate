using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockSelectionImg
{
    UnSelect,
    Select,
}

public abstract class BlockBase_Refact : MonoBehaviour
{
    [Header("=== 블록 기반값 ===")]
    [SerializeField] protected float _movSpeed;
    [SerializeField] private BlockHeroType_Refact _eBlockHeroType;
    [SerializeField] private BlockType_Refact _eBlockType;
    [SerializeField] private bool _isDocked;
    [SerializeField] protected GameObject _selectionParticle;

    public BlockHeroType_Refact BlockHeroType { get { return _eBlockHeroType; } }
    public BlockType_Refact BlockType { get { return _eBlockType; } }

    public abstract void DoBreakAction();

    public abstract void ActivateBlockSeletionUI(bool turnOn);

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

        GameFlowMgr_Refact._instance.DockedCount--;
    }
}