using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WolfEnemy : Enemy {

    public WolfEnemy(){
        currentHP = 15; 
        attackRange = 3; 
        damage = 4; 
        followRange = 7; 
        speed = 3; 
        attackCooldown = 3; 

    }

    public override void DropLoot(){
        
    }
}