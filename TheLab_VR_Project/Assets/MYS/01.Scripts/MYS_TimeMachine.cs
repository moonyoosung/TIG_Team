using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_TimeMachine : MonoBehaviour
{
    public enum TMState
    {
        Idle,
        Move
    }
    public TMState state;

    //필요속성 : 플레이어, 이동 위치
    GameObject player;
    public Transform[] TMPos;
    public float moveTime=2f;
    public GameObject led;
    public GameObject button;
    public Vector3 btOrizinPos;
    //현재시간
    float currentTime = 0;

    // 타임머신의 연료 통안에 들어갔는지 판단한다.
    // 필요속성 : 연료여부
    public bool fuel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        state = TMState.Idle;
        led.GetComponent<Light>().color = Color.red;
        btOrizinPos = button.transform.position;
    }


    void Update()
    {
        switch (state)
        {
            case TMState.Idle:
                break;
            case TMState.Move:
                TMMove();
                break;
        }
    }
    //타임머신이 작동되면 플레이어를 자식으로 넣고 흔들리다가 이동한다.

    private void TMMove()
    {
        currentTime += Time.deltaTime;
        if(currentTime > moveTime)
        {
            state = TMState.Idle;
            currentTime = 0;
            MoveToMap();
        }
    }

    //타임머신이 몇번재 맵의 TMPOS로 이동할지 결정해주는 함수
    private void MoveToMap()
    {
        player.GetComponent<CharacterController>().enabled = false;
        transform.position = TMPos[1].position;
        player.GetComponent<CharacterController>().enabled = true;
        //연료 초기화
        fuel = false;
        //이동상태 초기화
        MYS_DoorFrame.Instance.TmMove = false;
        //led 초기화
        led.GetComponent<Light>().color = Color.red;
        MYS_DoorFrame.Instance.state = MYS_DoorFrame.DoorFrameState.Open;
        PlayerChildIdent(false);
    }

    // 플레이어를 자식으로 넣을지 말지 결정하는 함수
    public void PlayerChildIdent(bool id)
    {
        if (id)
        {
            player.transform.parent = transform;
        }
        else
        {
            player.transform.parent = null;
        }
    }

    public void ButtonReset()
    {
        iTween.MoveTo(button, iTween.Hash(
    "position", btOrizinPos,
    "speed", 0.1f,
    "easetype", iTween.EaseType.linear,
    "oncompletetarget", gameObject,
    "oncomplete", "OnCompleteButtonAnim"));
    }
}
