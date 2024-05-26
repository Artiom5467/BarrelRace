using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private GameObject CountDown, LapTimer, CarControls; 

    void Start()
    {
        StartCoroutine(CountStart()); 
    }

    IEnumerator CountStart()
    {
        
        LapTimeManager.MinuteCount = 0; LapTimeManager.SecondCount = 0; LapTimeManager.MilliCount = 0;
        // Â³äë³ê 3, 2, 1
        yield return new WaitForSeconds(0.5f);
        CountDown.GetComponent<Text>().text = "3";

        CountDown.SetActive(true); 
        yield return new WaitForSeconds(1); 
        CountDown.SetActive(false); 
        CountDown.GetComponent<Text>().text = "2"; 

        CountDown.SetActive(true); 
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        CountDown.GetComponent<Text>().text = "1";

        CountDown.SetActive(true);
       
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        LapTimer.SetActive(true); 
        CarControls.SetActive(true); 
    }
}
