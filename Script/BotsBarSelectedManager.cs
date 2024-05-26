using UnityEngine;

public class BotsSelectedManager : MonoBehaviour
{
    private GameObject[] AICars;
    private string AICarString;
    private int AICarNumber;

    void Awake()
    {
        AICars = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in AICars)
        {
            if (go.name.Contains("AICar"))
            {
                if (go.name.Length == 6)// ім'я з одноразовим числом машина ШІ має 6 символів (наприклад: AICar4)
                {
                    AICarString = go.name.Substring(go.name.Length - 1);//отримати лише число імені (останній символ)
                    AICarNumber = int.Parse(AICarString);
                }
                else if (go.name.Length == 7)// ім'я з двоцифровим числом машина ШІ має 7 символів (наприклад: AICar15)
                {
                    AICarString = go.name.Substring(go.name.Length - 2);//отримати число імені (останні 2 символи)
                    AICarNumber = int.Parse(AICarString);
                }

                if (go.name == "AICar" + AICarNumber)
                {
                    if (AICarNumber > BotSelector.nBots)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }
}
