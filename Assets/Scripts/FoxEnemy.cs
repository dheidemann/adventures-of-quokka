using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FoxEnemy : Enemy {


    public FoxEnemy(){
        currentHP = 50; 
        attackRange = 2; 
        damage = 10; 
        followRange = 8; 
        speed = 3; 
        attackCooldown = 2; 

    }

    //public override void DropLoot()
    //{}
}