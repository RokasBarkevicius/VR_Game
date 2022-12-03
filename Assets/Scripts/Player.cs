using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string type = "";
    public bool ballPocketed8 = false;
    private List<int> ballsCollected = new List<int>();

    public Player(string name){
        Name = name;
    }

    public string Name{
        get;
        private set;
    }

    public int Points{
        get{
            return ballsCollected.Count;
        }
    }

    public void setType(string Type){
        type = Type;
    }

    public void Collect(int ballNumber){
        ballsCollected.Add(ballNumber);
    }

    public void pocketed8Ball(){
        ballPocketed8 = true;
    }
}
