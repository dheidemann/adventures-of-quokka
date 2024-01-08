using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public EquipableItem Weapon;
    public EquipableItem Armor;

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void Equip(EquipableItem item)
    {
        switch (item.type)
        {
            case EquipableType.Armor: 
                Armor = item;
                break;
            case EquipableType.Weapon: 
                Weapon = item;
                break;
        }            
        Remove(item);
    }

    public void Unequip(EquipableItem item)
    {
        switch (item.type)
        {
            case EquipableType.Armor: 
                Armor = null;
                break;
            case EquipableType.Weapon: 
                Weapon = null;
                break;
        }
        Add(item);
    }

    public void Drop(Item item)
    {}

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            ItemName.text = item.name;
            itemIcon.sprite = item.icon; 
        }
    }

    public void Consume(ConsumableItem item)
    {
        // Player.heal(item.heals)
        Remove(item);
    }

}