using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Outline : MonoBehaviour
{
    public Outline[] lines;
    public bool outlineState;
    public float currentTime;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 0.2초마다 검사하여 아웃라인이 켜지고 있는 상태면 꺼준다.
        currentTime += Time.deltaTime;
        if (currentTime > 0.2f)
        {
            if (outlineState)
            {
                outlineState = !outlineState;
            }
            currentTime = 0f;
        }
        // 아웃라인이 켜지면 외각선 지우기를 끄고 아웃라인이 꺼지면 외각선 지우기를 킨다.
        for (int i = 0; i < lines.Length; i++)
        {
            if (outlineState)
            {
                lines[i].enabled = true;
                lines[i].eraseRenderer = false;
            }
            else
            {
                lines[i].eraseRenderer = true;
                lines[i].enabled = false;
            }
        }
        
    }
}
