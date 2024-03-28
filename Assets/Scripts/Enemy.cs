    using UnityEngine; 
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Enemy : MonoBehaviour {

        public float maxHP;
        public int id;
        public float currentHP;
        public int attackRange;
        public bool attacked = false;
        public int damage;
        public GameObject player;
        public int followRange;
        public bool isFollowing = false;
        public float speed;
        public List<Item> drops;
        public int attackCooldown; 
        public ParticleSystem deathEffect;
        public GameObject healthBar;
        public Animator animator;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        init();
    }

    IEnumerator Attack(int attackCooldown, int damage) {
        animator.SetTrigger("attackTrigger");
        player.GetComponent<Player>().DecreaseStat(StatType.health, damage); 
        attacked = true;
        yield return new WaitForSecondsRealtime(attackCooldown);
        attacked = false;
    }

    public abstract void init();

    public void Update(){
        // Wenn Distanz auf x & y Achse zu Player kleiner als followRange -> setze isFollowing auf true und ruf Methode followPlayer() auf
        if (CheckRange(followRange)) {
            isFollowing = true; 
            FollowPlayer();

            // Wenn Distanz außerdem kleiner ist als die attackRange & der Gegner noch nicht angegriffen oder der attackCooldown abgelaufen ist,  greif Spieler an 
            if(CheckRange(attackRange) && attacked == false){
                StartCoroutine(Attack(attackCooldown, damage));
            }
        }
        // else isFollowing = false, da out of range; 
        else {
            isFollowing = false; 
        }
    }

    //Vergleicht die Distanz zwischen Spieler und Enenmy mit der eingegeben Range zb attackRange oder followRange 
    public bool CheckRange(int range){
        if(Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) <= range) {
            return true; 
        }
        return false; 
    }

    public void FollowPlayer(){
        if (isFollowing == true){
            // beweg dich auf küzestem Weg zum Player
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -6), speed * Time.deltaTime);
        }
    }

    public void ReceiveDamage(float damage){

        animator.SetTrigger("hurtTrigger");

        if(damage >= currentHP){ 
           Die();
        }
        else {
            currentHP -= damage;
            healthBar.GetComponent<enemyHealthBar>().SetHealth(currentHP / maxHP);
        }
    }

    public void Die(){
        // Enemy dies, with particle effect, dropping loot and destroying the game object  
        ParticleSystem instantiatedEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        instantiatedEffect.Play();
        DropLoot(); 
        Destroy(this.gameObject);
    }

    public virtual void DropLoot(){}
        //drop loot
    }
