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

    bool grapItem;

    void Start()
    {
        TM = GameObject.FindGameObjectWithTag("TM").GetComponent<MYS_TimeMachine>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {

        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();
        // 키보드의 왼쪽 ctrl 키를 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 레이를 쏴서 부딪힌 녀석이 있다면
            if (Physics.Raycast(ray, out hit))
            {
                //Keypad제어 함수
                OnClickKeyPad(hit);
                //버튼클릭제어 함수
                OnClickButton(hit);
                //캐비넷제어 함수
                OnClickCabinet(hit);
            }
        }
        // 왼쪽 F키를 눌렀을 때
        if (Input.GetMouseButton(0))
        {
            //레이를 쏴서 부딪힌 녀석의 레이어가 item이라면
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item") && !grapItem)
                {
                    //열쇠를 grappoint로 옮긴다.
                    hit.transform.position = grapPoint.position;
                    //열쇠의 중력값을 꺼준다.
                    hit.transform.GetComponent<Rigidbody>().useGravity = false;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;

                    //잡은 오브젝트 정보 저장
                    GrapingObj(hit.transform.gameObject);
                    if (hit.transform.gameObject.tag == "Possesion")
                    {
                        //인벤토리에 저장
                        MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
                    }
                }
            }
        }
        // F 버튼이 떼지면
        if (Input.GetMouseButtonUp(0) && grapItem)
        {
            // 잡고 있는 물건의 중력값을 켜준다.
            PutDownGrapObj();
        }

    }

    private void OnClickCabinet(RaycastHit hit)
    {
        if (hit.transform.gameObject.name.Contains("Cabinet"))
        {
            if (hit.transform.GetComponentInParent<MYS_Cabinet>().keyopen)
            {
                hit.transform.GetComponentInParent<MYS_Cabinet>().MoveCabinet();
            }
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
            grapItem = false;
            grapObj.GetComponent<Rigidbody>().useGravity = true;
            grapObj.GetComponent<Rigidbody>().isKinematic = false;
            grapObj.transform.parent = null;
        }
    }
    float angleX;
    float angleY;
    private void GrapingObj(GameObject obj)
    {
        if (!obj)
        {
            return;
        }
        grapItem = true;
        obj.transform.parent = transform;
        grapObj = obj;
        // 잡은 아이템의 회전을 고정
        //float x = Input.GetAxis("Mouse X");
        //float Y = Input.GetAxis("Mouse Y");

        //angleX += x * 100f * Time.deltaTime;
        //angleY += Y * 100f * Time.deltaTime;
        //obj.transform.localEulerAngles = new Vector3(-angleY, angleX, 0);
        obj.transform.localEulerAngles = Vector3.zero;

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
