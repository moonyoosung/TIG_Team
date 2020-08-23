using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MYS_TimeMachine : MonoBehaviour
{
    public enum TMState
    {
        Idle,
        Move
    }
    public TMState state;
    public Transform playerPos;
    //필요속성 : 플레이어, 이동 위치
    GameObject player;
    public Transform[] TMPos;
    public float moveTime = 2f;
    public GameObject led;
    public GameObject button;
    public Vector3 btOrizinPos;
    Vector3 orizinPos;
    //현재시간
    float currentTime = 0;
    //맵인덱스
    int mapIdx = 0;

    // 타임머신의 연료 통안에 들어갔는지 판단한다.
    // 필요속성 : 연료여부
    public bool fuel;
    //public GameObject number1clock;
    public GameObject water;
    public MYS_TimePuzzle TP;
    public GameObject docLetter;
    public MYS_Timer timer;
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
                if (Input.GetKeyDown(KeyCode.T))
                {
                    fuel = true;
                    MYS_DoorFrame.Instance.TmMove = true;
                    MYS_DoorFrame.Instance.state = MYS_DoorFrame.DoorFrameState.Close;
                }
                break;
            case TMState.Move:
                TMMove();
                break;
        }
    }

    private void TMMove()
    {
        currentTime += Time.deltaTime;
        //타임머신이 작동되면 플레이어를 자식으로 넣고 흔들리다가 이동한다.
        if (currentTime > moveTime)
        {
            state = TMState.Idle;
            currentTime = 0;
            transform.position = orizinPos;
            MoveToMap();
        }
    }
    public float shakeSize = 0.1f;
    public IEnumerator ShakeTimeMachineActive()
    {
        Vector3 localPos = transform.position;
        // - 현재시간
        float shakeTime = 0f;
        while (shakeTime <= moveTime - 0.1f)
        {
            shakeTime += Time.deltaTime;
            // 3축
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeSize;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeSize;
            float z = UnityEngine.Random.Range(-1f, 1f) * shakeSize;

            transform.position = localPos + new Vector3(x, y, z);
            yield return null;
        }
        // 제자리로
        //transform.position = localPos;
    }

    //타임머신이 몇번재 맵의 TMPOS로 이동할지 결정해주는 함수
    private void MoveToMap()
    {
        //만약 Time퍼즐을 맞추었다면 처음시점으로 돌아가고
        if (TP.result)
        {
            mapIdx = 0;
            if (!docLetter.activeSelf)
            {
                MYS_GameManager.Instance.OnPlayerDieDoctor();
            }
        }
        else
        {
            //이동할 맵을 변경하고
            mapIdx++;
        }
        if(mapIdx == 1)
        {
            timer.transform.gameObject.SetActive(true);
            timer.timerActive = true;
        }
        if (mapIdx > 6)
        {
            //플레이어 죽음
            MYS_GameManager.Instance.OnPlayerDieDoctor();            
        }

        //플레이어를 가져간다.
        PlayerChildIdent(true);

        //player.GetComponent<CharacterController>().enabled = false;
        transform.position = TMPos[mapIdx].position;
        orizinPos = transform.position;
        player.transform.position = playerPos.position;
        
        //player.GetComponent<CharacterController>().enabled = true;

        if (MYS_Inventory.Instance.inven.Contains(MYS_Clock.Instance.gameObject))
        {
            int nextYear;
            string[] divide = TMPos[mapIdx].parent.gameObject.name.Split('_');
            string[] divide2 = divide[1].Split('y');
            nextYear = int.Parse(divide2[0]);
            print("이동년도 : " + nextYear);
            MYS_Clock.Instance.CheckTimer(nextYear);
        }
        //연료 초기화
        fuel = false;
        //이동상태 초기화
        MYS_DoorFrame.Instance.TmMove = false;
        //led 초기화
        led.GetComponent<Light>().color = Color.red;
        MYS_DoorFrame.Instance.state = MYS_DoorFrame.DoorFrameState.Open;
        PlayerChildIdent(false);

        //물차오르는 맵일때
        if (mapIdx == 3)
        {
            WaterUp();
        }
        //물차오르는 다음맵
        if (mapIdx == 4)
        {
            water.SetActive(false);
            MYS_Water.Instace.waterInUI.SetActive(false);
        }
    }

    // 플레이어를 자식으로 넣을지 말지 결정하는 함수
    public void PlayerChildIdent(bool id)
    {
        if (id)
        {
            player.transform.parent = transform;
            ////인벤토리의 아이템을 가져간다.
            //for (int i = 0; i < MYS_Inventory.Instance.inven.Count; i++)
            //{
            //    MYS_Inventory.Instance.inven[i].transform.parent = transform;
            //}
        }
        else
        {
            player.transform.parent = null;
            //인벤토리의 아이템을 가져간다.
            //for (int i = 0; i < MYS_Inventory.Instance.inven.Count; i++)
            //{
            //    MYS_Inventory.Instance.inven[i].transform.parent = null;
            //}
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

    // 서서히 차오르는 물
    public void WaterUp()
    {
        water.GetComponent<AudioSource>().clip = MYS_SoundManager.Instance.SFX_Water;
        water.GetComponent<AudioSource>().volume = 0.1f;
        water.GetComponent<AudioSource>().loop = true;
        water.GetComponent<AudioSource>().Play();

        // 서서히 물이 차오른다.
        iTween.MoveTo(water, iTween.Hash(
                "position", transform.position + new Vector3(0, 5f, 0),
                "speed", 0.1f,
                "easetype", iTween.EaseType.linear));
        // 캐릭터가 물에 잠기면 UI를 켜준다.
        // 
    }
}
