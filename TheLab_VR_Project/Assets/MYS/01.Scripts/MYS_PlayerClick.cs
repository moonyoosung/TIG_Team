using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Steps;

/*
 * 버튼은 컨트롤러 충돌로 입력
 * 오브젝트 선택은 가져오기
 */
public class MYS_PlayerClick : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;
    public LineRenderer drawLine;
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
    public MYS_AlphaPad ap;
    MYS_CamRotate cm;
    bool grapItem;


    void Start()
    {
        TM = GameObject.FindGameObjectWithTag("TM").GetComponent<MYS_TimeMachine>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        cm = Camera.main.transform.GetComponent<MYS_CamRotate>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(RightHand.position, 0.1f);
        Gizmos.DrawWireSphere(LeftHand.position, 0.1f);

    }
    void Update()
    {

#if EDITOR
        //메인카메라에서 레이캐스트를 발사
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit = new RaycastHit();
                // 왼쪽 마우스 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0) && !grapItem)
        {
                    // 레이를 쏴서 부딪힌 녀석이 있다면
            if (Physics.Raycast(ray, out hit))
            {
                    OnClickActives(hit);
            }
        }
        // 아웃라인 검출하기
        if (Physics.Raycast(ray, out hit))
        {
            OutLineActiveControl(hit);
        }
#elif VR
        //컨트롤러에서 레이캐스트를 발사
        Ray rRay = new Ray(RightHand.position, RightHand.forward * 20f);
        //Ray lRay = new Ray(LeftHand.position, LeftHand.forward * 20f);
        RaycastHit hit = new RaycastHit();
        RaycastHit outlineHit = new RaycastHit();

        // 앞방향 아웃라인 검출하기
        if (Physics.Raycast(rRay, out outlineHit))
        {
            OutLineActiveControl(outlineHit);
        }

        // 손가까이에 오브젝트들이 있는지 검사
        CheckHandObject();

        // 컨트롤러 트리거 버튼을 눌렀을 때 
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Collider[] colls = Physics.OverlapSphere(RightHand.position, 0.1f);
            if (colls.Length > 0)
            {
                // 감지된 물체에 레이를 쏘고 
                if (Physics.Raycast(RightHand.position, colls[0].transform.position - RightHand.position, out hit))
                {
                    //한번만 호출되면 되는 동작들
                    OnClickActives(hit);
                }

            }
        }
        // 오른쪽 트리거 버튼을 누르면
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // 앞방향으로도 레이를 쏜다.
            if (Physics.SphereCast(rRay, 0.1f, out hit))
            {
                OnClickActives(hit);
            }
        }
#endif

#if EDITOR
        //왼쪽 마우스 버튼을 누르고 있을 때
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
            OnButtonStayDown(hit);
            }
        
        }
#elif VR
        //핸드 버튼이 누르고 있을 때
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Collider[] colls = Physics.OverlapSphere(RightHand.position, 0.1f);
            if (colls.Length > 0)
            {
                // 감지된 물체에 레이를 쏘고 
                if (Physics.Raycast(RightHand.position, colls[0].transform.position - RightHand.position, out hit))
                {
                    //버튼이 눌리고 있을 때 실행되는 함수
                    OnButtonStayDown(hit);
                }
            }
        }
        //트리거 버튼을 누르고 잇으면
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Physics.SphereCast(rRay, 0.1f, out hit))
            {
                OnButtonStayDown(hit);
            }
        }
#endif

#if EDITOR
        //왼쪽 마우스 버튼이 놓으면
        if (Input.GetMouseButtonUp(0) && grapItem)
        {
            //버튼이 놓아지면 한번 호출되는 함수
            OnButtonUp(hit);
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

#elif VR
        //트리거 버튼을 놓았을 때
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {

            //버튼이 놓아지면 한번 호출되는 함수
            OnButtonUp(hit);
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            //버튼이 놓아지면 한번 호출되는 함수
            OnButtonUp(hit);
        }
#endif
    }

    private void OnButtonUp(RaycastHit hit)
    {
        // 잡고 있는 물건의 중력값을 켜준다.
        if (!grapObj)
        {

        }
        else if (grapObj.tag.Contains("Book"))
        {
            grapObj.SetActive(false);
        }
        else
        {
            PutDownGrapObj();
        }
        grapItem = false;
    }

    private void OnButtonStayDown(RaycastHit hit)
    {
        //레이를 쏴서 부딪힌 녀석의 레이어가 item이라면
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item") && !grapItem)
        {
#if EDITOR
            //열쇠를 grappoint로 옮긴다.
            hit.transform.position = grapPoint.position;
#elif VR
            //잡은 물체를 컨트롤러에 위치시킨다.
            ChooseController(hit);
#endif
            if (hit.transform.GetComponent<Rigidbody>())
            {
                //열쇠의 중력값을 꺼준다.
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                hit.transform.GetComponent<Rigidbody>().isKinematic = true;
            }

            //잡은 오브젝트 정보 저장
            GrapingObj(hit.transform.gameObject);

            #region"인벤토리 저장"
            //인벤토리에 저장
            if (hit.transform.gameObject.tag == "Possesion")
            {
                MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.tag == "Book")
            {
                MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.tag == "Letter")
            {
                MYS_Inventory.Instance.SaveItemToInven(hit.transform.gameObject);
            }
            #endregion
        }
    }

    private void OnClickActives(RaycastHit hit)
    {
        //Keypad제어 함수
        OnClickKeyPad(hit);
        //Alphapad제어 함수
        OnClickAlphaPad(hit);
        //버튼클릭제어 함수
        OnClickButton(hit);
        //캐비넷제어 함수
        OnClickCabinet(hit);
        //티비 제어
        OnClickTvButton(hit);
        //퍼즐판 입력 함수
        OnClickTransitionPuzzle(hit);
        if (hit.transform.gameObject.tag == "Lever")
        {
            print(hit.transform.GetComponentInParent<MYS_Lever>().gameObject.name);
            hit.transform.GetComponentInParent<MYS_Lever>().OnCheckLever();
        }
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            hit.transform.GetComponentInParent<MYS_WirteLetter>().writeOn = true;
        }
    }

    private void CheckHandObject()
    {

    }

    private void ChooseController(RaycastHit hit)
    {
        hit.transform.position = RightHand.transform.position;
        hit.transform.parent = RightHand;
    }

    private void OnClickTransitionPuzzle(RaycastHit hit)
    {
        if (hit.transform.tag.Contains("TransitionPuzzle") && hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().outPuzzleState == false)
        {
            hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().MovePuzzlePlane();
        }
        else if (hit.transform.tag.Contains("TransitionPuzzle") && hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().outPuzzleState == true)
        {
            hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().ClosePuzzlePlane();

        }
        if (hit.transform.tag.Contains("TransPuzzleIdx") && hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().moveState)
        {
            string[] idx = hit.transform.gameObject.name.Split('_');
            print("오브젝트 번호 : " + idx[1]);
            hit.transform.GetComponentInParent<MYS_TransitionPuzzle>().FindNearIdx(int.Parse(idx[1]));
        }
    }

    private void OnClickTvButton(RaycastHit hit)
    {
        if (hit.transform.gameObject.name.Contains("B_TV"))
        {
            hit.transform.GetComponent<MYS_TVButton>().TVButton();
        }
    }

    private void OnClickAlphaPad(RaycastHit hit)
    {
        //만약 부딪힌 녀석의 태그에 AplhaPad가 있다면
        if (hit.transform.gameObject.tag == "AlphaPad")
        {
            //녀석의 이름을 가져와서 index값을 던져준다.
            string[] divisionName = hit.transform.gameObject.name.Split('_');

            ap.OnClickPadNumber(int.Parse(divisionName[1]));
        }
    }

    private void OutLineActiveControl(RaycastHit hit)
    {
        drawLine.SetPosition(0, RightHand.position);
        drawLine.SetPosition(1, hit.point);
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
        else
        {

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
        if (grapObj != null && grapObj.tag.Contains("Block"))
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
        //obj.transform.parent = transform;
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

