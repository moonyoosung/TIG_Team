using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_TransitionPuzzle : MonoBehaviour
{
    // 가로 3, 세로 3
    int hCount = 3;
    int vCount = 3;
    // 1. 클릭한 놈의 상, 하, 좌, 우를 체크하여
    // 2. 비어있는 칸이 있다면
    // 3. 비어있는 칸으로 이동해라

    // 인덱스들의 위치
    public Transform[] idxPos;
    // value값을 가지고 있는 게임오브젝트
    public GameObject[] idx;
    // 중복검사를 위한 리스트
    List<int> number = new List<int>();

    // 정답여부
    public bool result;
    public bool moveState = true;
    public bool outPuzzleState;

    Vector3 orizinPos;
    AudioSource audioPlayer;

    public MYS_MoveShelves shelves;
    void Start()
    {
        // 시작하면 퍼즐 위치를 랜덤하게 섞는다.
        Shuffle();
        orizinPos = transform.position;
        moveState = true;
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Shuffle()
    {
        bool state = true;
        for (int i = 0; i < idx.Length; i++)
        {
            // 랜덤한 숫자를 뽑아서
            int ran = UnityEngine.Random.Range(0, idxPos.Length);
            for (int k = 0; k < number.Count; k++)
            {
                state = true;
                // 리스트의 갯수만큼 숫자를 검사해서 겹치는게 있으면 다시뽑고
                if (number[k] == ran)
                {
                    state = false;
                    break;
                }
            }
            // 겹치는게 없으면
            if (state)
            {
                //리스트에 번호를 추가해놓고 위치를 해당 위치로 옮긴다.
                number.Add(ran);
                idx[ran].transform.position = idxPos[i].position;
            }
            else
            {
                //겹치는게 있으면 다시 뽑느다.
                i--;
            }
        }
        //for (int i = 0; i < number.Count; i++)
        //{
        //    print(i + "번째 : " + number[i]);
        //}

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < idx.Length; i++)
            {
                idx[i].transform.position = idxPos[i].transform.position;
                number[i] = i;
            }
        }
    }
    // 각 자리별로 검사
    public void CheckMovePossible(int n, int parse)
    {
        //number리스트의 item이 자리 수 n
        //number리스트의 idx값이 오브젝트 번호 parse
        //게임오브젝트의 순서는 puzzles에 저장
        //n번의 게임오브젝트가 빈공간이라면 이동한다.
        if (idx[number[n]].GetComponent<MYS_TPuzzleIndex>().spaceType == true)
        {
            audioPlayer.clip = MYS_SoundManager.Instance.SFX_SlidePuzzle;
            audioPlayer.volume = 0.3f;
            audioPlayer.Play();

            //이동시킨다.
            float tempX = idx[number[n]].transform.position.x;
            float tempY = idx[number[n]].transform.position.y;
            float tempZ = idx[number[n]].transform.position.z;
            idx[number[n]].transform.position = idx[parse].transform.position;
            //idx[parse].transform.position = new Vector3(tempX, tempY, tempZ);
            print(idx[number[n]] + "과 " + idx[parse] + "바꿈");
            moveState = false;
            iTween.MoveTo(idx[parse], iTween.Hash(
            "position", new Vector3(tempX, tempY, tempZ),
            "speed", 0.5f,
            "easetype", iTween.EaseType.linear,
            "oncompletetarget", gameObject,
            "oncomplete", "OnCompleteMove"));

            // 리스트 수정
            int temp = number.IndexOf(parse);
            int temp2 = number[n];
            number[n] = parse;
            number[temp] = temp2;

            //for (int i = 0; i < number.Count; i++)
            //{
            //    print(i + "번째 : " + number[i]);
            //}
        }
    }
    public void MovePuzzlePlane()
    {
        audioPlayer.clip = MYS_SoundManager.Instance.SFX_moveTransPuzzle;
        audioPlayer.volume = 0.3f;
        audioPlayer.Play();

        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.right*-1f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear,
        "oncompletetarget", gameObject,
        "oncomplete", "OnCompletePlane"));
    }
    public void ClosePuzzlePlane()
    {
        audioPlayer.clip = MYS_SoundManager.Instance.SFX_moveTransPuzzle;
        audioPlayer.volume = 0.3f;
        audioPlayer.Play();
        iTween.MoveTo(gameObject, iTween.Hash(
        "position", transform.position + transform.right * 1f,
        "speed", 0.5f,
        "easetype", iTween.EaseType.linear,
        "oncompletetarget", gameObject,
        "oncomplete", "OnCompleteClosePlane"));
    }
    public void OnCompleteClosePlane()
    {
        outPuzzleState = false;
        if (result)
        {
            //책장을 움직인다.
            shelves.MoveBack();

        }
    }
    public void OnCompletePlane()
    {
        outPuzzleState = true;
    }

    public void OnCompleteMove()
    {
        // 정답과 체크한다.
        for (int i = 0; i < number.Count; i++)
        {
            result = true;
            if(number[i] != i)
            {
                result = false;
                break;
            } 
        }

        moveState = true;
    }


    public void FindNearIdx(int parseIdx)
    {
        int n;
        int temp = number.IndexOf(parseIdx);
        // 오른쪽
        if (temp % hCount < hCount - 1)
        {
            n = temp + 1;
            //print("오른쪽 : " + n);
            CheckMovePossible(n, parseIdx);
        }

        // 왼쪽
        if (temp % hCount > 0)
        {
            n = temp - 1;
            //print("왼쪽 : " + n);
            CheckMovePossible(n, parseIdx);
        }

        // 위
        if (temp / hCount > 0)
        {
            n = temp - vCount;
            //print("위 : " + n);
            CheckMovePossible(n, parseIdx);
        }

        // 아래
        if (temp / hCount < vCount - 1)
        {
            n = temp + vCount;
            //print("아래 : " + n);
            CheckMovePossible(n, parseIdx);
        }
    }
}
