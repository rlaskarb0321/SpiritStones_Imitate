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
    // �Ϲ� ������
    Arrow_Archer,
    Bomb_Thief,
    Potion_Magician,
    Sword_Warrior,
    // ���� ������
    DoubleArrow_Archer,
    Dynamite_Thief,
    Elixir_Magician,
    DualSword_Warrior,
    None,
}

public enum eObstacleBlockType
{
    Skull,
}

public enum eBlockType_Refact
{
    Normal,
    Item,
    Obstacle,
    Count,
}

public enum eNormalBlockType_Refact
{
    Warrior,
    Archer,
    Thief,
    Magician,
    Count,
}

public enum eItemBlockType_Refact
{
    // �Ϲ� ������
    Arrow_Archer,
    Bomb_Thief,
    Potion_Magician,
    Sword_Warrior,
    NormalItemCount,
    // ���� ������
    DoubleArrow_Archer,
    Dynamite_Thief,
    Elixir_Magician,
    DualSword_Warrior,
    SpecialItemCount,
}

public enum eObstacleBlockType_Refact
{
    Skull,
}