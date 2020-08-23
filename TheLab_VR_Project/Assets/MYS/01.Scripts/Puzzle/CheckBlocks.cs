using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlocks : MonoBehaviour
{
    public Animation fuelBox;
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
    int count = 0;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < shape3.Length; i++)
            {
                print(i + "Shape3 " + i +"값 : " + shape3[i]);
            }
        }
        //만약 퍼즐이 성공이라면
        if (result)
        {
            if (count < 1)
            {
                count++;
                fuelBox.transform.GetComponentInChildren<BoxCollider>().enabled = false;
                fuel.transform.GetComponent<BoxCollider>().enabled = true;
                fuel.transform.GetComponent<Rigidbody>().useGravity = true;
                fuelBox.Play();
            }
            print("퍼즐성공");
            result = false;
        }

    }

    public void AllCheck()
    {
        print("퍼즐 체크 시작");
        for (int i = 0; i < checker.Length; i++)
        {
            print(i + "인자 값 : " + checker[i]);
        }

        if (shapeCount[0] == false)
        {
            // 배열 모두 검사하여 
            for (int i = 0; i < 3; i++)
            {

                // 각배열의 값이 shape1과 같으면
                if (checker[i] == shape1[i])
                {
                    shapeCount[0] = true;
                }
                else
                {
                    shapeCount[0] = false;
                    print(i + "번째가 다릅니다.");

                    break;
                }
            }
        }
        if (shapeCount[1] == false)
        {

            // 배열 모두 검사하여
            for (int i = 3; i < 7; i+=3)
            {
                shapeCount[1] = true;

                // 각배열의 값이 shape2과 같으면
                if (checker[i] != shape2[i])
                {
                    shapeCount[1] = false;
                    print(i + "번째가 다릅니다.");

                    break;
                }
            }
        }
        if (shapeCount[2] == false)
        {
            // 배열 모두 검사하여
            for (int i = 4; i < 6; i++)
            {
                shapeCount[2] = true;

                // 각배열의 값이 shape3과 같으면
                if (checker[i] != shape3[i])
                {
                    shapeCount[2] = false;
                    print(i + "번째가 다릅니다.");

                    break;
                }
            }
            // 배열 모두 검사하여
            for (int i = 7; i < 9; i++)
            {
                shapeCount[2] = true;

                // 각배열의 값이 shape3과 같으면
                if (checker[i] != shape3[i])
                {
                    shapeCount[2] = false;
                    print(i + "번째가 다릅니다.");

                    break;
                }
            }
        }

        // 모양이 모두 true 면
        for (int i = 0; i < shapeCount.Length; i++)
        {
            result = true;
            if (!shapeCount[i])
            {
                result = false;
                break;
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
