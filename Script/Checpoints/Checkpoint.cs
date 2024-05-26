using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private float nDistanceX, nDistanceY, nDistanceZ;
    private List<double> nDistChk = new List<double>();
    private int nCheckpointNumber, kPos;
    
    private void Start()
    {
        
        for (int i = 0; i < this.name.Length; i++)
        {
            if (this.name.Substring(i, 1) == "k")// всі пункти контролю мають назви типу: Chk1, Chk2, Chk3
            {
                kPos = i + 1;
                break;
            }
        }

        if (this.name.Length != 3) { nCheckpointNumber = Convert.ToInt32(this.name.Substring(kPos, this.name.Length - kPos)); }

        nDistChk.Clear();// очистити всі використані раніше відстані

        for (int i = 0; i <= BotSelector.nBots; i++)
        {
            nDistChk.Add(0);
        }
    }

    void Update()
    {
        for (int i = 0; i <= BotSelector.nBots; i++)
        {
            if (ChkManager.nChk[i] == nCheckpointNumber)
            {
               
                nDistanceZ = this.GetComponent<Transform>().localPosition.z - ChkManager.CarPosList[i].GetComponent<Transform>().position.z;
                nDistanceY = this.GetComponent<Transform>().localPosition.y - ChkManager.CarPosList[i].GetComponent<Transform>().position.y;
                nDistanceX = this.GetComponent<Transform>().localPosition.x - ChkManager.CarPosList[i].GetComponent<Transform>().position.x;
                nDistChk[i] = Math.Sqrt(Math.Pow(nDistanceX, 2) + Math.Pow(nDistanceY, 2) + Math.Pow(nDistanceZ, 2));
                ChkManager.nDistP[i] = nDistChk[i];
              
            }
        }
    }
}
