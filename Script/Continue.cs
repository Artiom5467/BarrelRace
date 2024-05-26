using UnityEngine;
using UnityEngine.SceneManagement;
public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, LapsBotsPanel, Countdown, FinishCamera, Checkpoints, LapsSelected, BotsSelected;
    public void Restart()
    {   //Продовжити
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Вмкликається при натисканні Грати
    public void Play()
    {   // усі гонкові елементи вмикаються
        RaceUI.SetActive(true); // інтерфейс гонки
        Countdown.SetActive(true); // Відлік (3,2,1,go)
        Checkpoints.SetActive(true); // Чекпоінти
        LapsSelected.SetActive(true); // Кількость кіл
        BotsSelected.SetActive(true); //Кількость ботів
        LapsBotsPanel.SetActive(false); //Меню
        FinishCamera.SetActive(false); //Міняєм камеру
    }
}
