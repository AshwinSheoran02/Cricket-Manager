using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class BowlerView : MonoBehaviour
{
    public Player player;
    public Image Jersey;
    public TMP_Text BowlerName;
    public TMP_Text JerseyNum;
    public TMP_Text bowling_figure;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            JerseyNum.text = player.JerseyNum;
            BowlerName.text = player.PlayerName;
            bowling_figure.text = $"{player.currentWickets}-{player.currentRunsGiven} ({player.currentBowlsBowled / 6}.{player.currentBowlsBowled % 6})";
        }
    }
}
