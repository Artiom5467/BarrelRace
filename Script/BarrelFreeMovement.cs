using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [Header("Player components")]
    [SerializeField] private Rigidbody rb;

    public Transform barrel;
    public Transform barrelSup;
    public float speedEfect = 100;
    public float maxRotation = 30f;
    

    [Header("Character stats")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 10;
    private bool _onGround;

    private const string MOVE_AMOUNT_ANIMATION_VARIABLE = "Move amount";
    private const string JUMP_ANIMATION_VARIABLE = "Jump";

    #region Performing
    int left = 0;
    int right = 0;
    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        Vector3 forwardLook = new Vector3(camera.forward.x, 0, camera.forward.z);
        Vector3 moveDirection = forwardLook * vertical + camera.right * horizontal;

        Movement(moveDirection);
        

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float rotationBarrel = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime * -speedEfect;
        
        float rotationInput = Input.GetAxis("Horizontal");

        // Обчислення швидкості обертання з урахуванням вводу користувача
        // float rotationSpeedModified = rotationInput * rotationSpeed;
        transform.Rotate(rotation, 0, 0);
        
     
        
        if (rotation > 0 && left == 0) 
        {
            barrelSup.Rotate(15, 0, 0);
            left++;
        }

        if (rotation < 0 && right == 0 )
        {
            barrelSup.Rotate(-15, 0, 0);
            right++;
        }

        if (rotation == 0)
        {
            if (left > 0)
            {
                barrelSup.Rotate(-15, 0, 0);
                left = 0;
            }
            if (right > 0)
            {
                barrelSup.Rotate(15, 0, 0);
                right = 0;
            }

            
        }
        
        float currentRotation = transform.eulerAngles.z;
        
        // rb.AddTorque(Vector3.up * rotationSpeedModified);
        barrel.Rotate(0, rotationBarrel, 0);
        barrel.Rotate(0, rotation*10, 0);
         // moveDirection += camera.up * horizontal;
         //
         // Debug.Log(moveDirection);
    }


    #endregion

    #region Movement
    
    private void Movement(Vector3 moveDirection)
    {
        Vector3 velocityDir = moveDirection * moveSpeed;

        velocityDir.y = rb.velocity.y;
        rb.velocity = velocityDir;
    }
    
    
    // private void RotationNormal(Vector3 rotationDirection)
    // {
    //     Vector3 targetDir = rotationDirection;
    //     Quaternion loolDir = Quaternion.LookRotation(targetDir);
    //     Quaternion targetRot = Quaternion.Slerp(transform.rotation, loolDir, rotationSpeed * Time.deltaTime);
    //     transform.rotation = targetRot;
    // }



    #endregion


}