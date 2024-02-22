using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.VisualScripting;


[TestFixture]
public class EnemyTests
{
    
    [Test]
    public void TestFoxEnemyKonstruktor()
    {
        FoxEnemy foxy = new FoxEnemy();
        Assert.AreEqual(foxy.currentHP, 10); 
        Assert.AreEqual(foxy.attackRange, 2); 
        Assert.AreEqual(foxy.damage, 3); 
        Assert.AreEqual(foxy.followRange, 5); 
        Assert.AreEqual(foxy.speed, 2); 
        Assert.AreEqual(foxy.attackCooldown, 2); 
    }

    [Test]
    public void TestReceiveDamage() {
        FoxEnemy foxy = new FoxEnemy(); 
        foxy.ReceiveDamage(5); 
        Assert.AreEqual(foxy.currentHP, 5); 
        WolfEnemy wolf = new WolfEnemy(); 
        wolf.ReceiveDamage(5); 
        Assert.AreEqual(wolf.currentHP, 10); 
    }
    
    [Test]
    public void TestDie(){ 
        GameObject enemy = new GameObject();
        enemy.AddComponent<FoxEnemy>(); 
        //Problem: in Die() wird Destrory() aufgerufen, der compiler blockiert den Aufruf aus dem Editor Mode 
        //enemy.GetComponent<FoxEnemy>().Die(); 

        /*try {
            if (foxy == null){

            }
        } catch(Exception e)
        {
            if (e is System.NullReferenceException){
            // Null Exception -> Enemy is deleted
            Assert.IsTrue(1 == 1); 
            }
        }*/
    }

    [Test]
    public void TestDropLoot(){
        //TODO
    }

   
}
