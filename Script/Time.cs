using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public bool Paused;

    public void TimeScale0()
    {
        Time.timeScale = 0; // �������� ���
    }

    public void TimeScale1()
    {
        AudioListener.volume = 1f; // �������� ����
        Time.timeScale = 1; // �������� ���
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState(); //(�����/����������)
        }

        
    }

    //Es� �����
    public void Pause()
    {
        ChangeState(); 
    }

   
    private void ChangeState()
    {
        Paused = !Paused; 
        if (Paused)
        {
            
            AudioListener.volume = 0f;
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        else
        {
            
            AudioListener.volume = 1f;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }
}
