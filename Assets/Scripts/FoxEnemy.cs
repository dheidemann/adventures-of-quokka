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

<<<<<<< HEAD
    //public virtual DropLoot(){
=======
    public override void DropLoot(){
>>>>>>> 67dcd085b278d355e8d8bb5e5ec4727e4b2d8708

    //}
}