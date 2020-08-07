using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아웃라인 이펙트를  껏다 켰다 하기 위한 스크립트
public class MYS_KeyOutLine : MonoBehaviour
{
    public Outline clickEffect;
    public Outline clickEffect2;

    public bool outlineState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineState)
        {
            clickEffect.eraseRenderer = false;
            clickEffect2.eraseRenderer = false;
        }
        else
        {
            clickEffect.eraseRenderer = true;
            clickEffect2.eraseRenderer = true;
        }
    }
}
