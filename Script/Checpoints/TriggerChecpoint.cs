using System;
using UnityEngine;

public class ChkTrigger : MonoBehaviour
{
    public static bool startDis;
    private int nCheckpointNumber, kPos, CurrentChk, NextChk;
    public int CarPosListNumber;

    private void Start()
    {
        if (transform.parent.name.Length == 6)//AICar1)
        {
            CarPosListNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1));
        }
        if (transform.parent.name.Length == 7)//AICar11)
        {
            CarPosListNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 2));
        }
        if (CarPosListNumber == 0)
        {
            gameObject.name = "CarPos" + CarPosListNumber;
        }
        else
        {
            if (transform.parent.name.Length == 6)//CarPosAI1
            {
                gameObject.name = "CarPosAI0" + CarPosListNumber;
            }
            if (transform.parent.name.Length == 7)//CarPosAI11
            {
                gameObject.name = "CarPosAI" + CarPosListNumber;
            }
        }
        CurrentChk = 0;
        NextChk = 1;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Substring(0, 3) == "Chk")
        {
            ChkManager.nDistP[CarPosListNumber]= 0;
            for (int i = 0; i < this.name.Length; i++)
            {
                if (other.gameObject.name.Substring(i, 1) == "k")
                {
                    kPos = i + 1;
                    break;
                }
            }
            
            nCheckpointNumber = Convert.ToInt32(other.gameObject.name.Substring(kPos, other.gameObject.name.Length - kPos));
            if (CurrentChk + 1 == nCheckpointNumber)
            {
                ChkManager.nChk[CarPosListNumber]= nCheckpointNumber;
                CurrentChk += 1;
                NextChk += 1;

                if (nCheckpointNumber == 1)
                {
                    startDis = true;
                    ChkManager.nLapsP[CarPosListNumber] += 1;
                    CurrentChk = 1;
                    if (CarPosListNumber == 0)
                    {
                        LapTimeManager.MinuteCount = 0;
                        LapTimeManager.SecondCount = 0;
                        LapTimeManager.MilliCount = 0;
                    }
                }
            }
           
            if (GameObject.Find("Chk" + NextChk) == null)
            {
                CurrentChk = 0;
                NextChk = 1;
            }
        }
    }
}