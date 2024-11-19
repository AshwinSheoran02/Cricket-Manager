using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BatsmanView : MonoBehaviour
{
    public Player player;
    public Image Jersey;
    public TMP_Text BatsmanName;
    public TMP_Text Score;
    public TMP_Text JerseyNum;
    public Slider bar;
    public bool striker = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            JerseyNum.text = player.JerseyNum;
            BatsmanName.text = player.PlayerName;
            if(striker )
            {
                Score.text = $"{player.currentRuns}({player.currentBowlsPlayed})*";
            }
            else
            {
                Score.text = $"{player.currentRuns}({player.currentBowlsPlayed})";
            }
        }
    }

    public int getAggression()
    {
        if(player == null)
        {
            return 0;
        }
        if (bar.value == 0.5f)
        {
            return player.Aggression;
        }
        else if (bar.value < 0.5f)
        {
            return Mathf.RoundToInt(player.Aggression - player.Aggression * (0.5f - bar.value) * 1.25f);
        }
        else
        {
            return Mathf.RoundToInt(player.Aggression + (100-player.Aggression) * (bar.value - 0.5f)*2f);
        }
    }

    public void AddAgression()
    {
        bar.value = Mathf.Clamp(bar.value+0.1f, 0.1f, 1f);
    }

    public void ReduceAggression()
    {
        bar.value = Mathf.Clamp(bar.value - 0.1f, 0.1f, 1f);
    }
}
