using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void InventoryTestsSimplePasses()
    {
        InventoryManager inventory = new InventoryManager();
        Item item = new Item();
        inventory.Add(item);
        ClassicAssert.AreEqual(inventory.items.Length, 1);

        EquipableItem weapon = new EquipableItem();
        weapon.type = EquipableType.Weapon;
        inventory.Add(weapon);
        ClassicAssert.AreEqual(inventory.Items.Length, 2);
        inventory.Equip(weapon);
        ClassicAssert.AreEqual(inventory.items.Length, 1);
        ClassicAssert.AreEqual(inventory.Weapon, weapon);
        inventory.Unequip(weapon);
        ClassicAssert.AreEqual(inventory.Items.Length, 2);
        ClassicAssert.AreEqual(inventory.Weapon, null);

        ConsumableItem fruit = new ConsumableItem();
        inventory.Add(fruit);
        ClassicAssert.AreEqual(inventory.Items.Length, 3);
        inventory.Consume(fruit);
        ClassicAssert.AreEqual(inventory.Items.Length, 2);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InventoryTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
