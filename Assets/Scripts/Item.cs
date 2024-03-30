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