using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem : ItemBlock
{
    [SerializeField] private float _movSpeed;
    [SerializeField] private bool _isDocked;
    public Sprite _ignitedImg;
    private Image _thisImg;
    private bool _isIgnited;
    public override eSpecialBlockType SpecialType
    {
        get { return _specialType; }
        set { _specialType = value; }
    }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    private void Start()
    {
        base.AddToMemoryList();
        MoveBlock(this.gameObject);
        _thisImg = GetComponent<Image>();
        SpecialType = eSpecialBlockType.Sword_Warrior;
    }

    public override void DoAction()
    {
        if (!_isIgnited)
        {
            ChangeImage();
            _isIgnited = true;
        }
        else
        {
            ItemAction();
            base.RemoveFromMemoryList();
            Destroy(gameObject);
        }
    }

    protected override void ChangeImage()
    {
        _thisImg.sprite = _ignitedImg;
    }

    protected override void ItemAction()
    {
        Debug.Log("Sword Item Action");
    }
}
