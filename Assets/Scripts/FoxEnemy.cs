using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FoxEnemy : Enemy {

    public override void init()
    {
        maxHP = 50;
        currentHP = 50;
        attackRange = 1;
        damage = 5;
        followRange = 6;
        speed = 2;
        attackCooldown = 4;
    }
    
    //public override void DropLoot()
    //{}
}