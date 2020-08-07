using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_BlocksOutline : MonoBehaviour
{
    Outline blockoutline;
    public bool outlineState;
    // Start is called before the first frame update
    void Start()
    {
        blockoutline = GetComponent<cakeslice.Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineState)
        {
            blockoutline.eraseRenderer = false;
        }
        else
        {
            blockoutline.eraseRenderer = true;
        }
    }
}
