using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BatsmanSummaryCard : MonoBehaviour
{
    public Player player;
    public TMP_Text batsmanName;
    public TMP_Text batsmanScore;
    public Image image;
    public Image backGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            batsmanName.enabled = false;
            batsmanScore.enabled = false;
            image.enabled = false;
            backGround.enabled = false;
        }
        else
        {
            batsmanName.enabled = true;
            batsmanScore.enabled = true;
            image.enabled = true;
            backGround.enabled = true;
        }
        if(player != null)
        {
            batsmanName.text = player.PlayerName;
            if (player.isOut)
            {
                batsmanScore.text = $"{player.currentRuns} ({player.currentBowlsPlayed})";
            }
            else
            {
                batsmanScore.text = $"{player.currentRuns} ({player.currentBowlsPlayed})*";
            }
        }
    }
}
