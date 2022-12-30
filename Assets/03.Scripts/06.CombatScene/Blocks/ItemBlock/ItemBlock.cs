using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBlock : BlockBase
{
    public eSpecialBlockType _specialType;
    [SerializeField] protected Sprite _ignitedImg;
    protected Image _thisImg;
    protected bool _isIgnited;

    protected abstract void ChangeImage();

    protected abstract IEnumerator ItemAction();
}
