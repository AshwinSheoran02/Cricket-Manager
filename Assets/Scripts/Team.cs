using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List<GameObject>players = new List<GameObject>();
    public bool isPlayerTeam = false;
    public bool isMarketTeam = false;
    public GameObject empty;
    // Start is called before the first frame update
    void Awake()
    {
        if (isPlayerTeam)
        {
            ReadCSV.instance.LoadPlayerTeam();
            List<string[]> playerData = ReadCSV.instance.PlayerData;
            LoadPlayersTeam(playerData);
        }
        else if (isMarketTeam)
        {
            ReadCSV.instance.LoadMarketTeam();
            List<string[]> playerData = ReadCSV.instance.MarketData;
            LoadMarketTeam(playerData);
        }
        else
        {
            ReadCSV.instance.LoadOpponentTeam();
            List<string[]> playerData = ReadCSV.instance.OpponentData;
            LoadPlayersTeam(playerData);
        }
    }

    private void LoadPlayersTeam(List<string[]>playerData)
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            GameObject gameObject = Instantiate(empty, transform);
            gameObject.AddComponent<Player>();
            Player player = gameObject.GetComponent<Player>();
            player.PlayerRole = playerData[i][0];
            player.PlayerName = playerData[i][2];
            player.PlayerType = playerData[i][1];
            player.Hand = playerData[i][3];
            player.JerseyNum = playerData[i][4];
            player.Country = "IND";
            player.Aggression = int.Parse(playerData[i][5]);
            player.Consistency = int.Parse(playerData[i][6]);
            player.Economy = int.Parse(playerData[i][7]);
            player.Incisiveness = int.Parse(playerData[i][8]);
            players.Add(gameObject);
            gameObject.name = player.PlayerName;
        }
    }

    private void LoadMarketTeam(List<string[]> playerData)
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            GameObject gameObject = Instantiate(empty, transform);
            gameObject.AddComponent<Player>();
            Player player = gameObject.GetComponent<Player>();
            player.PlayerRole = playerData[i][0];
            player.PlayerName = playerData[i][2];
            player.PlayerType = playerData[i][1];
            player.Hand = playerData[i][3];
            player.JerseyNum = playerData[i][4];
            player.Country = playerData[i][5];
            player.Aggression = int.Parse(playerData[i][6]);
            player.Consistency = int.Parse(playerData[i][7]);
            player.Economy = int.Parse(playerData[i][8]);
            player.Incisiveness = int.Parse(playerData[i][9]);
            players.Add(gameObject);
            gameObject.name = player.PlayerName;
        }
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
