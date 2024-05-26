using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    // Типи приводу автомобіля
    internal enum CarDriveType
    {
        FrontWheelDrive, // Передній привід
        RearWheelDrive, // Задній привід
        FourWheelDrive // Повний привід
    }

    // Типи швидкості
    internal enum SpeedType
    {
        MPH, // Милі на годину
        KPH // Кілометри на годину
    }

    // Клас контролера автомобіля
    public class CarController : MonoBehaviour
    {
        [SerializeField] private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive; // Тип приводу автомобіля
        [SerializeField] private WheelCollider[] m_WheelColliders = new WheelCollider[4]; // Колесні колайдери
        [SerializeField] private GameObject[] m_WheelMeshes = new GameObject[4]; // Візуальні об'єкти коліс
        [SerializeField] private Vector3 m_CentreOfMassOffset; // Зміщення центру маси
        [SerializeField] public float m_MaximumSteerAngle; // Максимальний кут кермування
        [Range(0, 1)][SerializeField] private float m_SteerHelper; // Допоміжна функція кермування
        [Range(0, 1)][SerializeField] private float m_TractionControl; // Контроль тяги
        [SerializeField] public float m_FullTorqueOverAllWheels; // Повний крутний момент по всім колесам
        [SerializeField] private float m_ReverseTorque; // Крутний момент заднього ходу
        [SerializeField] private float m_MaxHandbrakeTorque; // Максимальний крутний момент ручного гальма
        [SerializeField] private float m_Downforce = 100f; // Додаткова сила на ґрунт
        [SerializeField] private SpeedType m_SpeedType; // Тип швидкості
        [SerializeField] public float m_Topspeed = 200; // Максимальна швидкість
        [SerializeField] private static int NoOfGears = 5; // Кількість передач
        [SerializeField] private float m_RevRangeBoundary = 1f; // Межа обертів
        [SerializeField] private float m_SlipLimit; // Межа прослизання
        [SerializeField] private float m_BrakeTorque; // Крутний момент гальма
        public Transform barrel;

        private Quaternion[] m_WheelMeshLocalRotations; // Локальні обертання візуальних коліс
        private Vector3 m_Prevpos, m_Pos; // Попереднє положення та поточне положення
        private float m_SteerAngle; // Кут кермування
        private int m_GearNum; // Номер передачі
        private float m_GearFactor; // Фактор передачі
        private float m_OldRotation; // Попередня обертовацька швидкість
        private float m_CurrentTorque; // Поточний крутний момент
        private Rigidbody m_Rigidbody;
        private const float k_ReversingThreshold = 0.01f; 

        public bool Skidding { get; private set; } // Чи заноситься автомобіль
        public float BrakeInput { get; private set; } // Вхід гальма
        public float CurrentSteerAngle { get { return m_SteerAngle; } } // Поточний кут кермування
        public float CurrentSpeed { get { return m_Rigidbody.velocity.magnitude * 2.23693629f; } } // Поточна швидкість
        public float MaxSpeed { get { return m_Topspeed; } } // Максимальна швидкість
        public float Revs { get; private set; } // Оберти
        public float AccelInput { get; private set; } // Вхід прискорення


        private void Start()
        {
            m_WheelMeshLocalRotations = new Quaternion[4];
            for (int i = 0; i < 4; i++)
            {
                m_WheelMeshLocalRotations[i] = m_WheelMeshes[i].transform.localRotation;
            }
            m_WheelColliders[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;

            m_MaxHandbrakeTorque = float.MaxValue;

            m_Rigidbody = GetComponent<Rigidbody>();
            m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl * m_FullTorqueOverAllWheels);

            Application.targetFrameRate = 60;
        }



        private void GearChanging()
        {
            float f = Mathf.Abs(CurrentSpeed / MaxSpeed);
            float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
            float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

            if (m_GearNum > 0 && f < downgearlimit)
            {
                m_GearNum--;
            }

            if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
            {
                m_GearNum++;
            }
        }


        
        private static float CurveFactor(float factor)
        {
            return 1 - (1 - factor) * (1 - factor);
        }

        private static float ULerp(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }


        private void CalculateGearFactor()
        {
            float f = (1 / (float)NoOfGears);
           
            var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(CurrentSpeed / MaxSpeed));
            m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
        }


        private void CalculateRevs()
        {

            CalculateGearFactor();
            var gearNumFactor = m_GearNum / (float)NoOfGears;
            var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
            Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
        }


        public void Move(float steering, float accel, float footbrake, float handbrake)
        {
            for (int i = 0; i < 4; i++)
            {
                Quaternion quat;
                Vector3 position;
                m_WheelColliders[i].GetWorldPose(out position, out quat);
                m_WheelMeshes[i].transform.position = position;
                m_WheelMeshes[i].transform.rotation = quat;
            }

            steering = Mathf.Clamp(steering, -1, 1);
            AccelInput = accel = Mathf.Clamp(accel, 0, 1);
            BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);
            handbrake = Mathf.Clamp(handbrake, 0, 1);
            m_SteerAngle = steering * m_MaximumSteerAngle;
            m_WheelColliders[0].steerAngle = m_SteerAngle;
            m_WheelColliders[1].steerAngle = m_SteerAngle;

            SteerHelper();
            ApplyDrive(accel, footbrake);
            CapSpeed();

            if (handbrake > 0f)
            {
                var hbTorque = handbrake * m_MaxHandbrakeTorque;
                m_WheelColliders[2].brakeTorque = hbTorque;
                m_WheelColliders[3].brakeTorque = hbTorque;
            }


            CalculateRevs();
            GearChanging();

            AddDownForce();
            CheckForWheelSpin();
            TractionControl();
            RotateBarrel();
        }

        public void RotateBarrel()
        {

            if (barrel != null)
            {
                float speed = m_Rigidbody.velocity.magnitude;
                float rotationAngle = speed * Time.deltaTime;
                barrel.Rotate(0, rotationAngle * -100, 0);
            }
            else
            {
                Debug.LogError("Barrel Transform is not assigned.");
            }
        }
        private void CapSpeed()
        {
            float speed = m_Rigidbody.velocity.magnitude;
            switch (m_SpeedType)
            {
                case SpeedType.MPH:

                    speed *= 2.23693629f;
                    if (speed > m_Topspeed)
                        m_Rigidbody.velocity = (m_Topspeed / 2.23693629f) * m_Rigidbody.velocity.normalized;
                    break;

                case SpeedType.KPH:
                    speed *= 3.6f;
                    if (speed > m_Topspeed)
                        m_Rigidbody.velocity = (m_Topspeed / 3.6f) * m_Rigidbody.velocity.normalized;
                    break;
            }
        }


        private void ApplyDrive(float accel, float footbrake)
        {

            float thrustTorque;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 4f);
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].motorTorque = thrustTorque;
                    }
                    break;

                case CarDriveType.FrontWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[0].motorTorque = m_WheelColliders[1].motorTorque = thrustTorque;
                    break;

                case CarDriveType.RearWheelDrive:
                    thrustTorque = accel * (m_CurrentTorque / 2f);
                    m_WheelColliders[2].motorTorque = m_WheelColliders[3].motorTorque = thrustTorque;
                    break;

            }

            for (int i = 0; i < 4; i++)
            {
                if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, m_Rigidbody.velocity) < 50f)
                {
                    m_WheelColliders[i].brakeTorque = m_BrakeTorque * footbrake;
                }
                else if (footbrake > 0)
                {
                    m_WheelColliders[i].brakeTorque = 0f;
                    m_WheelColliders[i].motorTorque = -m_ReverseTorque * footbrake;
                }
            }
        }


        private void SteerHelper()
        {
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelhit;
                m_WheelColliders[i].GetGroundHit(out wheelhit);
                if (wheelhit.normal == Vector3.zero)
                    return; 
            }
            if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
            {
                var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
                Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
                m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
            }
            m_OldRotation = transform.eulerAngles.y;
        }
        private void AddDownForce()
        {
            m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * m_Downforce *
                                                         m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
        }


        private void CheckForWheelSpin()
        {
            for (int i = 0; i < 4; i++)
            {
                WheelHit wheelHit;
                m_WheelColliders[i].GetGroundHit(out wheelHit);

                if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit)
                {

                }


            }
        }
        private void TractionControl()
        {
            WheelHit wheelHit;
            switch (m_CarDriveType)
            {
                case CarDriveType.FourWheelDrive:
                    for (int i = 0; i < 4; i++)
                    {
                        m_WheelColliders[i].GetGroundHit(out wheelHit);

                        AdjustTorque(wheelHit.forwardSlip);
                    }
                    break;

                case CarDriveType.RearWheelDrive:
                    m_WheelColliders[2].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[3].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;

                case CarDriveType.FrontWheelDrive:
                    m_WheelColliders[0].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    m_WheelColliders[1].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);
                    break;
            }
        }


        private void AdjustTorque(float forwardSlip)
        {
            if (forwardSlip >= m_SlipLimit && m_CurrentTorque >= 0)
            {
                m_CurrentTorque -= 10 * m_TractionControl;
            }
            else
            {
                m_CurrentTorque += 10 * m_TractionControl;
                if (m_CurrentTorque > m_FullTorqueOverAllWheels)
                {
                    m_CurrentTorque = m_FullTorqueOverAllWheels;
                }
            }
        }


    }
}
