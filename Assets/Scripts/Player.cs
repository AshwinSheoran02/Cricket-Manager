using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum OutReason
{
    RunOut, Catch, Stump, LBW, catch_Bowl, Bowled
}
public class Player : MonoBehaviour
{
    [Header("Player Description")]
    public string PlayerRole;
    public string PlayerType;
    public string PlayerName;
    public string Hand;
    public string JerseyNum;
    public string Country;

    [Header("Player Skills")]
    public int Aggression;
    public int Consistency;
    public int Economy;
    public int Incisiveness;

    [Header("Player Stats")]
    public int totalRuns;
    public int totalMatches;
    public int Avg;
    public int Eco;
    public int totalWickets;
    public int Hundreds;
    public int Fifties;
    public int five_wicket_hawl;
    public int totalFours;
    public int totalSixes;

    [Header("Current Stats")]
    public int currentRuns;
    public int currentBowlsPlayed;
    public int currentWickets;
    public int currentBowlsBowled;
    public int currentMaidenOvers;
    public int currentRunsGiven;
    public int fours;
    public int sixes;

    [Header("Out Stats")]
    public bool isOut;
    public string bowlerName;
    public string fielderName;
    public OutReason reason;
    public bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
