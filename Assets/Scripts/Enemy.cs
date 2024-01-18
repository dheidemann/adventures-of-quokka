using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {
    public int id; 
    public int currentHP; 
    public int attackRange; 
    public bool attacked = false; 
    public int damage; 
    public GameObject player; 
    public int followRange; 
    public bool isFollowing = false; 
    public float speed; 
    public List<Item> drops; 
    public int attackCooldown; 
    


void Start() {
    player = GameObject.FindGameObjectWithTag("Player"); 
}

IEnumerator Attack(int attackCooldown, int damage) {
    player.GetComponent<player>().decreaseStat(statType.health, damage); 
    attacked = true;
    yield return new WaitForSecondsRealtime(attackCooldown);
    attacked = false;
}

public void Update(){
    // Wenn Distanz auf x & y Achse zu Player kleiner als followRange -> setze isFollowing auf true und ruf Methode followPlayer() auf
    if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) <= followRange) {
        isFollowing = true; 
        FollowPlayer();
    }
    // else isFollowing = false, da out of range; 
    else {
        isFollowing = false; 
    }
    //  Wenn Distanz auf x & y Achse zu Player kleiner als attackRange -> greif Player an
    if(Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) <= attackRange && attacked == false){
            StartCoroutine(Attack(attackCooldown, damage));
        }
}

public void FollowPlayer(){
    if (isFollowing == true){
        // beweg dich auf küzestem Weg zum Player
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed * Time.deltaTime);
    }
}

public void ReceiveDamage(int damage){
    if(damage >= currentHP){ 
       Die();
    }
    else {
        currentHP -= damage; 
    }
}

public void Die(){
    //TODO: Bluteffekt 
    DropLoot(); 
    Destroy(this);
}

public virtual void DropLoot(){}
    //TODO
}