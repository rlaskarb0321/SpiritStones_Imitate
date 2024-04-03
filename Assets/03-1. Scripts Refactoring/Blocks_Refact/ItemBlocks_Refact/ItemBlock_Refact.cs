using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBlock_Refact : BlockBase_Refact
{
    [Header("=== 아이템 블록 ===")]
    [SerializeField] protected Sprite _ignitedImg;
    [SerializeField] protected bool _isIgnited;
    [SerializeField] protected bool _isSpecialItem;
    [SerializeField] protected float _actionDelay;

    /// <summary>
    /// 아이템 블록은 한번 이으면 점화가되고, 이 후 블록이 정리되면 효과가 발동
    /// </summary>
    protected abstract void IgniteItemBlock();

    /// <summary>
    /// 블록이 정리된 후 발동시키는 아이템 효과
    /// </summary>
    protected abstract IEnumerator DoItemAction();
}