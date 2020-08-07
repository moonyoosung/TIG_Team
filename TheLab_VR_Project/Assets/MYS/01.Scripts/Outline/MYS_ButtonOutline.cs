using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_ButtonOutline : MonoBehaviour
{
    Outline buttonoutline;
    public bool outlineState;
    // Start is called before the first frame update
    void Start()
    {
        buttonoutline = GetComponent<cakeslice.Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineState)
        {
            buttonoutline.eraseRenderer = false;
        }
        else
        {
            buttonoutline.eraseRenderer = true;
        }
    }
}
