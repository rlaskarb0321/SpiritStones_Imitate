using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowItem_2 : ItemBlock_2
{
    [SerializeField] private float _movSpeed;
    [SerializeField] private bool _isDocked;
    public Sprite _ignitedImg;
    private Image _thisImg;
    private bool _isIgnited;
    public GameObject _arrowPrefabs;
    [SerializeField] private int _arrowCount;

    public override eSpecialBlockType SpecialType 
    { 
        get { return _specialType; }
        set { _specialType = value; }
    }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    public override void DoAction()
    {
        if (!_isIgnited)
        {
            ChangeImg();
            _isIgnited = true; 
        }
        else
        {
            Debug.Log("Arrow Item Action");
            base.RemoveFromMemoryList();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        base.AddToMemoryList();
        MoveBlock(this.gameObject);
        _thisImg = GetComponent<Image>();
        SpecialType = eSpecialBlockType.Arrow_Archer;
    }

    void ChangeImg()
    {
        _thisImg.sprite = _ignitedImg;
    }
}
