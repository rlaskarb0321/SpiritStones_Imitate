using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBlock : BlockBase
{
    public eSpecialBlockType _specialType;

    protected abstract void ChangeImage();

    protected abstract void ItemAction();
}
