using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBlockType
{
    Normal,
    Item,
    Obstacle,
    Count,
}

public enum eNormalBlockType
{
    Warrior,
    Archer,
    Thief,
    Magician,
    None,
}

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