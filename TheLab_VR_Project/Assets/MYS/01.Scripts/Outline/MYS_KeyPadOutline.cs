using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_KeyPadOutline : MonoBehaviour
{
    Outline kepad;
    public bool outlineState;
    void Start()
    {
        kepad = GetComponent<cakeslice.Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineState)
        {
            kepad.eraseRenderer = false;
        }
        else
        {
            kepad.eraseRenderer = true;
        }
    }
}
