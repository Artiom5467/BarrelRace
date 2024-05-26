using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public bool Paused;

    public void TimeScale0()
    {
        Time.timeScale = 0; // зупинити час
    }

    public void TimeScale1()
    {
        AudioListener.volume = 1f; // відновити звук
        Time.timeScale = 1; // відновити час
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState(); //(пауза/відновлення)
        }

        
    }

    //Esс пауза
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
