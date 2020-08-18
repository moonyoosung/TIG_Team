using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_WirteLetter : MonoBehaviour
{
    public GameObject player;
    public float range = 1f;
    public GameObject writeButton;
    public GameObject text;
    bool buttonActive;
    public bool writeOn;
    public GameObject doctor;
    public GameObject cup;
    public Transform cupPos;
    public GameObject docLetter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        // 플레이어와의 거리가 distance만큼 작아지면 버튼 오브젝트를 킨다.
        if (dis < range && buttonActive == false)
        {
            writeButton.SetActive(true);
            buttonActive = true;
        }
        else
        {
            writeButton.SetActive(false);
        }

        if (buttonActive && !text.activeSelf && writeOn)
        {
            //편지쓰는 애니메이션 실행

            //편지내용 활성화
            text.SetActive(true);

            // 박사를 사라지게 한다.
            doctor.SetActive(false);
            // 컵의 위치를 책상위로 바꾼다.
            cup.transform.position = cupPos.position;
            cup.transform.up = cupPos.up;
            cup.transform.forward = -cupPos.right;
            // 최종 암호문을 활성화한다.
            docLetter.SetActive(true);
        }
    }
}
