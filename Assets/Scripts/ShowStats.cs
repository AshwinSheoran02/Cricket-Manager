using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour
{
    public static ShowStats instance;
    public Transform Battingcontent;
    public Transform Bowlingcontent;
    public GameObject battingView;
    public GameObject bowlingView;
    List<GameObject> battingViews = new List<GameObject>();
    List<GameObject> bowlingViews = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateStats(List<GameObject>BattingList,List<GameObject>BowlingList)
    {
        foreach(GameObject p in BattingList)
        {
            GameObject gameObject = Instantiate(battingView, Battingcontent);
            battingViews.Add(gameObject);
            BattingStatsView battingStatsView = gameObject.GetComponent<BattingStatsView>();
            battingStatsView.player = p.GetComponent<Player>();
        }
        foreach(GameObject p in BowlingList)
        {
            GameObject gameObject = Instantiate(bowlingView, Bowlingcontent);
            bowlingViews.Add(gameObject);
            BowlingStatsView battingStatsView = gameObject.GetComponent<BowlingStatsView>();
            battingStatsView.player = p.GetComponent<Player>();
        }
        UpdateStats();
    }

    public void UpdateStats()
    {
        foreach(GameObject g in battingViews)
        {
            BattingStatsView view = g.GetComponent<BattingStatsView>();
            Player p = view.player;
            if (!p.isPlaying)
            {
                view.Name.text = p.PlayerName;
                view.outReason.enabled = false;
                view.Runs.enabled = false;
                view.Balls.enabled = false;
                view.Fours.enabled = false;
                view.Sixes.enabled = false;
                view.StrikeRate.enabled = false;
            }
            else
            {
                view.outReason.enabled = true;
                view.Runs.enabled = true;
                view.Balls.enabled = true;
                view.Fours.enabled = true;
                view.StrikeRate.enabled = true;
                view.Sixes.enabled = true;

                view.Runs.text = p.currentRuns.ToString();
                view.Balls.text = p.currentBowlsPlayed.ToString();
                view.Fours.text = p.fours.ToString();
                view.Sixes.text = p.sixes.ToString();
                if(p.currentBowlsPlayed == 0)
                {
                    view.StrikeRate.text = "0";
                }
                else
                {
                    view.StrikeRate.text = ((p.currentRuns * 100f) / p.currentBowlsPlayed).ToString("0.00");
                }
                if (p.isOut)
                {
                    if (p.fielderName == p.bowlerName)
                    {
                        p.reason = OutReason.catch_Bowl;
                    }
                    if (p.reason == OutReason.Stump)
                    {
                        view.outReason.text = "st " + p.fielderName + " b " + p.bowlerName;
                    }
                    else if(p.reason == OutReason.RunOut)
                    {
                        view.outReason.text = "run out " + p.fielderName;
                    }
                    else if(p.reason == OutReason.Bowled)
                    {
                        view.outReason.text = "b " + p.bowlerName;
                    }
                    else if(p.reason == OutReason.Catch)
                    {
                        view.outReason.text = "c " + p.fielderName + " b " + p.bowlerName;
                    }
                    else if(p.reason == OutReason.catch_Bowl)
                    {
                        view.outReason.text = "c & b " + p.bowlerName;
                    }
                    else
                    {
                        view.outReason.text = "lbw " + p.bowlerName;
                    }
                }
                else
                {
                    view.outReason.text = "Not Out";
                }
            }
        }
        foreach(GameObject g in bowlingViews)
        {
            BowlingStatsView view = g.GetComponent<BowlingStatsView>();
            Player p = view.player;
            view.Name.text = p.PlayerName;
            view.Runs.text = p.currentRunsGiven.ToString("0");
            int balls = p.currentBowlsBowled % 6;
            int over = p.currentBowlsBowled / 6;
            float add = balls / 10f;
            if(over + add == 0)
            {
                view.Economy.text = "0";
            }
            else
            {
                view.Economy.text = (p.currentRunsGiven / (over + add)).ToString("0.00");
            }
            view.Maidens.text = p.currentMaidenOvers.ToString("0");
            view.Wickets.text = p.currentWickets.ToString("0");
            if(add == 0)
            {
                view.Overs.text = over.ToString("0");
            }
            else
            {
                view.Overs.text = (over + add).ToString("0.0");
            }
        }
    }

    public void ClearStats()
    {
        foreach(GameObject p in battingViews)
        {
            Destroy(p);
        }
        foreach (GameObject p in bowlingViews)
        {
            Destroy(p);
        }
        battingViews.Clear();
        bowlingViews.Clear();
    }
}
