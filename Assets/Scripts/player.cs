using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum statType
{
    fitness,
    health
}

public class player : MonoBehaviour
{
    public Image fitnessBar;

    public Image healthBar;

    private float fitness;

    private float health;

    private float extraDamage;

    private Levels level;

    private bool regenerating;

    private bool sprinting;

    private bool readToAttack;

    private Rigidbody2D rb;

    private Vector2 movement;

    private LayerMask raycastFilter;

    private Animator animator;

    private Animator GUIAnimator;


    void Start()
    {
        GUIAnimator = GameObject.FindGameObjectWithTag("GUI").GetComponent<Animator>();
        animator = GetComponent<Animator>();
        extraDamage = 0;
        raycastFilter = LayerMask.GetMask("enemy");
        rb = GetComponent<Rigidbody2D>();
        readToAttack = true;
        regenerating = false;
        sprinting = false;
        level = new Levels();
        fitness = level.GetCurrentStat(playerStats.maxFitness);
        health = level.GetCurrentStat(playerStats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
        if (Input.GetKeyDown(KeyCode.E) && readToAttack)
        {
            StartCoroutine(attack());

        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //keep UI up to date
        healthBar.fillAmount = health / level.GetCurrentStat(playerStats.maxHealth);
        fitnessBar.fillAmount = fitness / level.GetCurrentStat(playerStats.maxFitness);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && CheckFitness())
        {
            rb.MovePosition(rb.position + movement * level.GetCurrentStat(playerStats.sprintSpeed) * Time.fixedDeltaTime);
            decreaseStat(statType.fitness, 0.5f);
            sprinting = true;
        }
        else
        {
            rb.MovePosition(rb.position + movement * level.GetCurrentStat(playerStats.moveSpeed) * Time.fixedDeltaTime);
            sprinting = false;
            if (!regenerating && CheckFitness())
            {
                StartCoroutine(regenerate(fitness, false));
            }
        }
    }

    public void increaseStat(statType type, float value)
    {
        switch (type)
        {
            case statType.fitness:
                if (fitness + value < level.GetCurrentStat(playerStats.maxFitness))
                {
                    fitness += value;
                }
                else
                {
                    fitness = level.GetCurrentStat(playerStats.maxFitness);           
                }
                break;
            case statType.health:
                if (health + value < level.GetCurrentStat(playerStats.maxHealth))
                {
                    health += value;
                }
                else
                {
                    health = level.GetCurrentStat(playerStats.maxHealth);
                }
                break;
        }
    }

    public void decreaseStat(statType type, float value)
    {
        switch (type)
        {
            case statType.fitness:
                if ((int)(fitness - value) > 0)
                {
                    fitness -= value;
                }
                else
                {
                    fitness = 0;
                    StartCoroutine(regenerate(fitness, true));
                }
                break;
            case statType.health:
                animator.SetTrigger("hurtTrigger");
                if (health - value > 0)
                {
                    health -= value;

                }
                else
                {
                    health = 0;
                    die();
                }
                break;
        }
    }

    public void IncreaseExtraDamage(int dmg)
    {
        extraDamage += dmg;
    }
    public void DecreaseExtraDamage(int dmg)
    {
        extraDamage -= dmg;
    }

    public bool CheckFitness()
    {
        if ((int)fitness > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void die()
    {
        level.DecreaseLevel();
        SceneManager.LoadScene("deathScreen");
    }

    IEnumerator regenerate(float currentFitness, bool empty)
    {
        regenerating = true;
        if (empty)
        {
            yield return new WaitForSecondsRealtime(level.GetCurrentStat(playerStats.timeToRegenerateFitnessAfterEmpty) / 1000);
        }
        else
        {
            yield return new WaitForSecondsRealtime(level.GetCurrentStat(playerStats.timeToRegenerateFitness) / 1000);
        }
        while (fitness >= currentFitness && fitness < level.GetCurrentStat(playerStats.maxFitness) && !sprinting)
        {
            increaseStat(statType.fitness, 0.04f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        regenerating = false;
    }
    IEnumerator attack()
    {
        float localAttackCooldown = (5000 - level.GetCurrentStat(playerStats.attackCooldown)) / 1000;
        readToAttack = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, level.GetCurrentStat(playerStats.reach)/20f, raycastFilter);
        animator.SetTrigger("attackTrigger");
        GUIAnimator.SetFloat("cooldown", 1 / localAttackCooldown);
        GUIAnimator.SetTrigger("pressedTrigger");

        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(level.GetCurrentStat(playerStats.damage) + extraDamage);
        }
        yield return new WaitForSecondsRealtime(localAttackCooldown);
        print("ready to attack");
        readToAttack = true;
    }

    public float getHealth()
    {
        return health;
    }
    public float getFitness()
    {
        return fitness;
    }
}
