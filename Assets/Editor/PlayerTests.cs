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
        player.testStart();
        player.DecreaseStat(StatType.fitness, 10);
        float tempFitness = player.GetFitness();
        player.IncreaseStat(StatType.fitness, 1);
        Assert.AreEqual(player.GetFitness(), tempFitness+1);
    }

    [Test]
    public void TestDecreaseStat() {
        Player player = new Player();
        player.testStart();
        float tempHealth = player.GetHealth();
        player.DecreaseStat(StatType.health, 1);
        Assert.AreEqual(player.GetHealth(), tempHealth-1);
    }

    [Test]
    public void TestRegenerate(){
        Player player = new Player();
        player.testStart();
        player.DecreaseStat(StatType.fitness, 90);
        Assert.AreEqual(player.GetFitness(), 10);
        float tempFitness = player.GetFitness();
        Assert.IsTrue(tempFitness<=player.GetFitness());
        Assert.IsTrue(player.GetFitness() < player.GetLevel().GetCurrentStat(playerStats.maxFitness));
        Assert.IsFalse(player.IsSprinting());
        player.startRegenerate(false);
        Assert.AreEqual(player.GetFitness(), 100);
        Assert.IsFalse(player.IsRegenerating());
    }

   
}
