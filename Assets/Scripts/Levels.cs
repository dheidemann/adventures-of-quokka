using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum playerStats
{
    // implementiert Req 2.2.3.1.1 Enum playerStats
    maxHealth,
    maxFitness,
    numberAttacks,
    reach,
    size,
    timeToRegenerateFitness,
    timeToRegenerateFitnessAfterEmpty,
    moveSpeed,
    sprintSpeed,
    swimSpeed,
    damage,
    attackCooldown
}

public class Levels
{
    //implementiert Req 2.2.3.1.2 Klasse Levels
    private int level; // saves current player Level
    private Dictionary<(int, playerStats), int> LevelStats; //saves Overview about Stats at different Levels


    public Levels()
    {
        // set starting level to 1
        level = 1;
        //initialize levelstats
        LevelStats = new Dictionary<(int, playerStats), int>();
        // fill Level Stats
        LevelStats.Add((1, playerStats.maxHealth), 100);
        LevelStats.Add((1, playerStats.maxFitness), 100);
        LevelStats.Add((1, playerStats.numberAttacks), 0);
        LevelStats.Add((1, playerStats.reach), 100);
        LevelStats.Add((1, playerStats.size), 100);
        LevelStats.Add((1, playerStats.timeToRegenerateFitness), 2000);
        LevelStats.Add((1, playerStats.timeToRegenerateFitnessAfterEmpty), 4000);
        LevelStats.Add((1, playerStats.moveSpeed), 5);
        LevelStats.Add((1, playerStats.sprintSpeed), 8);
        LevelStats.Add((1, playerStats.swimSpeed), 10);
        LevelStats.Add((1, playerStats.damage), 15);
        LevelStats.Add((1, playerStats.attackCooldown), 2000);


        foreach (playerStats stat in Enum.GetValues(typeof(playerStats)))
        {
            for (int i = 2; i <= 10; i++)
            {
                LevelStats.Add((i, stat), (int)(Math.Round(((0.1 * i) + 1) * GetCurrentStat(stat))));
            }
        }
    }

    //Getter for playerStats
    public int GetCurrentLevel()
    {
        return level;
    }
    public int GetCurrentStat(playerStats type)
    {
        return LevelStats[(level, type)];
    }
    public Dictionary<(int, playerStats), int> GetLevelStats(){
        return LevelStats; 
    }

    //Manipulate Player Level
    //Increase Level 
    public void IncreaseLevel()
    {
        // check if level is in allowed range if increased, if yes level is increased by 1
        if (level < 10){
            level++;
        }
        else {
            Debug.Log("Level can not be increased, as Level is already 10"); 
        }
    }

    //Decrease Level 
    public void DecreaseLevel()
    {
        //check if level is in allowed range if decreased, if yes level is decreased by 1
        if(level > 1){
            level--;
        }
        else {
            Debug.Log("Level can not be decreased, as Level is already 1");
        }
    }

    public void SetLevel(int i)
    {
        //check if input is in allowed range, if yes level is updated
        if(i > 0 && i < 11){
            level = i; 
        }
        else {
            Debug.Log("Level can not be set to input parameter, as it is out of allowed range"); 
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
