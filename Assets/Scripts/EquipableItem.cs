using UnityEngine;

public enum EquipableType
{
    Weapon,
    Armor
}

[CreateAssetMenu(fileName="New Equipable Item",menuName="Item/Equipable")]
public class EquipableItem : Item
{
    public int resists;
    public int damage;
    public EquipableType type;
}