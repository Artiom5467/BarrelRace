using UnityEngine;
using UnityEngine.SceneManagement;
public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, LapsBotsPanel, Countdown, FinishCamera, Checkpoints, LapsSelected, BotsSelected;
    public void Restart()
    {   //����������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //����������� ��� ��������� �����
    public void Play()
    {   // �� ������ �������� ����������
        RaceUI.SetActive(true); // ��������� �����
        Countdown.SetActive(true); // ³��� (3,2,1,go)
        Checkpoints.SetActive(true); // ��������
        LapsSelected.SetActive(true); // ʳ������� ��
        BotsSelected.SetActive(true); //ʳ������� ����
        LapsBotsPanel.SetActive(false); //����
        FinishCamera.SetActive(false); //̳���� ������
    }
}
