using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMarket : MonoBehaviour
{
    public static ShowMarket instance;
    [Header("UI Player Elements")]
    public Sprite BatsmanImage;
    public Sprite BowlerImage;
    public Sprite AllRounderImage;
    public Sprite WicketKeeperImage;
    public Scrollbar slide;


    [Header("Team Attributes")]
    public Team MarketTeam;
    public GameObject Player_View;
    public List<GameObject> allViews = new List<GameObject>();

    [Header("Transform")]
    public Transform content;
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

    public void Show()
    {
        foreach (GameObject pT in MarketTeam.players)
        {
            GameObject game = Instantiate(Player_View, content);
            Player_View view = game.GetComponent<Player_View>();
            Player p = pT.GetComponent<Player>();
            if (p.PlayerRole == "Wicketkeeper")
            {
                view.role.sprite = WicketKeeperImage;
            }
            else if (p.PlayerRole == "Batsman")
            {
                view.role.sprite = BatsmanImage;
            }
            else if (p.PlayerRole == "Bowler")
            {
                view.role.sprite = BowlerImage;
            }
            else
            {
                view.role.sprite = AllRounderImage;
            }
            view.p = p;
            view.Name.text = p.PlayerName;
            // float ov = p.Aggression / 4f + p.Consistency / 4f + p.Incisiveness / 4f + p.Economy / 4f;
            float ov = CalculateOverallScore(p);
            view.ovr.text = ov.ToString("0");
            view.hand.text = p.Hand;
            view.JerseyNum.text = p.JerseyNum;
            view.country.text = p.Country;
            allViews.Add(game);
        }
    }

    public void Clear()
    {
        foreach (GameObject g in allViews)
        {
            Destroy(g);
        }
        allViews.Clear();
    }

    private float CalculateOverallScore(Player p)
    {
        float ov;

        if (p.PlayerRole == "Batsman" || p.PlayerRole == "Wicketkeeper")
        {
            // For batsman and wicket keeper: ovr = (consistency + aggression) / 2
            ov = (p.Consistency + p.Aggression) / 2f;
        }
        else if (p.PlayerRole == "Bowler")
        {
            // For bowler: ovr = (incisiveness + economy) / 2
            ov = (p.Incisiveness + p.Economy) / 2f;
        }
        else
        {
            // For all-rounder: ovr = (aggression + consistency + incisiveness + economy) / 4
            ov = (p.Aggression + p.Consistency + p.Incisiveness + p.Economy) / 4f;
        }

        return ov;
    }
}
