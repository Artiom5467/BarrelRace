using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChkManager : MonoBehaviour
{
   
    [SerializeField] private GameObject PositionDisplay;
    // Тригер завершення гонки
    [SerializeField] private GameObject RaceFinishTrigger;
    // Оновлення кіл
    [SerializeField] private GameObject LapCounter;
    // Скільки кругів вибрано
    private int LapsSelected;
    private GameObject[] CarPosListGameObjects;
    // Значення гравця та об'єкт гри
    public static List<int> nChk = new List<int>();
    public static List<int> nLapsP = new List<int>();
    public static List<double> nDistP = new List<double>();
    public static List<GameObject> UnsortedCarPosList = new List<GameObject>();
    public static List<GameObject> CarPosList = new List<GameObject>();
    public List<GameObject> PublicCarPosList = new List<GameObject>();
    public static List<double> scoreP = new List<double>();
    public static List<int> pNum = new List<int>();
    public static int posMax;
    bool added = false;

    private GameObject[] UnparentChks;

    public void Start()
    {
      
        scoreP.Clear();
        pNum.Clear();
        nChk.Clear();
        nDistP.Clear();
        nLapsP.Clear();
        UnsortedCarPosList.Clear();
        CarPosList.Clear();
        PublicCarPosList.Clear();
        // Початок
        ChkTrigger.startDis = false; 
        PositionDisplay.GetComponent<Text>().text = "--";

        CarPosListGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in CarPosListGameObjects)
        {
            if (go.name.Contains("CarPos"))
            {
                UnsortedCarPosList.Add(go);
            }
        }

        for (int i = 0; i < UnsortedCarPosList.Count; i++)
        {
            added = false;
            for (int j = 0; j < CarPosList.Count; j++)
            {
                if (UnsortedCarPosList[i].name.CompareTo(CarPosList[j].name) < 0)
                {
                    CarPosList.Insert(j, UnsortedCarPosList[i]);
                    added = true;
                    break;
                }
            }
            if (!added)
                CarPosList.Add(UnsortedCarPosList[i]);
        }

        for (int i = 0; i < UnsortedCarPosList.Count; i++)
        {
            added = false;
            for (int j = 0; j < PublicCarPosList.Count; j++)
            {
                if (UnsortedCarPosList[i].name.CompareTo(PublicCarPosList[j].name) < 0)
                {
                    PublicCarPosList.Insert(j, UnsortedCarPosList[i]);
                    added = true;
                    break;
                }
            }
            if (!added)
                PublicCarPosList.Add(UnsortedCarPosList[i]);
        }

        
        for (int i = 0; i <= BotSelector.nBots; i++) 
        {
            nChk.Add(0);
            nDistP.Add(0);
            nLapsP.Add(0);
            scoreP.Add(0);
            pNum.Add(i + 1); 
        }
        UnparentChks = GameObject.FindGameObjectsWithTag("chk");
        foreach (GameObject go in UnparentChks)
        {
            go.transform.parent = null;
        }
    }

    public void Update()
    {
        LapsSelected = LapSelector.nLaps;

        for (int i = 0; i <= BotSelector.nBots; i++) 
        {
            scoreP[i] = nDistP[i] + (nChk[i] * 100) + (nLapsP[i] * 10000);
        }

        if (ChkTrigger.startDis)
        {
            posPlayer(1);
            PositionDisplay.GetComponent<Text>().text = (posMax) + CardinalPos(posMax) + " Місце";
        }

        if (LapsSelected < nLapsP[0])
        {
            RaceFinishTrigger.SetActive(true);
        }
        
        LapCounter.GetComponent<Text>().text = Convert.ToString(nLapsP[0]);
    }
    public string CardinalPos(int i)
    {

        string s = "  ";
        return s;
    }
    public static void posPlayer(int nPlayer)
    {
        double max = 0;
        posMax = 1;

        for (int i = 0; i < scoreP.Count; i++)
        {
            if (scoreP[nPlayer - 1] < scoreP[i])
            {
                max = scoreP[i];
                posMax = posMax + 1;
            }
        }
    }
}