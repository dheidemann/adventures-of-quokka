using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.VisualScripting;


[TestFixture]
public class PlayerTests
{
    
    [Test]
    public void TestIncreaseStat()
    {
        Player player = new Player();
        int tempFitness = player.fitness;
        player.IncreaseStat(StatType.fitness, 1);
        Assert.AreEqual(player.fitness, tempFitness+1);
    }

    [Test]
    public void TestDecreaseStat() {
        Player player = new Player();
        int tempHealth = player.health;
        player.DecreaseStat(StatType.health, 1);
        Assert.AreEqual(player.health, tempHealth-1);
    }
    
    [Test]
    public void TestCheckFitness(){ 
        Player player = new Player();
        Assert.IsTrue(player.CheckFitness());
        player.DecreaseStat(StatType.fitness, 100);
        Assert.IsFalse(player.CheckFitness());
    }

    [Test]
    public void TestRegenerate(){
        Player player = new Player();
        player.DecreaseStat(StatType.fitness, 90);
        Assert.AreEqual(player.fitness, 10);
        float tempFitness = player.fitness;
        Assert.IsTrue(tempFitness<=player.fitness);
        Assert.IsTrue(player.fitness < player.level.GetCurrentStat(playerStats.maxFitness));
        Assert.IsFalse(sprinting);
        player.StartCoroutine(Regenerate(player.fitness, false));
        Assert.AreEqual(player.fitness, 100);
        Assert.IsFalse(player.regenerating);
    }

   
}
