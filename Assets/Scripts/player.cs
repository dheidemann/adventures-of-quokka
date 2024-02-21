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

    void Start()
    {
        extraDamage = 0;
        raycastFilter = LayerMask.GetMask("enemy");
        rb = GetComponent<Rigidbody2D>();
        readToAttack = true;
        regenerating = false;
        sprinting = false;
        level = new Levels();
        fitness = level.getCurrentStat(playerStats.maxFitness);
        health = level.getCurrentStat(playerStats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, movement * 5f, Color.red);
        if (Input.GetKeyDown(KeyCode.E) && readToAttack)
        {
            StartCoroutine(attack());

        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //keep UI up to date
        healthBar.fillAmount = health / level.getCurrentStat(playerStats.maxHealth);
        fitnessBar.fillAmount = fitness / level.getCurrentStat(playerStats.maxFitness);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && checkFitness())
        {
            rb.MovePosition(rb.position + movement * level.getCurrentStat(playerStats.sprintSpeed) * Time.fixedDeltaTime);
            decreaseStat(statType.fitness, 0.5f);
            sprinting = true;
            print(checkFitness());
        }
        else
        {
            rb.MovePosition(rb.position + movement * level.getCurrentStat(playerStats.moveSpeed) * Time.fixedDeltaTime);
            sprinting = false;
            if (!regenerating && checkFitness())
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
                if (fitness + value < level.getCurrentStat(playerStats.maxFitness))
                {
                    fitness += value;
                }
                else
                {
                    fitness = level.getCurrentStat(playerStats.maxFitness);           
                }
                break;
            case statType.health:
                if (health + value < level.getCurrentStat(playerStats.maxHealth))
                {
                    health += value;
                }
                else
                {
                    health = level.getCurrentStat(playerStats.maxHealth);
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
                    print("no fitness left");
                    StartCoroutine(regenerate(fitness, true));
                }
                break;
            case statType.health:
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

    public bool checkFitness()
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
        level.decreaseLevel();
        SceneManager.LoadScene("deathScreen");
    }

    IEnumerator regenerate(float currentFitness, bool empty)
    {
        regenerating = true;
        if (empty)
        {
            yield return new WaitForSecondsRealtime(level.getCurrentStat(playerStats.timeToRegenerateFitnessAfterEmpty) / 1000);
        }
        else
        {
            yield return new WaitForSecondsRealtime(level.getCurrentStat(playerStats.timeToRegenerateFitness) / 1000);
        }
        while (fitness >= currentFitness && fitness < level.getCurrentStat(playerStats.maxFitness) && !sprinting)
        {
            increaseStat(statType.fitness, 0.04f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        regenerating = false;
    }
    IEnumerator attack()
    {
        readToAttack = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 5f, raycastFilter);

        print(hit);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Enemy>().ReceiveDamage((int)(level.getCurrentStat(playerStats.damage) + extraDamage));
            print("damaged enemy" + (level.getCurrentStat(playerStats.damage) + extraDamage));
        }
        yield return new WaitForSecondsRealtime((5000 - level.getCurrentStat(playerStats.attackCooldown)) / 1000);
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
