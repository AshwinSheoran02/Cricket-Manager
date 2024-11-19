using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_View : MonoBehaviour
{
    public Image role;
    public TMP_Text Name;
    public TMP_Text hand;
    public TMP_Text ovr;
    public TMP_Text JerseyNum;
    public TMP_Text country;
    public bool isMarketView;
    public Player p;
    public bool isSelected;
    public Image swapImage;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            swapImage.rectTransform.Rotate(new Vector3(0, 0, swapImage.rectTransform.rotation.z + rotationSpeed * Time.deltaTime));
        }
    }

    public int findIdx()
    {
        int idx = -1;
        List<GameObject> view = ShowSquad.instance.allViews;
        for (int i = 0; i < view.Count; i++)
        {
            if (view[i] == this.gameObject)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    public void downPosition()
    {
        List<GameObject> view = ShowSquad.instance.allViews;
        int idx = findIdx();
        if (idx == view.Count-1)
        {
            return;
        }
        List<GameObject> players = ShowSquad.instance.PlayerTeam.players;
        GameObject g = players[idx + 1];
        players[idx + 1] = players[idx];
        players[idx] = g;
        ShowSquad.instance.Clear();
        ShowSquad.instance.Show();
    }

    public void upPosition() {
        int idx = findIdx();
        if (idx == 0)
        {
            return;
        }
        List<GameObject> players = ShowSquad.instance.PlayerTeam.players;
        GameObject g = players[idx - 1];
        players[idx-1] = players[idx];
        players[idx] = g;
        ShowSquad.instance.Clear();
        ShowSquad.instance.Show();
    }

    public void Swap(int idx,int i)
    {
        List<GameObject> players = ShowSquad.instance.PlayerTeam.players;
        GameObject g = players[i];
        players[i] = players[idx];
        players[idx] = g;
        ShowSquad.instance.Clear();
        ShowSquad.instance.Show();
    }

    public void onSwap()
    {
        isSelected = true;
        List<GameObject> view = ShowSquad.instance.allViews;
        int idx = findIdx();
        for(int i  = 0;i< view.Count; i++) {
            Player_View v = view[i].GetComponent<Player_View>();
            if(idx != i && v.isSelected)
            {
                Swap(idx, i);
                break;
            }
        }
    }

    public void onClickBuy()
    {
        List<GameObject> players = ShowMarket.instance.MarketTeam.players;
        GameObject rem = null;
        for(int i = 0;i < players.Count; i++)
        {
            if (players[i].GetComponent<Player>() == p)
            {
                rem = players[i];
                break;
            }
        }
        if(rem != null)
        {
            players.Remove(rem);
            Destroy(rem);
        }
        ShowSquad.instance.CreatePlayer(p,false);
        Destroy(this.gameObject);
    }
}
