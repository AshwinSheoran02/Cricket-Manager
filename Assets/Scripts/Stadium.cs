using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// using System;

public class Stadium : MonoBehaviour
{
   
    public TMP_Text peopleText;
    public TMP_Text profitText;
    public TMP_Text ticketPriceText;

    private int ticketPrice = 500;
    private int stadiumCapacity = 50000;
    private int numberOfPeople = 0;
    public GameObject stadiumPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Set UI elements (you need to assign these in the Unity Editor)
        // peopleText = peopleText;
        // profitText = profitText;

        // Simulate people coming to watch the game
        SimulatePeopleComing();

        // Calculate and display profit
        // SimulatePeopleComing();
        CalculateAndDisplayProfit();
    }

    public void SetTicketPrice(int newPrice)
    {
        if (newPrice >= 100 && newPrice <= 1000)
        {
            ticketPrice = newPrice;
            Debug.Log($"Ticket price set to {newPrice} rupees.");
            UpdateUI();
        }
        else
        {
            Debug.Log("Invalid ticket price. Ticket price should be between 100 and 1000 rupees.");
        }
    }

    public void IncreaseTicketPrice()
    {
        SetTicketPrice(ticketPrice + 100);
        SimulatePeopleComing();
    }

    public void DecreaseTicketPrice()
    {
        SetTicketPrice(ticketPrice - 100);
        SimulatePeopleComing();
    }

    public void SimulatePeopleComing()
    {
        float decreaseFactor = 1f - Mathf.Clamp01((float)(ticketPrice - 300) / 1000f);
        numberOfPeople = Mathf.RoundToInt(stadiumCapacity * decreaseFactor);

        Debug.Log($"{numberOfPeople} people are coming to watch the game.");
        UpdateUI();
    }

    public void CalculateAndDisplayProfit()
    {
        int profit = ticketPrice * numberOfPeople / 1000;
        Debug.Log($"Profit earned: {profit} rupees");
        UpdateUI();
    }

    void UpdateUI()
    {
        ticketPriceText.text = $"Ticket Price: {ticketPrice} ";
        // Update the UI text elements
        peopleText.text = $"People: {numberOfPeople/1000}K / {stadiumCapacity/1000}K";
        profitText.text = $"Profit: {ticketPrice * numberOfPeople /1000} rupees";
    }

    // Method to start the stadium panel
    public void StartStadiumPanel()
    {
        // Put your logic here to start the stadium panel
        stadiumPanel.SetActive(true);
        Debug.Log("Stadium panel started!");
    }
}
