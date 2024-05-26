using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotSelector : MonoBehaviour
{
    private List<GameObject> AICarsList = new List<GameObject>();
    private GameObject[] AICars;
    [SerializeField] private GameObject BotUp, BotDown, Bots, BotsPanel;
    public static int nBots;
    private int ReactivatePlusButton;

    private void Start()
    {
        nBots = 1;

        AICars = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in AICars)
        {
            if (go.name.Contains("AICar"))
            {
                AICarsList.Add(go);
            }
        }

        ReactivatePlusButton = (AICarsList.Count / 2);
        ReactivatePlusButton--;
    }

    public void Up()
    {
        nBots = Convert.ToInt32(Bots.GetComponent<Text>().text) + 1;
        Bots.GetComponent<Text>().text = Convert.ToString(nBots);

        if (Bots.GetComponent<Text>().text == (AICarsList.Count / 2).ToString())
        {
            BotUp.SetActive(false);
        }

        if (Bots.GetComponent<Text>().text == "2")
        {
            BotDown.SetActive(true);
        }
    }

    public void Down()
    {
        nBots = Convert.ToInt32(Bots.GetComponent<Text>().text) - 1;
        Bots.GetComponent<Text>().text = Convert.ToString(nBots);

        if (Bots.GetComponent<Text>().text == "1")
        {
            BotDown.SetActive(false);
        }

        if (Bots.GetComponent<Text>().text == ReactivatePlusButton.ToString())
        {
            BotUp.SetActive(true);
        }
    }
}
