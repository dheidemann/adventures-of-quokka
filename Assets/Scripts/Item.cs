using System;
using UnityEngine;

[CreateAssetMenu(fileName="New Collectable Item",menuName="Item/Collectable")]
public class Item : ScriptableObject 
{
    public int id;
    public string itemName;
    public Sprite icon;
    public bool collected = false;
}

public enum EquipableType
{
    Weapon,
    Armor
}

[CreateAssetMenu(fileName="New Equipable Item",menuName="Item/Equipable")]
public class EquipableItem : Item
{
    public int resists;
    public EquipableType type;
}

[CreateAssetMenu(fileName="New Consumable Item",menuName="Item/Consumable")]
public class ConsumableItem : Item
{
    public int heals;
}