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
                if (go.name.Length == 6)// ��'� � ����������� ������ ������ ز �� 6 ������� (���������: AICar4)
                {
                    AICarString = go.name.Substring(go.name.Length - 1);//�������� ���� ����� ���� (������� ������)
                    AICarNumber = int.Parse(AICarString);
                }
                else if (go.name.Length == 7)// ��'� � ����������� ������ ������ ز �� 7 ������� (���������: AICar15)
                {
                    AICarString = go.name.Substring(go.name.Length - 2);//�������� ����� ���� (������ 2 �������)
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
