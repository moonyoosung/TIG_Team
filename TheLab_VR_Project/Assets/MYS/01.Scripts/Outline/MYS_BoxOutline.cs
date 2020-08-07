using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_BoxOutline : MonoBehaviour
{
    public Outline[] boxoutlines;
    public bool outlineState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boxoutlines.Length; i++)
        {
            if (outlineState)
            {
                boxoutlines[i].eraseRenderer = false;
            }
            else
            {
                boxoutlines[i].eraseRenderer = true;
            }
        }
    }
}
