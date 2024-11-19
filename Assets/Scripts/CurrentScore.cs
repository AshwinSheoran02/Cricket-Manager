using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using System.Diagnostics;


public class CurrentScore : MonoBehaviour
{
    public GameObject Team1;
    public GameObject Team2;

    public BatsmanView Batsman1_View;
    public BatsmanView Batsman2_View;
    public BowlerView Bowler_View;

    private int batsmanIndex_1 = 0;
    private int batsmanIndex_2 = 1;
    private int currentBatsmen = 0;
    private int currentBowlerIndex = 0;

    private int maxOvers = 20;
    private int totalScore = 0;
    private int totalWickets = 0;

    private int totalBalls = 0;

    public TMP_Text runsText;
    public TMP_Text wicketsText;
    public TMP_Text oversText;

    public TextMeshProUGUI commentaryText;

    public TextMeshProUGUI[] previousResultsText; // Array to store the TextMeshProUGUI components of the previous results
    private List<string> previousResults = new List<string>();  // List to store the previous result

    public GameObject MainMenu;
    public GameObject Ground;

    public GameObject WinningScreen;
    public GameObject ScorecardPanel;
    public TMP_Text ScorecardText;
    public int Innings = 1;

    public TMP_Text TMP_AggressionBatsman1;
    public TMP_Text TMP_AggressionBatsman2;

    public TMP_Text TMP_WinText;

    public int targetScore = 0;


    void Start()
    {

    }
    public void StartSimulation()
    {
        Ground.SetActive(true);
        MainMenu.SetActive(false);
        List<GameObject> Batsmen = new List<GameObject>();
        List<GameObject> Bowlers = new List<GameObject>();
        List<GameObject> Fielders = new List<GameObject>();
        Player WicketKeeper = null;
        for (int i = 0; i < 11; i++)
        {
            GameObject player = Team1.GetComponent<Team>().players[i];
            Batsmen.Add(player);
        }
        for(int i = 0;i < 11; i++)
        {
            GameObject player = Team2.GetComponent<Team>().players[i];
            Fielders.Add(player);
            if (player.GetComponent<Player>().PlayerRole == "Wicketkeeper")
            {
                WicketKeeper = player.GetComponent<Player>();
            }
        }
        for (int i = 6; i < 11; i++)
        {
            GameObject player = Team2.transform.GetComponent<Team>().players[i];
            Bowlers.Add(player);
        }
        UpdateUI();
        StartCoroutine(SimulateMatch(Batsmen, Bowlers, WicketKeeper,Fielders));
        StartCoroutine(createInstance(Batsmen,Bowlers));
    }

    IEnumerator createInstance(List<GameObject> Batting,List<GameObject>Bowling)
    {
        while (ShowStats.instance == null)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ShowStats.instance.CreateStats(Batting,Bowling);
    }

    void Update()
    {
        if (Innings == 2)
        {
            Innings++;
            List<GameObject> Batsmen = new List<GameObject>();
            List<GameObject> Bowlers = new List<GameObject>();
            List<GameObject> Fielders = new List<GameObject>();
            Player WicketKeeper = null;
            for (int i = 0; i < 11; i++)
            {
                GameObject player = Team2.GetComponent<Team>().players[i];
                Batsmen.Add(player);
            }
            for (int i = 10; i >= 0; i--)
            {
                GameObject player = Team1.GetComponent<Team>().players[i];
                Fielders.Add(player);
                if (player.GetComponent<Player>().PlayerRole == "Wicketkeeper")
                {
                    WicketKeeper = player.GetComponent<Player>();
                }
            }
            targetScore  = totalScore + 1;
            Debug.Log("Target Score is " + targetScore);
            totalScore = 0;
            totalWickets = 0;
            totalBalls = 0;
            currentBowlerIndex = 0;
            batsmanIndex_1 = 0;
            batsmanIndex_2 = 1;
            currentBatsmen = 0;
            for (int i = 6; i < 11; i++)
            {
                GameObject player = Team1.transform.GetComponent<Team>().players[i];
                Bowlers.Add(player);
            }
            ShowStats.instance.ClearStats();
            Summary.instance.isSecondInings = true;
            StartCoroutine(createInstance(Batsmen, Bowlers));
            StartCoroutine(SimulateMatch(Batsmen, Bowlers, WicketKeeper, Fielders));
        }
    }

    public BatsmanView getCurrentBatsman()
    {
        if(currentBatsmen == batsmanIndex_1)
        {
            return Batsman1_View;
        }
        return Batsman2_View;
    }

    public void UpdateOutIndex(List<GameObject>Batsmen)
    {
        int nextIndex = Mathf.Max(batsmanIndex_1 + 1, batsmanIndex_2 + 1) % 11;
        if (currentBatsmen == batsmanIndex_1)
        {
            batsmanIndex_1 = nextIndex;
            Batsman1_View.bar.value = 0.5f;
            Batsman1_View.player = Batsmen[batsmanIndex_1].GetComponent<Player>();
            Batsman1_View.player.isPlaying = true;
        }
        else
        {
            batsmanIndex_2 = nextIndex;
            Batsman2_View.bar.value = 0.5f;
            Batsman2_View.player = Batsmen[batsmanIndex_2].GetComponent<Player>();
            Batsman2_View.player.isPlaying = true;
        }
        currentBatsmen = nextIndex;
    }

    public void UpdateStrikerEnd()
    {
        if(currentBatsmen == batsmanIndex_1)
        {
            Batsman1_View.striker = true;
            Batsman2_View.striker = false;
        }
        else
        {
            Batsman2_View.striker = true;
            Batsman1_View.striker = false;
        }
    }
    public void Run()
    {
        if(currentBatsmen == batsmanIndex_1)
        {
            currentBatsmen = batsmanIndex_2;
        }
        else
        {
            currentBatsmen = batsmanIndex_1;
        }
    }

    public void outCommentary(Player P,List<GameObject>Fielders,Player Keeper,bool isWicket,int runs)
    {
        if (isWicket)
        {
            int randomIndex = Random.Range(0, 6);
            switch (randomIndex)
            {
                case 0:
                    DisplayRandomCommentary(commentaryForCatch);
                    P.fielderName = Fielders[Random.Range(0, 11)].GetComponent<Player>().PlayerName;
                    P.reason = OutReason.Catch;
                    break;
                case 1:
                    DisplayRandomCommentary(commentaryForRunOut);
                    P.fielderName = Fielders[Random.Range(0, 11)].GetComponent<Player>().PlayerName;
                    P.reason = OutReason.RunOut;
                    break;
                case 2:
                    DisplayRandomCommentary(commentaryForStump);
                    P.fielderName = Keeper.PlayerName;
                    P.reason = OutReason.Stump;
                    break;
                case 3:
                    DisplayRandomCommentary(commentaryForLBW);
                    P.fielderName = "None";
                    P.reason = OutReason.LBW;
                    break;
                case 4:
                    DisplayRandomCommentary(commentaryForCatchBowl);
                    P.fielderName = P.bowlerName;
                    P.reason = OutReason.catch_Bowl;
                    break;
                case 5:
                    DisplayRandomCommentary(commentaryForBowled);
                    P.fielderName = "None";
                    P.reason = OutReason.Bowled;
                    break;
                default:
                    DisplayRandomCommentary(commentaryForBowled);
                    P.fielderName = "None";
                    P.reason = OutReason.Bowled;
                    break;
            }
        }
        else
        {
            switch (runs)
            {
                case 0:
                    DisplayRandomCommentary(commentaryFor0Runs);
                    break;
                case 1:
                    DisplayRandomCommentary(commentaryFor1Run);
                    break;
                case 2:
                    DisplayRandomCommentary(commentaryFor2Runs);
                    break;
                case 3:
                    DisplayRandomCommentary(commentaryFor3Runs);
                    break;
                case 4:
                    DisplayRandomCommentary(commentaryFor4Runs);
                    break;
                case 5:
                    DisplayRandomCommentary(commentaryFor5Runs);
                    break;
                case 6:
                    DisplayRandomCommentary(commentaryFor6Runs);
                    break;
            }
        }
    }
    IEnumerator SimulateMatch(List<GameObject> Batsmen, List<GameObject> Bowlers, Player Keeper,List<GameObject>Fielders)
    {
        yield return new WaitForSeconds(1f);
        ShowStats.instance.UpdateStats();
        for (int j = 0; j < maxOvers; j++) // 20 overs match
        {
            UpdateUI();
            int overScore = 0;
            int overWickets = 0;
            List<string> overResults = new List<string>(); // To store the result of each ball in the over
            for (int i = 0; i < 6; i++) // 6 balls per over
            {
                // Get the current batsman and bowler
                Batsman1_View.player = Batsmen[batsmanIndex_1].GetComponent<Player>();
                Batsman2_View.player = Batsmen[batsmanIndex_2].GetComponent<Player>();
                Batsman1_View.player.isPlaying = true;
                Batsman2_View.player.isPlaying = true;
                UpdateStrikerEnd();
                Bowler_View.player = Bowlers[currentBowlerIndex].GetComponent<Player>();
                ShowStats.instance.UpdateStats();
                string outcome = SimulateBowl(Bowler_View, getCurrentBatsman());
                overResults.Add(outcome); // Store the outcome of each ball
                if (outcome == "Wicket")
                {
                    overWickets++;
                    totalWickets++;
                    Player P = getCurrentBatsman().player;
                    P.isOut = true;
                    P.bowlerName = Bowler_View.player.PlayerName;
                    outCommentary(P, Fielders, Keeper,true,0);
                    UpdateOutIndex(Batsmen);
                    ShowStats.instance.UpdateStats();
                }
                else
                {
                    int runs = int.Parse(outcome);
                    overScore += runs;
                    totalScore += runs;
                    outCommentary(null, Fielders, Keeper, false, runs);
                    previousResults.Insert(0, outcome == "Wicket" ? "W" : outcome);
                    if (previousResults.Count > 6)
                    {
                        previousResults.RemoveAt(previousResults.Count - 1);
                    }
                    for (int k = 0; k < previousResultsText.Length; k++)
                    {
                        if (k < previousResults.Count)
                        {
                            string displayOutcome = previousResults[k] == "Wicket" ? "W" : previousResults[k];
                            previousResultsText[k].text = displayOutcome;
                        }
                        else
                        {
                            previousResultsText[k].text = "";
                        }
                    }

                    if (runs == 1 || runs == 3)
                    {
                        Run();
                    }
                    if (i == 5)
                    {
                        // Bowler Selection
                        Run();
                        currentBowlerIndex = (currentBowlerIndex + 1) % Bowlers.Count;
                    }
                    if (totalWickets == Batsmen.Count || totalWickets == 10 || (j == maxOvers - 1 && i == 6))
                    {
                        if (Innings > 1)
                        {
                            StartCoroutine(checkForWinOnLastBall());
                            yield break;
                        }
                        ShowScorecard(Batsmen, Bowlers);
                        yield return new WaitForSeconds(15f);
                        Innings = 2;
                        ScorecardPanel.SetActive(false);
                        yield break;
                    }
                }
                if (Innings > 1  && totalScore >= targetScore)
                {
                    Ground.SetActive(false);
                    WinningScreen.SetActive(true);
                    yield return new WaitForSeconds(5f);
                    Debug.Log("Team 1 Lost !!!!!!!!!");
                    TMP_WinText.text = "Team 1 Lost";
                    MainMenu.SetActive(true);
                    yield break;
                }
                if (Innings == 2 && totalScore == targetScore-1)
                {
                    Ground.SetActive(false);
                    WinningScreen.SetActive(true);
                    yield return new WaitForSeconds(5f);
                    Debug.Log("Match Tied !!!!!!!!!");
                    TMP_WinText.text = "Match Tied";
                    MainMenu.SetActive(true);
                    yield break;
                }
                UpdateUI();
                yield return new WaitForSeconds(1f);
            }
            if ( Innings > 1 &&  ( totalWickets == Batsmen.Count || totalWickets == 9 || (j == maxOvers - 1 )) )
            {
                Ground.SetActive(false);
                WinningScreen.SetActive(true);
                yield return new WaitForSeconds(5f);
                Debug.Log("Team 1 Won !!!!!!!!!");
                TMP_WinText.text = "Team 1 Won";
                MainMenu.SetActive(true);        
                yield break;
            }
            if (Innings > 1 && totalScore >= targetScore)
            {
                Ground.SetActive(false);
                WinningScreen.SetActive(true);
                yield return new WaitForSeconds(5f);
                Debug.Log("Team 1 Lost !!!!!!!!!");
                TMP_WinText.text = "Team 1 Lost";
                MainMenu.SetActive(true);
                yield break;
            }
        }
        if (Innings > 1 &&  ( totalWickets == Batsmen.Count || totalWickets == 9 ) )
        {
            Ground.SetActive(false);
            WinningScreen.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Team 1 Won !!!!!!!!!");
            TMP_WinText.text = "Team 1 Won";
            MainMenu.SetActive(true);                     
            yield break;
        }
        if (Innings > 1 &&  (totalScore == targetScore -1  ) )
        {
            Ground.SetActive(false);
            WinningScreen.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Match Tied !!!!!!!!!");
            TMP_WinText.text = "Match Tied";
            MainMenu.SetActive(true);                     
            yield break;
        }
        Debug.Log("Showing Scorecard in 3 2 1 ");
        ShowScorecard(Batsmen, Bowlers);
        yield return new WaitForSeconds(15f);
        Innings = 2;
        ScorecardPanel.SetActive(false);
        yield break;
    }

    IEnumerator checkForWinOnLastBall()
    {
        if (totalScore < targetScore - 1)
        {
            Ground.SetActive(false);
            WinningScreen.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Team 1 Won !!!!!!!!!");
            TMP_WinText.text = "Team 1 Won";
            MainMenu.SetActive(true);
        }
        else if (totalScore == targetScore - 1)
        {
            Ground.SetActive(false);
            WinningScreen.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Match Tied !!!!!!!!!");
            TMP_WinText.text = "Match Tied";
            MainMenu.SetActive(true);
        }
        else
        {
            Ground.SetActive(false);
            WinningScreen.SetActive(true);
            yield return new WaitForSeconds(5f);
            Debug.Log("Team 1 Lost !!!!!!!!!");
            TMP_WinText.text = "Team 1 Lost";
            MainMenu.SetActive(true);
        }
    }


    void UpdateUI()
    {
        if(Innings == 1)
        {
            Summary.instance.Team1balls = totalBalls;
            Summary.instance.Team1wickets = totalWickets;
            Summary.instance.Team1runs = totalScore;
        }
        else
        {
            Summary.instance.Team2balls = totalBalls;
            Summary.instance.Team2wickets = totalWickets;
            Summary.instance.Team2runs = totalScore;
        }
        runsText.text = "" + totalScore;
        wicketsText.text = "" + totalWickets;
        oversText.text = (totalBalls / 6).ToString("0") + "." + (totalBalls % 6);
        TMP_AggressionBatsman1.text = $"Aggression: {Batsman1_View.getAggression()}";
        TMP_AggressionBatsman2.text = $"Aggression: {Batsman2_View.getAggression()}";
    }

    public string SimulateBowl(BowlerView bowler, BatsmanView batsman)
    {
        totalBalls++;
        bowler.player.currentBowlsBowled++;
        batsman.player.currentBowlsPlayed++;
        // Extract attributes and scale them to 0-1 range
        float bowlerEconomy = bowler.player.Economy / 100f;
        float bowlerIncisiveness = bowler.player.Incisiveness / 100f;
        float batsmanAggression = batsman.getAggression()/100;
        float batsmanConsistency = batsman.player.Consistency / 100f;

        // Adjusted factors for target score and wickets based on pitch conditions
        float targetScoreFactor = 3f;
        float targetHighScoreFactor = 5f;
        float targetWicketsFactor = 0.25f;

        // Calculate probabilities
        float wicketProbability = (0.25f * bowlerIncisiveness) + (0.5f * batsmanAggression) - (0.25f * batsmanConsistency);
        float boundaryProbability = (0.3f * batsmanAggression) - (0.35f * bowlerEconomy);
        float runProbability = (0.2f * batsmanConsistency) - (0.25f * bowlerEconomy);

        // Adjust probabilities based on target factors
        wicketProbability *= targetWicketsFactor;
        boundaryProbability *= targetHighScoreFactor;
        runProbability *= targetScoreFactor;

        // Debug.Log(wicketProbability);
        // Determine ball outcome
        float outcome = Random.Range(0f, 1f);
        // Debug.Log("Wicket " + wicketProbability);
        // Debug.Log("Boundary " + boundaryProbability);
        // Debug.Log("Outcome " + outcome);

        if (outcome < wicketProbability)
        {
            // Debug.Log("Wicket!!!!!!!!!!!!");
            bowler.player.currentWickets++;
            UpdateSummary(batsman.player, bowler.player);
            return "Wicket";            
        }
        else if (outcome < wicketProbability + boundaryProbability)
        {
            int boundaryScored = (Random.Range(0, 2) * 2 + 4);
            bowler.player.currentRunsGiven += boundaryScored;
            batsman.player.currentRuns += boundaryScored;
            UpdateSummary(batsman.player, bowler.player);
            return boundaryScored.ToString(); // Return a random number between 4 and 6 as a string for boundary
        }
        else
        {   
            int runsScored = Random.Range(0, 3);
            bowler.player.currentRunsGiven += runsScored;
            batsman.player.currentRuns += runsScored;
            UpdateSummary(batsman.player, bowler.player);
            return runsScored.ToString(); // Return a random number between 0 and 3 as a string for runs
        }
    }

    public void UpdateSummary(Player batsman,Player bowler)
    {
        if (!Summary.instance.isSecondInings)
        {
            Summary.instance.Card1.BatsmanCards(batsman);
            Summary.instance.Card1.UpdateBowlingCard(bowler);
        }
        else
        {
            Summary.instance.Card2.BatsmanCards(batsman);
            Summary.instance.Card2.UpdateBowlingCard(bowler);
        }
    }

    public void DisplayRandomCommentary(List<string> commentaryList)
    {
        if (commentaryList.Count > 0)
        {
            int randomIndex = Random.Range(0, commentaryList.Count);
            commentaryText.text = commentaryList[randomIndex];
        }
    }


    public void ShowScorecard(List<GameObject> Batsmen, List<GameObject> Bowlers)
    {
        //Debug.Log("Showing Scorecard");
        string batsmenScorecard = "Batsmen Scorecard\n\n";
        for (int i = 0; i < Batsmen.Count; i++)
        {
            Player batsman = Batsmen[i].GetComponent<Player>();
            string playerName = batsman.PlayerName;
            int runs = batsman.currentRuns;
            int ballsFaced = batsman.currentBowlsPlayed;

            float strikeRate = (ballsFaced > 0) ? ((float)runs / ballsFaced) * 100 : 0;

            batsmenScorecard += $"{playerName}: {runs}  ({ballsFaced} ), S/R : {strikeRate.ToString("0.00")}\n";
        }

        // Populate bowlers scorecard information
        string bowlersScorecard = "\nBowlers Scorecard\n\n";
        for (int i = 0; i < Bowlers.Count; i++)
        {

            Player bowler = Bowlers[i].GetComponent<Player>();
            string bowlerName = bowler.PlayerName;
            int wickets = bowler.currentWickets;
            int runsGiven = bowler.currentRunsGiven;
            int oversBowled = bowler.currentBowlsBowled / 6;
            int ballsBowled = bowler.currentBowlsBowled % 6;

            float economy = (oversBowled > 0) ? (float)runsGiven / oversBowled : 0;

            bowlersScorecard += $"{bowlerName}: W: {wickets} , R: {runsGiven} , O: {oversBowled}.{ballsBowled} , Eco: {economy.ToString("0.00")}\n";
        }

        // Combine batsmen and bowlers scorecards
        string scorecard = $"{batsmenScorecard}\n{bowlersScorecard}";

        // Display the total score
        scorecard += $"\nTotal Score: {totalScore}/{totalWickets} in {totalBalls / 6}.{totalBalls % 6} overs\n";

        // Set the scorecard text
        ScorecardText.text = scorecard;

        // Show the scorecard panel
        ScorecardPanel.SetActive(true);
    }

    // Commentary lines for different run outcomes
    public List<string> commentaryFor0Runs = new List<string>
    {
        "Driven straight to the extra cover. No runs.",
        "Defended back to the bowler. No runs.",
        "Played back to the bowler. No runs taken.",
        "Straight to the fielder. Dot ball.",
        "Well fielded by the bowler. No runs.",
        "Missed the shot. No runs scored.",
        "Solid defense. No runs taken."
    };

    public List<string> commentaryFor1Run = new List<string>
    {
        "Drive deep down to deep mid-wicket. One run taken.",
        "Pushed to the off-side. Quick single taken.",
        "Guided down to third man. One run scored.",
        "Drove through the covers. Single taken.",
        "Placed into the gap. One run.",
        "Quick run. Batsmen cross for a single.",
        "Worked into the leg side. One run taken."
    };

    public List<string> commentaryFor2Runs = new List<string>
    {
        "Excellent running between the wickets. Two runs taken.",
        "Played to deep square leg. Batsmen run hard for two.",
        "Pushed into the gap. Quick two runs.",
        "Drove to the off-side. Batsmen complete a comfortable double.",
        "Well-placed shot. Two runs taken.",
        "Guided to third man. Two runs scored.",
        "Quickly played into the gap. Two runs taken."
    };

    public List<string> commentaryFor3Runs = new List<string>
    {
        "Beautifully placed. Three runs taken.",
        "Driven through the covers. Batsmen run well for three.",
        "Pushed into the deep. Running hard, they get three.",
        "Guided behind point. Three runs scored.",
        "Excellent placement. Three runs taken.",
        "Drove to the mid-off region. Three runs scored.",
        "Quick running between the wickets. Three runs taken."
    };

    public List<string> commentaryFor4Runs = new List<string>
    {
        "Smashed over the bowler! Four runs.",
        "Over the covers. Four runs!",
        "Driven through the gap. Boundary!",
        "Worked down to fine leg. Four!",
        "Cracked through the off-side. Four runs.",
        "Pulled away to the square leg boundary. Four runs.",
        "Superb timing. Four runs scored."
    };

    public List<string> commentaryFor5Runs = new List<string>
    {
        "A run and overthrow. Five runs!",
        "Misfield in the outfield. Batsmen take five.",
        "Quick running. Misfield allows five runs.",
        "Misplayed in the field. Five runs taken.",
        "Overthrow from the deep. Five runs scored.",
        "Fielder fumbles, allowing five runs.",
        "Miscommunication in the field. Five runs taken."
    };

    public List<string> commentaryFor6Runs = new List<string>
    {
        "Smashed over the cover! Six runs.",
        "Over long-on. Maximum! Six runs.",
        "Launched over the mid-wicket boundary. Six!",
        "Driven straight back over the bowler. Six runs.",
        "Pulled away into the stands. Maximum!",
        "Lofted over extra cover. Six runs scored.",
        "Swept into the crowd. Six runs taken."
    };

    public List<string> commentaryForRunOut = new List<string>
    {
        // Run Out commentary
        "Run out! Direct hit. Batsman falls short.",
        "Miscommunication in the running. Run out!",
        // Add more run-out commentaries as needed
    };

    public List<string> commentaryForCatch = new List<string>
    {
        // Catch commentary
        "Caught at first slip. Wicket taken!",
        "Caught in the outfield. Excellent catch!",
        "One-handed stunner! Batsman departs.",
        "Diving catch at mid-wicket. Sensational!",
        "Caught by the wicketkeeper. Top-class take!",
        "Aerial catch by the fielder. Brilliant!",
        // Add more catch commentaries as needed
    };

    public List<string> commentaryForStump = new List<string>
    {
        // Stump commentary
        "Stumped! Excellent work by the wicketkeeper.",
        "Missed the shot. Quick stumping!",
        // Add more stump commentaries as needed
    };

    public List<string> commentaryForLBW = new List<string>
    {
        // LBW commentary
        "LBW! Umpire raises the finger.",
        "Trapped in front. LBW appeal successful!",
        // Add more LBW commentaries as needed
    };

    public List<string> commentaryForCatchBowl = new List<string>
    {
        // Catch and Bowl commentary
        "Caught and bowled! Brilliant reflexes!",
        "Driven straight back. Caught by the bowler!",
        "Caught low by the bowler. Unbelievable!",
        "Full-length dive by the bowler. Catch taken!",
        "Quick reaction catch by the bowler. Wicket!",
        "One-handed return catch. Spectacular!",
        // Add more catch-and-bowl commentaries as needed
    };

    public List<string> commentaryForBowled = new List<string>
    {
        // Bowled commentary
        "Bowled him! Timber!",
        "Clean bowled! Stumps shattered.",
        "Middle stump knocked out of the ground!",
        "Swinging delivery. Bails go flying!",
        "Yorker delivery. Bowled all ends up!",
        "Off stump sent cartwheeling. What a delivery!",
        // Add more bowled commentaries as needed
    };
}