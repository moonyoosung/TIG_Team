using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_AlphaPad : MonoBehaviour
{   

    public string[] alphaAnswer = { "T", "E", "G", "L", "D" };
    public string[] keyPadValue = { "S", "N", "D", "E", "V", "L", "C", "K", "I", "G", "T", "M" };
    public string[] inputAlpha = new string[5];
    int idx = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnClickPadNumber(int number)
    {
        // inputAlpha배열에 하나씩 넣는다.
        inputAlpha[idx] = keyPadValue[number-1];
        
        //정답 검사
        if (idx == 4)
        {
            int count = 0;
            //정답 패스워드와 비교하여 정답or오답을 출력한다.
            for (int i = 0; i < alphaAnswer.Length; i++)
            {
                //만약 정답과 입력 숫자가 다르면
                if (alphaAnswer[i] != inputAlpha[i])
                {
                    print("오답입니다.");
                    idx = 0;
                    ResetInput();
                    break;
                }
                else
                {
                    count++;
                }

            }

            if (count == 5)
            {
                print("정답입니다.");
                // 엔딩화면 출력

                idx = 0;
                ResetInput();
            }
        }
        else
        {
            //input인자값 상승
            idx++;
        }

    }

    private void ResetInput()
    {
        for (int i = 0; i < inputAlpha.Length; i++)
        {
            inputAlpha[i] = "";
        }
    }
}
