using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlocks : MonoBehaviour
{
    // 블럭의 모양
    public bool[] shape1 =
    {
        true, true, true,
        false, false, false,
        false, false, false,
    };
    public bool[] shape2 =
    {
        false, false, false,
        true, false, false,
        true, false, false,
    };
    public bool[] shape3 =
    {
        false, false, false,
        false, true, true,
        false, true, true,
    };
    // 블럭의 수
    public bool[] shapeCount = new bool[3];
    // 검사할 배열
    bool[] checker;
    // 퍼즐 성공 여부
    public bool result;

    //배치할 퍼즐 위치
    public Transform linePos;
    public Transform leftPos;
    public Transform rightPos;

    //보상
    public GameObject fuel;
    void Start()
    {
        checker = new bool[shape1.Length];
    }

    public void Check(int index, bool v)
    {
        checker[index] = v;
        print(index + ", " + v);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //만약 퍼즐이 성공이라면
            if (result)
            {
                GameObject createFuel = Instantiate(fuel, transform.position, transform.rotation);
                print("퍼즐성공");
                result = false;
            }

        }
    }

    public void AllCheck()
    {
        if (!shapeCount[0])
        {
            // 배열 모두 검사하여 
            for (int i = 0; i < checker.Length; i++)
            {
                // 각배열의 값이 shape1과 같으면
                if (checker[i] == shape1[i])
                {
                    shapeCount[0] = true;
                }
                else
                {
                    shapeCount[0] = false;
                }

            }
        }
        if (!shapeCount[1])
        {

            // 배열 모두 검사하여
            for (int i = 0; i < checker.Length; i++)
            {
                // 각배열의 값이 shape1과 같으면
                if (checker[i] == shape2[i])
                {
                    shapeCount[1] = true;
                }
                else
                {
                    shapeCount[1] = false;
                }

            }
        }
        if (!shapeCount[2])
        {
            // 배열 모두 검사하여
            for (int i = 0; i < checker.Length; i++)
            {
                // 각배열의 값이 shape1과 같으면
                if (checker[i] == shape3[i])
                {
                    shapeCount[2] = true;
                }
                else
                {
                    shapeCount[2] = false;
                }

            }
        }
        // 모양이 모두 true 면
        for (int i = 0; i < shapeCount.Length; i++)
        {
            if (!shapeCount[i])
            {
                result = false;
            }
            else
            {
                result = true;
            }
            
        }
        //print("검사");

        for (int i = 0; i < checker.Length; i++)
        {
            checker[i] = false;
        }

    }
}
