using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_PlayerClick : MonoBehaviour
{
    public Image pointer;
    // 비밀번호를 담을 변수
    string[] password = new string[4];
    public int[] answerPW = { 1, 2, 3, 4 };
    // 물건 집을 위치
    public Transform grapPoint;
    // 잡은 물건을 저장할 변수
    GameObject grapObj;
    int idx = 0;

    MYS_TimeMachine TM;
    public MYS_KeypadNumber kn;

    void Start()
    {
        TM = GameObject.FindGameObjectWithTag("TM").GetComponent<MYS_TimeMachine>();
    }

    void Update()
    {

        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();
        // 키보드의 왼쪽 ctrl 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 레이를 쏴서 부딪힌 녀석이 있다면
            if (Physics.Raycast(ray, out hit))
            {
                //Keypad제어 함수
                OnClickKeyPad(hit);
                //버튼클릭제어 함수
                OnClickButton(hit);

            }
        }
        // 왼쪽 F키를 눌렀을 때
        if (Input.GetKey(KeyCode.F))
        {
            //레이를 쏴서 부딪힌 녀석의 레이어가 item이라면
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    //열쇠를 grappoint로 옮긴다.
                    hit.transform.position = grapPoint.position;
                    //열쇠의 중력값을 꺼준다.
                    hit.transform.GetComponent<Rigidbody>().useGravity = false;
                    //잡은 오브젝트 정보 저장
                    GrapingObj(hit.transform.gameObject);
                }
            }
        }
        // F 버튼이 떼지면
        if (Input.GetKeyUp(KeyCode.F))
        {
            // 잡고 있는 물건의 중력값을 켜준다.
            PutDownGrapObj();
        }

    }

    private void OnClickButton(RaycastHit hit)
    {
        if (hit.transform.gameObject.name.Contains("Button"))
        {
            TM.btOrizinPos = hit.transform.position;
            //버튼 애니메이션
            iTween.MoveTo(hit.transform.gameObject, iTween.Hash(
                "position", hit.transform.position + hit.transform.up * -0.05f,
                "speed", 0.1f,
                "easetype", iTween.EaseType.linear,
                "oncompletetarget", gameObject,
                "oncomplete", "OnCompleteButtonAnim"));
        }
    }
    //버튼 클릭 후 애니메이션 호출 함수
    public void OnCompleteButtonAnim()
    {
        print("버튼애니메이션 끝");
        if (TM.fuel)
        {
            MYS_DoorFrame.Instance.state = MYS_DoorFrame.DoorFrameState.Close;
            MYS_DoorFrame.Instance.TmMove = true;
            TM.led.GetComponent<Light>().color = Color.white;
        }

        TM.ButtonReset();
    }

    private void PutDownGrapObj()
    {
        if (grapObj)
        {
            grapObj.GetComponent<Rigidbody>().useGravity = true;
            grapObj.transform.parent = null;
        }
    }

    private void GrapingObj(GameObject obj)
    {
        if (!obj)
        {
            return;
        }
        obj.transform.parent = transform;
        grapObj = obj;

    }

    private void OnClickKeyPad(RaycastHit hit)
    {
        //만약 부딪힌 녀석의 이름에 Keypad가 있다면
        if (hit.transform.gameObject.name.Contains("Keypad"))
        {
            //녀석의 이름을 가져와서 Password에 넣는다.
            string[] divisionName = hit.transform.gameObject.name.Split('_');
            //idx예외처리
            if (idx >= 4)
            {
                print("확인을 눌러주세요");
            }
            else
            {
                password[idx] = divisionName[1];
                //KeypadNumber의 이미지를 바꾸어준다.
                kn.ChangeKeypadNum(int.Parse(password[idx]), idx);
                print(password[idx]);
                idx++;
            }
        }
        else if (hit.transform.gameObject.name.Contains("Enter"))
        {
            int count = 0;
            //정답 패스워드와 비교하여 정답or오답을 출력한다.
            for (int i = 0; i < answerPW.Length; i++)
            {
                //만약 정답과 입력 숫자가 다르면
                if (answerPW[i] != int.Parse(password[i]))
                {
                    print("오답입니다.");
                    idx = 0;
                    kn.ReSetKeypadNum();
                    break;
                }
                else
                {
                    count++;
                }

            }
            if (count == 4)
            {
                MYS_DoorFrame.Instance.state = MYS_DoorFrame.DoorFrameState.Open;
                print("정답입니다.");
                idx = 0;
                kn.ReSetKeypadNum();
            }
        }
    }

    public void InitPassWord()
    {
        for (int i = 0; i < password.Length; i++)
        {
            password[i] = "-";
        }
    }
}
