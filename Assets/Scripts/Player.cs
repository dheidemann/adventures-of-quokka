using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StatType
{
    fitness,
    health
}

public class Player : MonoBehaviour
{
    public static Player Instance;

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

    private void Awake()
    {
        Instance = this;
    }

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
        if (Input.GetKeyDown(KeyCode.E) && readToAttack)
        {
            StartCoroutine(Attack());

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
            DecreaseStat(StatType.fitness, 0.5f);
            sprinting = true;
        }
        else
        {
            rb.MovePosition(rb.position + movement * level.GetCurrentStat(playerStats.moveSpeed) * Time.fixedDeltaTime);
            sprinting = false;
            if (!regenerating && CheckFitness())
            {
                StartCoroutine(Regenerate(fitness, false));
            }
        }
    }

    public void IncreaseStat(StatType type, float value)
    {
        switch (type)
        {
            case StatType.fitness:
                if (fitness + value < level.GetCurrentStat(playerStats.maxFitness))
                {
                    fitness += value;
                }
                else
                {
                    fitness = level.GetCurrentStat(playerStats.maxFitness);           
                }
                break;
            case StatType.health:
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

    public void DecreaseStat(StatType type, float value)
    {
        switch (type)
        {
            case StatType.fitness:
                if ((int)(fitness - value) > 0)
                {
                    fitness -= value;
                }
                else
                {
                    fitness = 0;
                    StartCoroutine(Regenerate(fitness, true));
                }
                break;
            case StatType.health:
                animator.SetTrigger("hurtTrigger");
                if (health - value > 0)
                {
                    health -= value;

                }
                else
                {
                    health = 0;
                    Die();
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

    public void Die()
    {
        level.DecreaseLevel();
        SceneManager.LoadScene("deathScreen");
    }

    IEnumerator Regenerate(float currentFitness, bool empty)
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
            IncreaseStat(StatType.fitness, 0.04f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        regenerating = false;
    }
    IEnumerator Attack()
    {
        float localAttackCooldown = (5000 - level.GetCurrentStat(playerStats.attackCooldown)) / 1000;
        readToAttack = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, level.GetCurrentStat(playerStats.reach)/20f, raycastFilter);
        animator.SetTrigger("attackTrigger");
        GUIAnimator.SetFloat("cooldown", 1 / localAttackCooldown);
        GUIAnimator.SetTrigger("pressedTrigger");

        hit.collider?.gameObject.GetComponent<Enemy>().ReceiveDamage(level.GetCurrentStat(playerStats.damage) + extraDamage);
        yield return new WaitForSecondsRealtime(localAttackCooldown);
        print("ready to attack");
        readToAttack = true;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetFitness()
    {
        return fitness;
    }
}
