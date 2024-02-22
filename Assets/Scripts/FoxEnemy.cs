using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FoxEnemy : Enemy {


    public void Start()
    {

        maxHP = 100;
        currentHP = 100;
        attackRange = 2;
        damage = 10;
        followRange = 8;
        speed = 3;
        attackCooldown = 2;

    }

    //public override void DropLoot()
    //{}
}