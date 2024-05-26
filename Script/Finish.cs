using UnityEngine;
using UnityEngine.UI;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private GameObject FinishCam; // камера фінішу
    [SerializeField] private GameObject ViewModes; 
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2; 
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose; // різні панелі фінішу
  
    void OnTriggerEnter()
    {
        this.GetComponent<BoxCollider>().enabled = false; //фініш вимикається
        FinishCam.SetActive(true); 
        PauseButton.SetActive(false);
        Panel1.SetActive(false);
        Panel2.SetActive(false); 
        ViewModes.SetActive(false);
        string text = PosDisplay.GetComponent<Text>().text;

        if (text[0] == '1')
        {
            FinishPanelWin.SetActive(true); // виграшна панель активується
            FinishPanelLose.SetActive(false); // панель поразки вимикається
        }
        else
        {
            FinishPanelWin.SetActive(false); // виграшна панель вимикається
            FinishPanelLose.SetActive(true); // панель поразки активується
            Time.timeScale = 0; // час зупиняється
        }
    }
}
