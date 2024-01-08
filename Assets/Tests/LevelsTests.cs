using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class LevelsTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void testLevelsKonstruktor()
    {
        // Use the Assert class to test conditions

        //TODO: Konstruktor,  getter, increaseLevel, decreaseLevel, 
        Levels testlevel = new Levels(); 
        ClassicAssert.NotNull(testLevel); 
        ClassicAssert.AreQual(testLevel.getCurrentLevel(), 1);
    }

    [Test]
    public void testGetLevelStats(){
        Levels testlevel = new Levels(); 
        
        Assert.AreEqual(testlevel.getCurrentMaxLife, 1);
        Assert.AreEqual(testlevel.getCurrentMaxFitness, 1);
        Assert.AreEqual(testlevel.getCurrentReach, 1); 
        Assert.AreEqual(testlevel.getCurrentSize, 1); 

    }

    [Test]
    public void testIncreaseLevel(){
        Levels testLevel = new Levels(); 
        testLevel.increaseLevel();
        Assert.AreEqual(testLevel.getCurrentLevel(), 2); 
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

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
