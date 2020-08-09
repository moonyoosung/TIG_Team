using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_TimePuzzle : MonoBehaviour
{
    public bool result;
    public bool[] answer = new bool[43];
    public bool[] table;

    void Start()
    {
        table = new bool[answer.Length];
    }

    void Update()
    {
        
    }
    public void InitTable(int idx, bool value)
    {
        table[idx] = value;
        CheckAnswer();
    }
    public void CheckAnswer()
    {
        for (int i = 0; i < table.Length; i++)
        {
            result = true;
            if(answer[i] != table[i])
            {
                result = false;
                break;
            }
        }
    }
}
