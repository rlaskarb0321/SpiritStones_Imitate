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
        StartCoroutine(MovePosition(block));
    }

    IEnumerator MovePosition(GameObject block)
    {
        yield return new WaitUntil(() => !IsDocked); // !_isDocked 이면 실행

        // 동작 구현
        block.transform.Translate(Vector2.down * MovSpeed * Time.deltaTime);

        StartCoroutine(MovePosition(block));
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
