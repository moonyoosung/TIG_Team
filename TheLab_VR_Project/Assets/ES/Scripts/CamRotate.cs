using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{

    //카메라의 회전속도
    public float camRotSpeed = 200.0f;

    float angleX;
    float angleY;

    //카메라의 회전 범위


    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        angleX += h * camRotSpeed * Time.fixedDeltaTime;
        angleY += v * camRotSpeed * Time.fixedDeltaTime;

        //카메라 회전 범위 지정
        angleY = Mathf.Clamp(angleY, -60f, 60f);

        //회전범위 적용한 값 넣기
        transform.eulerAngles = new Vector3(0, angleX, 0);
        Camera.main.transform.eulerAngles = 
            new Vector3(-angleY, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
    }
}
