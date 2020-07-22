using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_PlayerClick : MonoBehaviour
{
    public Image pointer;
    // 비밀번호를 담을 변수
    string[] password = new string[4];
    int[] answerPW = { 1, 2, 3, 4 };

    int idx = 0;
    void Start()
    {

    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();
        // 마우스를 왼쪽 클릭 했을 때 

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 레이를 쏴서 부딪힌 녀석이 있다면
            if (Physics.Raycast(ray, out hit))
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
                            break;
                        }
                        else
                        {
                            count++;
                        }

                    }
                    if (count == 4)
                    {
                        print("정답입니다.");
                        idx = 0;
                    }
                }
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
