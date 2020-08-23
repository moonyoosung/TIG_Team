using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 문을 열고 닫을 동작을 할 스크립트
public class MYS_DoorFrame : MonoBehaviour
{
    static public MYS_DoorFrame Instance;
    private void Awake()
    {
        Instance = this;
    }
    public enum DoorFrameState
    {
        Idle,
        Close,
        Open
    }
    public DoorFrameState state;
    public GameObject doorFrame;
    public float doorSpeed = 5f;
    public bool TmMove = false;
    float angleX = 0;
    MYS_TimeMachine TM;
    // 만약 문에 달려있는 키패드의 비밀번호를 맞춘다면
    // doorClose함수에서는 y회전값을 0도까지 작아지게 한다.
    void Start()
    {
        state = DoorFrameState.Idle;
        TM = GetComponent<MYS_TimeMachine>();
    }

    void Update()
    {
        CheatCode();

        switch (state)
        {
            
            case DoorFrameState.Idle:
                //아무런 동작을 하지 않는다.
                break;
            case DoorFrameState.Open:
                if(angleX <= -60)
                {
                    state = DoorFrameState.Idle;                    
                    break;
                }
                // doorFrame의 x 회전값이 -40도까지 doorSpeed만큼 작아
                if (angleX > -60)
                {
                    angleX -= doorSpeed * Time.deltaTime;
                    doorFrame.transform.localEulerAngles = new Vector3(angleX, 0, 0);
                }
                break;
            case DoorFrameState.Close:
                if (angleX >= 0)
                {
                    state = DoorFrameState.Idle;
                    if (TmMove)
                    {
                        // 타임머신에게 이동하라 알려준다.
                        StartCoroutine(TM.GetComponent<MYS_TimeMachine>().ShakeTimeMachineActive());
                        TM.GetComponent<MYS_TimeMachine>().state = MYS_TimeMachine.TMState.Move;
                    }
                    break;
                }
                if (angleX < 0)
                {
                    angleX += doorSpeed * Time.deltaTime;
                    doorFrame.transform.localEulerAngles = new Vector3(angleX, 0, 0);
                }
                break;
        }
    }

    private void CheatCode()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            state = DoorFrameState.Close;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            state = DoorFrameState.Open;
        }
    }
}
