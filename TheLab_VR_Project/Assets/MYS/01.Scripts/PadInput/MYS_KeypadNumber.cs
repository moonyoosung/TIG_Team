using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_KeypadNumber : MonoBehaviour
{
    //플레이어가 키패드의 번호를 클릭하면
    // password배열의 0번째 부터 숫자가 들어가고
    // 필요속성
    // 숫자 이미지들을 가지고 있을 배열, UI이미지들
    public Sprite[] numbers;
    public Image[] keypadNum;
    private void Start()
    {
        ReSetKeypadNum();
    }
    // 해당 이미지를 이미지 번호에 맞는 이미지 번호를 바꾸어 준다
    public void ChangeKeypadNum(int num, int passidx)
    {
        // 만약 num이 1이라면 numbers의[0]번째를 선택하여 keypadnum의 이미지로 넣어준다.
        keypadNum[passidx].sprite = numbers[num - 1];
    }
    // 번호를 초기화
    public void ReSetKeypadNum()
    {
        for (int i = 0; i < keypadNum.Length; i++)
        {
            keypadNum[i].sprite = numbers[9];
        }
    }
}
