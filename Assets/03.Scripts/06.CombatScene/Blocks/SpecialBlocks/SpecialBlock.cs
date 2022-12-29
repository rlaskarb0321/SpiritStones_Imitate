using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSpecialBlockType
{
    // 일반 아이템
    Arrow_Archer,
    Bomb_Thief,
    Potion_Magician,
    Sword_Warrior,
    // 강한 아이템
    DoubleArrow_Archer,
    Dynamite_Thief,
    Elixir_Magician,
    DualSword_Warrior,
    None,
}

public abstract class SpecialBlock
{
    public abstract void DoAction();
}

public class ArrowItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Arrow Item Action");
    }
}

public class DoubleArrowItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("DoubleArrow Item Action");
    }
}

public class BombItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Bomb Item Action");
    }
}

public class DynamiteItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Dynamite Item Action");
    }
}

public class PotionItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Potion Item Action");
    }
}

public class ElixirItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Elixir Item Action");
    }
}

public class SwordItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("Sword Item Action");
    }
}

public class DualSwordItem : SpecialBlock
{
    public override void DoAction()
    {
        Debug.Log("DualSword Item Action");
    }
}