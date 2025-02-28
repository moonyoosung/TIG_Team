﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_PlayerMove : MonoBehaviour
{
    // 이동속도
    public float speed = 10f;
    public float gravity = -10f;
    public float jumpPower = 1f;
    float yVelocity;
    CharacterController cc;
    int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir= Camera.main.transform.TransformDirection(dir);
        yVelocity += gravity * Time.deltaTime;
        transform.forward = Camera.main.transform.forward;

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            jumpCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount ==0)
        {
            yVelocity = jumpPower;
            jumpCount++;
        }
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
