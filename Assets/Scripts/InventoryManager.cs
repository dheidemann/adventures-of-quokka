using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> itemList = new();

    private EquipableItem weapon;
    private EquipableItem armor;

    public GameObject weaponField;
    public GameObject armorField;
    public Transform inventoryContent;
    public GameObject inventoryItemTemplate;
    public Sprite defaultIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item collectableItem)
    {
        itemList.Add(collectableItem);
    }

    public void Remove(Item collectableItem)
    {
        itemList.Remove(collectableItem);
    }

    public void Equip(EquipableItem collectableItem)
    {
        switch (collectableItem.type)
        {
            case EquipableType.Armor: 
                armor = collectableItem;
                break;
            case EquipableType.Weapon: 
                weapon = collectableItem;
                break;
        }            
        Remove(collectableItem);
    }

    public void Unequip(EquipableItem collectableItem)
    {
        switch (collectableItem.type)
        {
            case EquipableType.Armor: 
                armor = null;
                break;
            case EquipableType.Weapon: 
                weapon = null;
                break;
        }
        Add(collectableItem);
    }

    public void Drop(Item collectableItem)
    {
        Remove(collectableItem);
    }

    private void SetIconOn(GameObject inventoryItem, Sprite icon) 
    {
        var itemIcon = inventoryItem.transform.Find("ItemIcon").GetComponent<Image>();
        if (icon != null) 
        {
            itemIcon.sprite = icon;
        }
        else
        {
            itemIcon.sprite = defaultIcon;
        }
    }

    public void ListItems()
    {
        // clean inventory before opening
        // othewise the items multiply
        foreach (Transform collectableItem in inventoryContent)
        {
            Destroy(collectableItem.gameObject);
        }

        foreach (var collectableItem in itemList)
        {
            GameObject inventoryItem = Instantiate(inventoryItemTemplate, inventoryContent);
            SetIconOn(inventoryItem, collectableItem.icon);

            Button button = inventoryItem.GetComponent<Button>();
            button.onClick.AddListener(() => HandleItemClick(collectableItem));
        }
        
        SetIconOn(weaponField, weapon?.icon);
        SetIconOn(armorField, armor?.icon);
    }

    public void Consume(ConsumableItem collectableItem)
    {
        Player.Instance.IncreaseStat(StatType.health, collectableItem.heals);
        Remove(collectableItem);
    }

    public void HandleItemClick(Item clickedItem)
    {
        if (clickedItem is EquipableItem equipableItem)
        {
            Equip(equipableItem);
        }
        else if (clickedItem is ConsumableItem consumableItem)
        {
            Consume(consumableItem);
        }

        ListItems();
    }

}