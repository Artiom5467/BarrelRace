using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class BarreAICarBehaviour : MonoBehaviour
{
    CarController AICarController;
    private Rigidbody AICarRigidbody;
    private float AICarSpeed;
    private int CheckReverse, StartReverse;
    private float NormalTorque, NormalSteering, NormalTopspeed;
    private WheelCollider[] AllWheelColliders;

    void Start()
    {
        StartCoroutine(CheckReverseCoroutine());
        AICarRigidbody = GetComponent<Rigidbody>();
        AICarController = gameObject.GetComponentInParent(typeof(CarController)) as CarController;
        AllWheelColliders = GetComponentsInChildren<WheelCollider>();
        NormalTorque = AICarController.m_FullTorqueOverAllWheels;
        NormalSteering = AICarController.m_MaximumSteerAngle;
        NormalTopspeed = AICarController.m_Topspeed;
    }

    IEnumerator CheckReverseCoroutine()
    {
        //Затримка в 3 секунди
        yield return new WaitForSeconds(3);
        CheckReverse = 1;//Чи треба використовувати задній хід
        yield return null;
    }

    private void Update()
    {
        AICarSpeed = AICarRigidbody.velocity.magnitude;//Швидкість ШІ
        if (AICarSpeed < 0.25f && CheckReverse == 1)
        {
            CheckReverse = 0;
            StartReverse = 1;
        }

        if (StartReverse == 1)//Включаємо задній хід
        {
            
            AICarController.m_FullTorqueOverAllWheels = (AICarController.m_FullTorqueOverAllWheels * -1);
            AICarController.m_MaximumSteerAngle = 0;
            StartReverse = 0;
            StartCoroutine(ReverseCoroutine());
        }
    }

    IEnumerator ReverseCoroutine()
    {
        foreach (WheelCollider wc in AllWheelColliders)
            wc.enabled = false;
        foreach (WheelCollider wc in AllWheelColliders)
            wc.enabled = true;
        //Чекає
        yield return new WaitForSeconds(1);
        //Повертаає в норму
        AICarController.m_FullTorqueOverAllWheels = AICarController.m_FullTorqueOverAllWheels + NormalTorque + NormalTorque;
        //Може знову повернути
        yield return new WaitForSeconds(1);
        AICarController.m_MaximumSteerAngle = NormalSteering;
        yield return new WaitForSeconds(1);
        CheckReverse = 1;
        yield return null;
    }

    //Антизіткнення
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "AICarCollider")
        {
            AICarController.m_Topspeed = 15;//Зменшуємо швидкість
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "AICarCollider")
        {
            AICarController.m_Topspeed = NormalTopspeed;
        }
    }
}
