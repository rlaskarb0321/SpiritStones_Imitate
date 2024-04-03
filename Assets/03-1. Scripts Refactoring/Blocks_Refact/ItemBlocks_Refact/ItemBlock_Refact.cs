using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBlock_Refact : BlockBase_Refact
{
    [Header("=== ������ ��� ===")]
    [SerializeField] protected Sprite _ignitedImg;
    [SerializeField] protected bool _isIgnited;
    [SerializeField] protected bool _isSpecialItem;
    [SerializeField] protected float _actionDelay;

    /// <summary>
    /// ������ ����� �ѹ� ������ ��ȭ���ǰ�, �� �� ����� �����Ǹ� ȿ���� �ߵ�
    /// </summary>
    protected abstract void IgniteItemBlock();

    /// <summary>
    /// ����� ������ �� �ߵ���Ű�� ������ ȿ��
    /// </summary>
    protected abstract IEnumerator DoItemAction();
}