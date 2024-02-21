using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


[TestFixture]
public class LevelsTests {
    
    [Test]
    public void TestDecreaseLevel(){
        Levels testLevel = new Levels(); 
        testLevel.SetLevel(2);
        testLevel.DecreaseLevel();
        Assert.AreEqual(testLevel.GetCurrentLevel(), 1); 
    }

    [Test]
    public void TestSetLevel(){
        Levels testLevel = new Levels(); 
        testLevel.SetLevel(3); 
        Assert.AreEqual(testLevel.GetCurrentLevel(), 3); 
    }

    [Test]
    public void TestIncreaseLevel(){
        Levels testLevel = new Levels(); 
        testLevel.IncreaseLevel(); 
        Assert.AreEqual(testLevel.GetCurrentLevel(), 2); 
    }

    [Test]
    public void TestGetCurrentStat(){
        Levels testLevel = new Levels(); 
        Assert.AreEqual(testLevel.GetCurrentStat(playerStats.maxHealth), 100);
    }

    [Test]
    public void TestLevels(){
        Levels testLevel = new Levels(); 
        Assert.AreNotEqual(null, testLevel.GetLevelStats());
        Assert.AreEqual(1, testLevel.GetCurrentLevel()); 
    }

    [Test]
    public void TestLevelMath(){
        Levels testLevel = new Levels(); 
        testLevel.IncreaseLevel(); 
        Assert.AreEqual(testLevel.GetCurrentStat(playerStats.maxHealth), 120); 
    }
   
}

