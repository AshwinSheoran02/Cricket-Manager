using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    public static ReadCSV instance;
    public List<string[]> MarketData = new List<string[]>();
    public List<string[]> PlayerData = new List<string[]>();
    public List<string[]> OpponentData = new List<string[]>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadPlayerTeam()
    {
        TextAsset csvFilePath = Resources.Load<TextAsset>("PlayerTeam");
        using (StringReader reader = new StringReader(csvFilePath.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { 
                string[] row = line.Split(',');
                PlayerData.Add(row);
            }
        }
    }

    public void LoadOpponentTeam()
    {
        TextAsset csvFilePath = Resources.Load<TextAsset>("OpponentTeam");
        using (StringReader reader = new StringReader(csvFilePath.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                OpponentData.Add(row);
            }
        }
    }

    public void LoadMarketTeam()
    {
        TextAsset csvFilePath = Resources.Load<TextAsset>("Market");
        using (StringReader reader = new StringReader(csvFilePath.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                MarketData.Add(row);
            }
        }
    }
}
