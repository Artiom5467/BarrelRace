using UnityEngine;

public class CarCamera : MonoBehaviour
{
    Transform rootNode;
    Transform car;
    Rigidbody carPhysics;
    private GameObject PlayerCar;
    public float posX, posY, posZ;
    public float rotX;

    [Tooltip("Якщо швидкість нижче")]
    public float rotationThreshold = 1f;

    [Tooltip("Як сильно камера відстає.")]
    public float cameraStickiness = 10.0f;

    [Tooltip("Чим менше значення, тим плавніше обертання камери")]
    public float cameraRotationSpeed = 5.0f;

    private void Start()
    {
        PlayerCar = GameObject.FindGameObjectWithTag("PlayerCar");
    }

    void LateUpdate()
    {
        GetComponent<Camera>().transform.Translate(posX, posY, posZ);
        float rotZ = GetComponent<Camera>().transform.eulerAngles.z;
        GetComponent<Camera>().transform.Rotate(rotX, 0, -rotZ);
        rootNode = GetComponent<Transform>();
        car = PlayerCar.GetComponent<Transform>();
        carPhysics = PlayerCar.GetComponent<Rigidbody>();
        Quaternion look;

        // Переміщує камеру, щоб вона відповідала позиції автомобіля.
        rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);
        look = Quaternion.LookRotation(car.forward);
        look = Quaternion.Slerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
        rootNode.rotation = look;
    }
}
