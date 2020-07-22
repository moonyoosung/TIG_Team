using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_CamRotate : MonoBehaviour
{
    // 카메라 회전 속도
    public float camRotateSpeed = 100f;
    float angleX;
    float angleY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        angleX += h*camRotateSpeed * Time.deltaTime;
        angleY += v * camRotateSpeed * Time.deltaTime;

        angleY = Mathf.Clamp(angleY, -60, 60);
        transform.eulerAngles = new Vector3(-angleY, angleX, 0);
    }
}
