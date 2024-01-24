using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FoxEnemy : Enemy {

    public FoxEnemy(){
        currentHP = 10; 
        attackRange = 2; 
        damage = 3; 
        followRange = 5; 
        speed = 2; 
        attackCooldown = 2; 

    }

    //public virtual DropLoot(){

    //}
}