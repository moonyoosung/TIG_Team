using cakeslice;
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
    public int[] answerPW = { 6, 5, 4, 9 };
    // 물건 집을 위치
    public Transform grapPoint;
    // 잡은 물건을 저장할 변수
    GameObject grapObj;
    int idx = 0;

    MYS_TimeMachine TM;
    public MYS_KeypadNumber kn;
    MYS_CamRotate cm;
    OutlineEffect outline;
    bool grapItem;

    void Start()
    {
        TM = GameObject.FindGameObjectWithTag("TM").GetComponent<MYS_TimeMachine>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cm = Camera.main.transform.GetComponent<MYS_CamRotate>();
        outline = Camera.main.transform.GetComponent<OutlineEffect>();
    }

    void Update()
    {

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward*10f, Color.blue, 3f);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            OutLineActiveControl(hit);

        }
        // 왼쪽 마우스 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0) && !grapItem)
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
        //왼쪽 마우스 버튼을 눌렀을 때
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(Camera.main.transform.position, hit.point, Color.red, 3f);
                //레이를 쏴서 부딪힌 녀석의 레이어가 item이라면
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item") && !grapItem)
                {
                    //열쇠를 grappoint로 옮긴다.
                    hit.transform.position = grapPoint.position;
                    if (hit.transform.GetComponent<Rigidbody>())
                    {
                        //열쇠의 중력값을 꺼준다.
                        hit.transform.GetComponent<Rigidbody>().useGravity = false;
                        hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    }

                    //잡은 오브젝트 정보 저장
                    GrapingObj(hit.transform.gameObject);

                    if (hit.transform.gameObject.tag == "Possesion")
                    {
                        //인벤토리에 저장
                        MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
                    }else if(hit.transform.gameObject.tag == "Book")
                    {
                        //인벤토리에 저장
                        MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
                    }

                }
            }

        }
        //왼쪽 마우스 버튼이 떼지면
        if (Input.GetMouseButtonUp(0) && grapItem)
        {
            // 잡고 있는 물건의 중력값을 켜준다.
            if (grapObj.tag.Contains("Book"))
            {
                grapObj.SetActive(false);
            }
            else
            {
                PutDownGrapObj();
            }
            grapItem = false;
        }
        if (grapItem)
        {
            //만약 키보드 왼쪽 Alt키를 누르면
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                //카메라 회전을 멈추고 아이템을 회전시킨다.
                cm.enabled = false;
                grapObj.transform.GetComponent<ItemRotate>().enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                //아이템 회전을 멈추고 카메라를 회전시킨다.
                cm.enabled = true;
                grapObj.transform.GetComponent<ItemRotate>().enabled = false;
            }
        }
    }

    private void OutLineActiveControl(RaycastHit hit)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item") && !grapItem)
        {
            hit.transform.GetComponent<MYS_Outline>().outlineState = true;
            hit.transform.GetComponent<MYS_Outline>().currentTime = 0f;
            //if(hit.transform.gameObject.tag == "Possesion")
            //{
            //    hit.transform.GetComponent<MYS_FuelOutline>().outlineState = true;
            //}
            //Component[] lines;
            //lines = hit.transform.GetComponentsInChildren(typeof(cakeslice.Outline));
            //foreach (cakeslice.Outline outline in lines)
            //{
            //    outline.eraseRenderer = false;
            //}
        }
        else if (hit.transform.gameObject.tag == "Keypad" || hit.transform.gameObject.tag == "Enter")
        {
            hit.transform.GetComponent<MYS_Outline>().outlineState = true;
            hit.transform.GetComponent<MYS_Outline>().currentTime = 0f;
        }
        else if (hit.transform.gameObject.name.Contains("Button"))
        {
            hit.transform.GetComponent<MYS_Outline>().outlineState = true;
            hit.transform.GetComponent<MYS_Outline>().currentTime = 0f;
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

    public void PutDownGrapObj()
    {
        if (grapObj)
        {
            grapObj.GetComponent<Rigidbody>().useGravity = true;
            grapObj.GetComponent<Rigidbody>().isKinematic = false;
            grapObj.transform.parent = null;
        }
        if (grapObj.tag.Contains("Block"))
        {
            grapObj.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void GrapingObj(GameObject obj)
    {
        if (!obj)
        {
            return;
        }
        grapItem = true;
        obj.transform.parent = transform;
        grapObj = obj;
    }

    private void OnClickKeyPad(RaycastHit hit)
    {
        //만약 부딪힌 녀석의 이름에 Keypad가 있다면
        if (hit.transform.gameObject.tag == "Keypad")
        {
            //print(hit.transform.position);
            //녀석의 이름을 가져와서 Password에 넣는다.
            string[] divisionName = hit.transform.gameObject.name.Split('_');
            password[idx] = divisionName[1];
            //KeypadNumber의 이미지를 바꾸어준다.
            kn.ChangeKeypadNum(int.Parse(password[idx]), idx);
            idx++;
            //idx예외처리
            if (idx == 4)
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
            //else
            //{
            //    password[idx] = divisionName[1];
            //    //KeypadNumber의 이미지를 바꾸어준다.
            //    kn.ChangeKeypadNum(int.Parse(password[idx]), idx);
            //    idx++;
            //}
        }
        //else if (hit.transform.gameObject.tag == "Enter")
        //{

        //}
    }

    public void InitPassWord()
    {
        for (int i = 0; i < password.Length; i++)
        {
            password[i] = "-";
        }
    }

}
