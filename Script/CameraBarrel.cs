using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    // Зміна активної камери під час гонки за допомогою клавіші V
    [SerializeField] private Camera NormalCam, FarCam, FPCam;//Камери
    [SerializeField] private string ButtonName;
    private int CamMode;

    void Update()
    {
        if (Input.GetButtonDown(ButtonName))
        {
            if (CamMode == 2)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }
            StartCoroutine(ChangeCamera());
        }
    }

    IEnumerator ChangeCamera()
    {
        yield return new WaitForSeconds(0.01f);
                                               
        if (CamMode == 0)
        {
            FarCam.gameObject.SetActive(true);
            FPCam.gameObject.SetActive(false);
        }
        if (CamMode == 1)
        {
            NormalCam.gameObject.SetActive(true);
            FarCam.gameObject.SetActive(false);
        }

        if (CamMode == 2)
        {
            FPCam.gameObject.SetActive(true);
            FarCam.gameObject.SetActive(false);
        }
    }
}