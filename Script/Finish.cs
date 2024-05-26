using UnityEngine;
using UnityEngine.UI;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private GameObject FinishCam; // ������ �����
    [SerializeField] private GameObject ViewModes; 
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2; 
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose; // ��� ����� �����
  
    void OnTriggerEnter()
    {
        this.GetComponent<BoxCollider>().enabled = false; //���� ����������
        FinishCam.SetActive(true); 
        PauseButton.SetActive(false);
        Panel1.SetActive(false);
        Panel2.SetActive(false); 
        ViewModes.SetActive(false);
        string text = PosDisplay.GetComponent<Text>().text;

        if (text[0] == '1')
        {
            FinishPanelWin.SetActive(true); // �������� ������ ����������
            FinishPanelLose.SetActive(false); // ������ ������� ����������
        }
        else
        {
            FinishPanelWin.SetActive(false); // �������� ������ ����������
            FinishPanelLose.SetActive(true); // ������ ������� ����������
            Time.timeScale = 0; // ��� �����������
        }
    }
}
