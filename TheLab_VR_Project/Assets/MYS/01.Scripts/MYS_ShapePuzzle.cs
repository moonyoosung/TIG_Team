using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_ShapePuzzle : MonoBehaviour
{
    // 퍼즐을 검사할 배열
    public GameObject[] puzzleIdx;
    // 각 배열별 가지고 있는 데이터는 correct
    void Start()
    {
        
    }

    void Update()
    {
        //만약 인덱스 [0,1,2] 번의 correct가 true
        //인덱스 [4,7] 번의 correct 가 true
        //인덱스 [5,6,8,9] 번의 correct가 true
        //정답이다.
        print("정답입니다.");
    }
}
