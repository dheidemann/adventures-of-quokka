using System.Collections;
using System;
//using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


[TestFixture]
public class EnemyTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void testFoxEnemyKonstruktor()
    {
        // Use the Assert class to test conditions
        FoxEnemy foxy = new FoxEnemy();
        Assert.AreEqual(foxy.currentHP, 10); 
        Assert.AreEqual(foxy.attackRange, 2); 
        Assert.AreEqual(foxy.damage, 3); 
        Assert.AreEqual(foxy.followRange, 5); 
        Assert.AreEqual(foxy.speed, 2.5); 
        Assert.AreEqual(foxy.attackCooldown, 2); 
    }

    [Test]
    public void testReceiveDamage() {
        FoxEnemy foxy = new FoxEnemy(); 
        foxy.ReceiveDamage(5); 
        Assert.AreEqual(foxy.currentHP, 5); 
        WolfEnemy wolf = new WolfEnemy(); 
        wolf.ReceiveDamage(5); 
        Assert.AreEqual(wolf.currentHP, 10); 
    }

    [Test]
    public void testAttck(){
        FoxEnemy foxy = new FoxEnemy(); 
        //foxy.Start(); 
        //foxy.Attack();
    }

    [Test]
    public void testDecreaseLevel(){
        Levels testLevel = new Levels(); 
        testLevel.increaseLevel();
        testLevel.decreaseLevel();
        Assert.AreEqual(testLevel.getCurrentLevel(), 1); 
    }

    [Test]
    public void testSetLevel(){
        Levels testLevel = new Levels(); 
        testLevel.setLevel(3); 
        Assert.AreEqual(testLevel.getCurrentLevel(), 3); 
    }

   
}
