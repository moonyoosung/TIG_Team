using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlocks : MonoBehaviour
{
    // 블럭의 모양
    int[] shape1 =
    {
        1, 1, 1,
        0, 0, 0,
        0, 0, 0,
    };
    int[] shape2 =
    {
        0, 0, 0,
        1, 0, 0,
        1, 0, 0,
    };
    int[] shape3 =
    {
        0, 0, 0,
        0, 1, 1,
        0, 1, 1,
    };
    // 블럭의 수
    public bool[] shapeCount = new bool[3];
    // 검사할 배열
    int[] checker;
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
        checker = new int[shape1.Length];
    }

    public void Check(int index, int v)
    {
        checker[index] = v;
        //print(index + ", " + v);
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
        print("퍼즐 체크 시작");
        for (int i = 0; i < checker.Length; i++)
        {
            print(i + "인자 값 : " + checker[i]);
        }
        for (int i = 0; i < shape1.Length; i++)
        {
            print(i + "모양 인자 값 : " + shape1[i]);
        }

        if(shapeCount[0] == false) { 
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
                print(i+ "번째가 다릅니다.");

                break;
            }
        }
        }
        if (shapeCount[1] == false)
        {

            // 배열 모두 검사하여
            for (int i = 0; i < checker.Length; i++)
            {
                shapeCount[1] = true;

                // 각배열의 값이 shape2과 같으면
                if (checker[i] != shape2[i])
                {
                    shapeCount[1] = false;
                    break;
                }
            }
        }
        if (shapeCount[2] == false)
        {
            // 배열 모두 검사하여
            for (int i = 0; i < checker.Length; i++)
            {
                shapeCount[2] = true;

                // 각배열의 값이 shape3과 같으면
                if (checker[i] != shape3[i])
                {
                    shapeCount[2] = false;
                    break;
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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            print("나는 퍼즐판입니다.");
        }
    }
}
