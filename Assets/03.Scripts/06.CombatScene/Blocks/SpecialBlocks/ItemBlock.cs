using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    [SerializeField] private BlockBase _blockBase;
    [SerializeField] private eSpecialBlockType _itemType;
    public SpecialBlock _specialBlock;
    public eSpecialBlockType ItemBlockType
    {
        get { return _itemType; }
        set { _itemType = value; }
    }


    void Start()
    {
        _itemType = _blockBase._specialType;

        switch (_itemType)
        {
            case eSpecialBlockType.Arrow_Archer:
                _specialBlock = new ArrowItem();
                break;

            case eSpecialBlockType.DoubleArrow_Archer:
                _specialBlock = new DoubleArrowItem();
                break;

            case eSpecialBlockType.Bomb_Thief:
                _specialBlock = new BombItem();
                break;

            case eSpecialBlockType.Dynamite_Thief:
                _specialBlock = new DynamiteItem();
                break;

            case eSpecialBlockType.Potion_Magician:
                _specialBlock = new PotionItem();
                break;

            case eSpecialBlockType.Elixir_Magician:
                _specialBlock = new ElixirItem();
                break;

            case eSpecialBlockType.Sword_Warrior:
                _specialBlock = new SwordItem();
                break;

            case eSpecialBlockType.DualSword_Warrior:
                _specialBlock = new DualSwordItem();
                break;
        }
    }

    public void DoAction()
    {
        _blockBase.DoItemBlockAction(_specialBlock);

        GameManager._instance._blockMgrList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void MoveBlock()
    {
        _blockBase.MoveBlock(this.gameObject);
    }
}
