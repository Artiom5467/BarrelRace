using System.Collections;
using UnityEngine;

public class AIBarrelTrack : MonoBehaviour
{
    private GameObject Barrel;
    private int CurrentBarrel;
    private string AICarName;

    private void Start()
    {
        //бере позицію наступної точки розміщеної на трасі.
        CurrentBarrel = 1;
        Barrel = GameObject.Find("Point" + CurrentBarrel);
        this.transform.position = Barrel.transform.position;
        if (gameObject.name.Length == 13)
        {
            AICarName = gameObject.name.Substring(gameObject.name.Length - 6);
        }
        if (gameObject.name.Length == 14)
        {
            AICarName = gameObject.name.Substring(gameObject.name.Length - 7);
        }
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == AICarName) // Якщо виявлено колайдер
        {
            Barrel = GameObject.Find("Point" + CurrentBarrel);
            this.transform.position = Barrel.transform.position;
            this.GetComponent<BoxCollider>().enabled = false;
            CurrentBarrel += 1;
            if (GameObject.Find("Point" + CurrentBarrel) == null)
            {
                CurrentBarrel = 1;
            }
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<BoxCollider>().enabled = true;// можемо знову увімкнути колайдер
        }
    }
}
