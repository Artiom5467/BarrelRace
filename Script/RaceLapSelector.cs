using System;
using UnityEngine;
using UnityEngine.UI;

// Кількість кіл
public class LapSelector : MonoBehaviour
{
    [SerializeField] private GameObject LapUp, LapDown, Laps, LapsPanel;
    public static int nLaps;
    [Header("Змініть максимальну кількість кіл тут:")]
    [SerializeField] private int MaximumLaps;
    private int ReactivatePlusButton;

    private void Start()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text);
        ReactivatePlusButton = MaximumLaps - 1;
    }

    public void Up()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text) + 1;
        Laps.GetComponent<Text>().text = Convert.ToString(nLaps);

        if (Laps.GetComponent<Text>().text == MaximumLaps.ToString())
        {
            LapUp.SetActive(false);
        }

        if (Laps.GetComponent<Text>().text == "3")
        {
            LapDown.SetActive(true);
        }
    }

    public void Down()
    {
        nLaps = Convert.ToInt32(Laps.GetComponent<Text>().text) - 1;
        Laps.GetComponent<Text>().text = Convert.ToString(nLaps);

        if (Laps.GetComponent<Text>().text == "2")
        {
            LapDown.SetActive(false);
        }

        if (Laps.GetComponent<Text>().text == ReactivatePlusButton.ToString())
        {
            LapUp.SetActive(true);
        }
    }
}
