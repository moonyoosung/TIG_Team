using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class CheckPuzzle : MonoBehaviour
{
    // 정답배열
    public int[] answer = { 3, 1, 2 };
    // 입력배열
    public int[] check = new int[3];
    public bool result;
    public Animation reward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (result)
        {
            reward.Play();
            result = false;
        }
    }
    public void CheckInit(int index, int value)
    {
        //print(index + "번째 값 : " + value);
        check[index] = value;
        CheckAnswer();
    }
    public void CheckAnswer()
    {
        for (int i = 0; i < check.Length; i++)
        {
            result = true;
            if (answer[i] != check[i])
            {
                result = false;
            }
        }
    }
}
