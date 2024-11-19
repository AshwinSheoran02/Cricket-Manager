using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Squad;
    public GameObject Market;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSquad()
    {
        Squad.SetActive(true);
        MainMenu.SetActive(false);
        Market.SetActive(false);
    }

    public void onClickBack()
    {
        Market.SetActive(false);
        Squad.SetActive(false);
        MainMenu.SetActive(true);   
    }

    public void onClickMarket()
    {
        Squad.SetActive(false);
        MainMenu.SetActive(false);
        Market.SetActive(true);
    }
}
