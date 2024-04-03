using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemDestroyType { Destory, None }

public abstract class ItemBlock_Refact : BlockBase_Refact
{
    [Header("=== ������ ��� ===")]
    [SerializeField] private ItemDestroyType _eDestroyType;
    [SerializeField] protected Sprite _ignitedImg;
    [SerializeField] protected bool _isIgnited;
    [SerializeField] protected bool _isSpecialItem;

    public ItemDestroyType DestroyType { get { return _eDestroyType; } }

    /// <summary>
    /// ������ ����� �ѹ� ������ ��ȭ���ǰ�, �� �� ����� �����Ǹ� ȿ���� �ߵ�
    /// </summary>
    protected abstract void IgniteItemBlock();
}