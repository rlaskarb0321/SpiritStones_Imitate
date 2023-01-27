using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockBase : MonoBehaviour
{
    [SerializeField] protected float _movSpeed;
    [SerializeField] protected bool _isDocked;
    public virtual float MovSpeed { get; set; }
    public virtual eNormalBlockType NormalType { get; set; }
    public virtual eSpecialBlockType SpecialType { get; set; }
    public virtual bool IsDocked { get; set; }
    
    private void FixedUpdate()
    {
        MoveBlock(this.gameObject);
    }

    public virtual void DoAction() { }
    
    public virtual void AddToMemoryList()
    {
        GameManager._instance._blockMgrList.Add(this.gameObject);
    }

    public virtual void RemoveFromMemoryList()
    {
        GameManager._instance._blockMgrList.Remove(this.gameObject);
    }

    public virtual void MoveBlock(GameObject block)
    {
        if (IsDocked)
            return;

        block.transform.Translate(Vector2.down * MovSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            IsDocked = true;
            GameManager._instance._dockedCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlockBorder"))
        {
            IsDocked = false;
            GameManager._instance._dockedCount--;
        }
    }
}
