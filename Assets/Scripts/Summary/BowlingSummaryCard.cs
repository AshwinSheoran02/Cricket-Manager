using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BowlingSummaryCard : MonoBehaviour
{
    public Player player;
    public TMP_Text bowlerName;
    public TMP_Text bowlingFigure;
    public Image backGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        { 
            bowlerName.enabled = false;
            bowlingFigure.enabled = false;    
            backGround.enabled = false;
        }
        else
        {
            bowlerName.enabled = true;
            bowlingFigure.enabled = true;
            backGround.enabled = true;
        }
        if(player != null)
        {
            bowlerName.text = player.PlayerName;
            int overs = player.currentBowlsBowled / 6;
            int balls = player.currentBowlsBowled % 6; 
            string figure = $"{player.currentRunsGiven}/{player.currentWickets} ({overs}.{balls})";
            bowlingFigure.text = figure;
        }
    }
}
