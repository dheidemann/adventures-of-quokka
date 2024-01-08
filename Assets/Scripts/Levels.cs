using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Levels : MonoBehaviour
{
    
    private int level; // saves current player Level
    private Dictionary<(int, string), int> LevelStats; //saves Overview about Stats at different Levels

    
    public Levels(){
        // set starting level to 1
        level = 1; 
        // fill Level Stats
        LevelStats.Add((1, "maxLife"), 1);
        LevelStats.Add((1, "maxFitness"), 1); 
        LevelStats.Add((1, "numberAttacks"), 0); 
        LevelStats.Add((1, "reach"), 1); 
        LevelStats.Add((1, "size"), 1); 

        LevelStats.Add((2, "maxLife"), 2);
        LevelStats.Add((2, "maxFitness"), 2); 
        LevelStats.Add((2, "newAttacks"), 1); 
        LevelStats.Add((2, "reach"), 2); 
        LevelStats.Add((2, "size"), 2); 
    }

    //Getter for stats
    public int getCurrentLevel(){
        return level; 
    }

    public int getCurrentMaxLife(){
        return LevelStats[(level, "maxLife")];
    }

    public int getCurrentMaxFitness(){
        return LevelStats[(level, "maxFitness")];
    }

    public int getCurrentAttacks(){
        //TODO
        return LevelStats[(level, "maxLife")];
    }

    public int getCurrentReach(){
        return LevelStats[(level, "reach")];
    }

     public int getCurrentSize(){
        return LevelStats[(level, "size")];
    }

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
