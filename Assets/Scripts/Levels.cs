using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum playerStats
{
    maxHealth,
    maxFitness,
    numberAttacks,
    reach,
    size,
    timeToRegenerateFitness,
    timeToRegenerateFitnessAfterEmpty,
    moveSpeed,
    sprintSpeed,
    swimSpeed
}

public class Levels
{
    private int level; // saves current player Level
    private Dictionary<(int, playerStats), int> LevelStats; //saves Overview about Stats at different Levels

    
    public Levels(){
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

        /*
        LevelStats.Add((2, playerStats.maxHealth), 2);
        LevelStats.Add((2, playerStats.maxFitness), 2); 
        LevelStats.Add((2, playerStats.numberAttacks), 1); 
        LevelStats.Add((2, playerStats.reach), 2); 
        LevelStats.Add((2, playerStats.size), 2);
        LevelStats.Add((2, playerStats.timeToRegenerateFitness), 2);
        LevelStats.Add((2, playerStats.timeToRegenerateFitnessAfterEmpty), 3);*/
        foreach (playerStats stat in Enum.GetValues(typeof(playerStats)))
        {
           for(int i = 2; i <= 10; i++)
            {
                LevelStats.Add((i, stat), (int)(Math.Round(1.1 * i * getCurrentStat(stat))));
            }
        }
    }

    //Getter for playerStats
    public int getCurrentLevel(){
        return level; 
    }
    public int getCurrentStat(playerStats type)
    {
        return LevelStats[(level, type)];
    }
    /*public int getCurrentMaxLife(){
        return LevelStats[(level, "maxLife")];
    }

    public int getCurrentMaxFitness(){
        return LevelStats[(level, "maxFitness")];
    }

    public int getCurrentAttacks(){
        //TODO
        return LevelStats[(level, "numberAttacks")];
    }

    public int getCurrentReach(){
        return LevelStats[(level, "reach")];
    }

     public int getCurrentSize(){
        return LevelStats[(level, "size")];
    }*/

    //Manipulate Player Level
    //Increase Level 
    public void increaseLevel(){
        level++; 
    }

    //Decrease Level 
    public void decreaseLevel(){
        level--; 
    }

    public void setLevel(int i){
        level = i; 
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
