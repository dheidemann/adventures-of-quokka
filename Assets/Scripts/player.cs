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

    private Levels level;

    private bool regenerating;

    private bool sprinting;

    private Rigidbody2D rb;

    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        regenerating = false;
        sprinting = false;
        level = new Levels();
        fitness = level.getCurrentStat(playerStats.maxFitness);
        health = level.getCurrentStat(playerStats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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
                    fitnessBar.fillAmount = fitness / level.getCurrentStat(playerStats.maxFitness);
                }
                else
                {
                    fitness = level.getCurrentStat(playerStats.maxFitness);
                    fitnessBar.fillAmount = fitness / level.getCurrentStat(playerStats.maxFitness);
                }
                break;
            case statType.health:
                if (health + value < level.getCurrentStat(playerStats.maxHealth))
                {
                    health += value;
                    healthBar.fillAmount = health / level.getCurrentStat(playerStats.maxHealth);
                }
                else
                {
                    health = level.getCurrentStat(playerStats.maxHealth);
                    healthBar.fillAmount = health / level.getCurrentStat(playerStats.maxHealth);
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
                    fitnessBar.fillAmount = fitness / level.getCurrentStat(playerStats.maxFitness);
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
                    healthBar.fillAmount = health / level.getCurrentStat(playerStats.maxHealth);

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
        if((int)fitness > 0)
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
}
