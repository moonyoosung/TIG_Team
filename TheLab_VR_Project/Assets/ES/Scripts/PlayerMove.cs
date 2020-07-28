using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어 이동시키기
    // 필요속성 
    // 방향, 속도
    public float moveSpeed = 10.0f;

    // CharacterController
    CharacterController cc;

    // 플레이어에 1인칭 카메라 탑재

    // 플레이어가 아이템을 만지면, getTrigger하면 아이템이 눈앞으로 이동해 온다.
    // 아이템카메라
    //public GameObject itemCamera;
    //bool itemSelect = false;
    // 플레이어가 아이템을 앞뒤좌우로 돌려본다.

    void Start()
    {
        // Player 게임 오브젝트 CharacterController 컴포넌트 가져오기
        cc = GetComponent<CharacterController>();


    }

    float yVelocity = 10;
    void Update()
    {
        // 사용자 키 입력에 따라 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
        dir.y = 0;
        transform.position -= Vector3.down*yVelocity*Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.fixedDeltaTime);

    }


}
