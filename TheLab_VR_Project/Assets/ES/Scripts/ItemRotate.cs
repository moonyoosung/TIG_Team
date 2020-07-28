using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    //아이템이 선택이 되면, 아이템을 돌려볼 수 있다.


    //아이템의 회전속도
    public float rotSpeed = 200.0f;

    float angleX;
    float angleY;

   
    void Start()
    {

    }


    void Update()
    {
        //마우스의 움직임에 따라 아이템 회전
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        angleX += h * rotSpeed * Time.fixedDeltaTime;
        angleY += v * rotSpeed * Time.fixedDeltaTime;

        transform.eulerAngles = new Vector3(angleY, -angleX, 0);

        
    }
}
