using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_PlayerMove : MonoBehaviour
{

    // 이동속도
    public float speed = 10f;
    public float gravity = -10f;
    public float jumpPower = 1f;
    public float rotSpeed = 30f;
    bool rotState;
    float yVelocity;
    CharacterController cc;
    int jumpCount;

    public GameObject telePortMarker;
    public Transform[] controllerPos;
    Material markerMat;
    bool movePossible;
    void Start()
    {
#if EDITOR
        cc = GetComponent<CharacterController>();
#elif VR

        markerMat = telePortMarker.GetComponent<MeshRenderer>().material;
        telePortMarker.SetActive(false);
#endif
    }

    void Update()
    {
#if EDITOR
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir= Camera.main.transform.TransformDirection(dir);
        yVelocity += gravity * Time.deltaTime;
        transform.forward = Camera.main.transform.forward;

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            jumpCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount ==0)
        {
            yVelocity = jumpPower;
            jumpCount++;
        }
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
#elif VR
        // 위치이동
        Teleport();
        // 플레이어 회전
        PlayerRotate();
#endif
    }

    private void PlayerRotate()
    {
        // 만익 엄지버튼의 axis값이 들어온다면
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).magnitude != 0 && rotState)
        {
            // 입력값
            Vector2 playerRot = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
            float dir = 0f;
            if (playerRot.x < 0f)
            {
                dir = -1f;
            }
            else if (playerRot.x > 0f)
            {
                dir = 1f;
            }
            float angleY = transform.eulerAngles.y + dir * rotSpeed;

            transform.eulerAngles = new Vector3(0, angleY, 0);
            rotState = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).magnitude == 0)
        {
            rotState = true;
        }

    }

    private void Teleport()
    {
        // 1. 만일 엄지버튼을 누르고 있는 상태라면...
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).magnitude != 0)
        {
            // 2. 가리키고 있는 방향에 지면에 이동 가능한지 여부를 체크한다.
            Ray LRay = new Ray(controllerPos[0].position, controllerPos[0].forward);
            RaycastHit hitInfo;
            // 레이어부분
            int groundLayer = LayerMask.NameToLayer("Ground");
            int WallLayer = LayerMask.NameToLayer("Wall");
            int UILayer = LayerMask.NameToLayer("UI");
            int checkMask = 1 << groundLayer | 1 << UILayer;
            // 텔레포트 마커방향설정
            Vector2 markerDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
            Vector3 dir = new Vector3(markerDir.x, 0, markerDir.y);
            dir = controllerPos[0].TransformDirection(dir);
            dir.y = 0;

            if (Physics.Raycast(LRay, out hitInfo, Mathf.Infinity, 1 << groundLayer))
            {
                // 2-1. 만일, 이동 불가능한 곳이면 마커를 붉은색으로 표시하고, 이동 가능한 곳이면 마커를 푸른색으로 표시하겠다.(overlapSphere)
                telePortMarker.SetActive(true);
                telePortMarker.transform.position = hitInfo.point + dir + hitInfo.normal * 0.1f;

                Collider[] cols = Physics.OverlapCapsule(hitInfo.point, hitInfo.point + new Vector3(0, 2, 0), 0.5f, ~checkMask);
                if (cols.Length > 0)
                {
                    markerMat.color = Color.red;
                    movePossible = false;
                }
                else
                {
                    markerMat.color = Color.blue;
                    movePossible = true;
                }
            }
        }

        // 3. 만일, 마커가 푸른색일 때 누르고 있던 인덱스 트리거를 놓으면
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).magnitude == 0)
        {
            // 4. 만일, 이동 가능한 지역이었다면... 그 위치로 이동하겠다.
            if (movePossible)
            {
                transform.position = telePortMarker.transform.position + new Vector3(0,transform.localScale.y,0);
            }
            // 5. 이동 불가능한 지역이엇다면 마커만 비활성화한다.

            telePortMarker.SetActive(false);

        }
    }
}
