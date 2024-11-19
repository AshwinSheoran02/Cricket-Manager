using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    public static Summary instance;
    public TeamSummaryCard Card1;
    public TeamSummaryCard Card2;
    public TMP_Text Commentary;
    public bool isSecondInings = false;
    public int Team1runs;
    public int Team2runs;
    public int Team1wickets;
    public int Team2wickets;
    public int Team1balls;
    public int Team2balls;
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
        if(!isSecondInings && Card2.gameObject.activeInHierarchy)
        {
            Card2.gameObject.SetActive(false);
        }
        if(isSecondInings && !Card2.gameObject.activeInHierarchy)
        {
            Card2.gameObject.SetActive(true);
        }
        if (!isSecondInings)
        {
            Card1.Score.text = $"{Team1runs} - {Team1wickets} ({Team1balls / 6}.{Team1balls % 6})";
            Commentary.text = "Team1 play first";
        }
        else
        {
            Card2.Score.text = $"{Team2runs} - {Team2wickets} ({Team2balls / 6}.{Team2balls % 6})";
            if(Team1runs < Team2runs)
            {
                Commentary.text = "Team 2 won !!!";
            }
            else
            {
                Commentary.text = $"Need {Team1runs - Team2runs + 1} runs in {120 - Team2balls} balls to win";
            }
        }
    }
}
