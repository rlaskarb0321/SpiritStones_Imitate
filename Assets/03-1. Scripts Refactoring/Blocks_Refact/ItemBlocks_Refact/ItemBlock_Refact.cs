using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemDestroyType { Destory, None }

public abstract class ItemBlock_Refact : BlockBase_Refact
{
    [Header("=== 아이템 블록 ===")]
    [SerializeField] private ItemDestroyType _eDestroyType;
    [SerializeField] protected Sprite _ignitedImg;
    [SerializeField] protected bool _isIgnited;
    [SerializeField] protected bool _isSpecialItem;
    [SerializeField] protected float _delay;

    public ItemDestroyType DestroyType { get { return _eDestroyType; } }
    public bool IsIgnited { get { return _isIgnited; } }

    /// <summary>
    /// 아이템 블록은 한번 이으면 점화가되고, 이 후 블록이 정리되면 효과가 발동
    /// </summary>
    protected abstract void IgniteItemBlock();
}