using UnityEngine;
using TMPro;

public class TeamSummaryCard : MonoBehaviour
{
    public BatsmanSummaryCard batsmanCard1;
    public BatsmanSummaryCard batsmanCard2;
    public BatsmanSummaryCard batsmanCard3;
    public BowlingSummaryCard bowlingCard;
    public TMP_Text TeamName;
    public TMP_Text Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBowlingCard(Player bowler)
    {
        if (bowlingCard.player == null || bowlingCard.player == bowler)
        {
            bowlingCard.player = bowler;
        }
        else if (bowlingCard.player != bowler)
        {
            float eco1 = bowlingCard.player.currentRunsGiven * 6f / bowlingCard.player.currentBowlsBowled;
            float eco2 = bowler.currentRunsGiven * 6f / bowler.currentBowlsBowled;
            if(bowler.currentWickets > bowlingCard.player.currentWickets)
            {
                bowlingCard.player = bowler;
            }
            else if(bowler.currentWickets == bowlingCard.player.currentWickets && eco1 > eco2)
            {
                bowlingCard.player = bowler;
            }
        }
    }

    public void UpdateCard(Player p1,Player p2,Player p3)
    {
        int runs1 = p1.currentRuns;
        int runs2 = p2.currentRuns;
        int runs3 = p3.currentRuns;
        int ball1 = p1.currentBowlsPlayed;
        int ball2 = p2.currentBowlsPlayed;
        int ball3 = p3.currentBowlsPlayed;
        if(runs1 < runs2 || (runs1 == runs2 && ball2 < ball1))
        {
            int run = runs1;
            int ball = ball1;
            batsmanCard1.player = p2;
            batsmanCard2.player = p1;
            p1 = batsmanCard1.player;
            p2 = batsmanCard2.player;
            runs1 = runs2;
            ball1 = ball2;
            runs2 = run;
            ball2 = ball;
        }
        if(runs1 < runs3 || (runs1 == runs3 && (ball3 < ball1)))
        {
            batsmanCard1.player = p3;
            batsmanCard3.player = p1;
            p3 = batsmanCard3.player;
            runs3 = runs1;
            ball3 = ball1;
        }
        if(runs2 < runs3 || (runs2 == runs3 && (ball3 < ball2)))
        {
            batsmanCard2.player = p3;
            batsmanCard3.player = p2;
        }
    }

    public void UpdateCard(Player p1, Player p2)
    {
        if(p1.currentRuns > p2.currentRuns)
        {
            return;
        }
        else if((p2.currentRuns == p1.currentRuns && p1.currentBowlsBowled > p2.currentBowlsBowled) || p2.currentRuns > p1.currentRuns)
        {
            batsmanCard1.player = p2;
            batsmanCard2.player = p1;
        }
    }

    public void BatsmanCards(Player Batsman)
    {
        bool Card1Free = batsmanCard1.player == null;
        bool Card2Free = batsmanCard2.player == null;
        bool Card3Free = batsmanCard3.player == null;
        Player p1 = batsmanCard1.player;
        Player p2 = batsmanCard2.player;
        Player p3 = batsmanCard3.player;

        if(p1 != null && p1 == Batsman)
        {
            if(p2 != null && p3 != null)
            {
                UpdateCard(p1, p2, p3);
            }
            else if(p2 != null)
            {
                UpdateCard(p1, p2);
            }
            return;
        }
        else if(p2 != null && p2 == Batsman)
        {
            if(p3 != null)
            {
                UpdateCard(p1, p2,p3);
            }
            return;
        }
        else if(p3 != null && p3 == Batsman)
        {
            UpdateCard(p1, p2, p3);
            return;
        }
        if(!Card1Free && !Card2Free && !Card3Free)
        {
            if(p3.currentRuns < Batsman.currentRuns)
            {
                p3 = Batsman;
                batsmanCard3.player = p3;
            }
            else if(p3.currentRuns ==  Batsman.currentRuns && Batsman.currentBowlsPlayed < p3.currentBowlsPlayed) {
                p3 = Batsman;
                batsmanCard3.player = p3;
            }
        }
        if (Card1Free)
        {
            batsmanCard1.player = Batsman;
            return;
        }
        else
        {
            p1 = batsmanCard1.player;
        }
        if(Card2Free)
        {
            batsmanCard2.player = Batsman;
            p2 = Batsman;
            UpdateCard(p1, p2);
            return;
        }
        else
        {
            p2 = batsmanCard2.player;
        }
        if (Card3Free)
        {
            batsmanCard3.player = Batsman;
            p3 = Batsman;
        }
        UpdateCard(p1, p2, p3);
    }
}
