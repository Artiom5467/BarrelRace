using System;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100;
    public Transform barrel;
    public Transform Camera;
    public float speedEfect = 100;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // // Отримуємо вхід користувача по осі X та Z
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // Створюємо вектор руху на основі вхідних даних користувача
        rb.velocity = new Vector3(-moveVertical* speed,0 , moveHorizontal* speed); 
        Vector3 movement = new Vector3(0, -moveHorizontal, moveVertical);
        
        // Застосовуємо швидкість до вектору руху та часу
        movement *= speed * Time.deltaTime;
        
         //Застосовуємо переміщення до позиції об'єкта
        transform.Translate(movement);
        
        // Отримуємо введення користувача для обертання
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float rotationBarrel = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime * -speedEfect;
        
        transform.Rotate(rotation, 0, 0);
        barrel.Rotate(0, rotationBarrel, 0);
        barrel.Rotate(0, rotation*10, 0);
        
  

    }
 
}