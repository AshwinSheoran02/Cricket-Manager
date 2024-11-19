using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSquad : MonoBehaviour
{
    public static ShowSquad instance;
    [Header("UI Player Elements")]
    public Sprite BatsmanImage;
    public Sprite BowlerImage;
    public Sprite AllRounderImage;
    public Sprite WicketKeeperImage;
    public Scrollbar slide;
    public GameObject OnTheBench;
    GameObject bench = null;

    [Header("Team Attributes")]
    public Team PlayerTeam;
    public GameObject Player_View;
    public List<GameObject>allViews = new List<GameObject>();

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

    public void CreatePlayer(Player p,bool isPresent)
    {
        if (!isPresent)
        {
            GameObject gameObject = Instantiate(PlayerTeam.empty, PlayerTeam.transform);
            gameObject.AddComponent<Player>();
            Player player = gameObject.GetComponent<Player>(); 
            player.PlayerRole = p.PlayerRole;
            player.PlayerName = p.PlayerName;
            player.PlayerType = p.PlayerType;
            player.Hand = p.Hand;
            player.JerseyNum = p.JerseyNum;
            player.Country = p.Country;
            player.Aggression = p.Aggression;
            player.Consistency = p.Consistency;
            player.Economy = p.Economy;
            player.Incisiveness = p.Incisiveness;
            PlayerTeam.players.Add(gameObject);
            gameObject.name = player.PlayerName;
            return;
        }
        GameObject game = Instantiate(Player_View, content);
        Player_View view = game.GetComponent<Player_View>();
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
        allViews.Add(game);
    }

    public void Show()
    {
        int num = 0;
        foreach (GameObject pT in PlayerTeam.players)
        {
            if (num == 11)
            {
                bench = Instantiate(OnTheBench, content);
            }
            CreatePlayer(pT.GetComponent<Player>(),true);
            num++;
        }
    }

    public void Clear()
    {
        foreach(GameObject g in allViews)
        {
            Destroy(g);
        }
        if(bench != null)
        {
            Destroy(bench);
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
