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
    [Header("=== ��� ��ݰ� ===")]
    [SerializeField] protected float _movSpeed;
    [SerializeField] private eBlockHeroType_Refact _eBlockHeroType;
    [SerializeField] private eBlockType_Refact _eBlockType;
    [SerializeField] private bool _isDocked;
    [SerializeField] protected GameObject _selectionParticle;

    public eBlockHeroType_Refact BlockHeroType { get { return _eBlockHeroType; } }
    public eBlockType_Refact BlockType { get { return _eBlockType; } }

    /// <summary>
    /// �ش� ����� �Ѻ� �׸��⿡ �־��� �� ���� ���� �޼���
    /// </summary>
    public abstract void DoBreakAction();

    /// <summary>
    /// �ش� ����� ���������� ����Ʈ ���� �޼���
    /// </summary>
    /// <param name="turnOn"></param>
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