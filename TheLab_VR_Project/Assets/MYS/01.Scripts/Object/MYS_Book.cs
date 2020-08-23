using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자의 입력에 따라 책장을 넘긴다.
public class MYS_Book : MonoBehaviour
{
    public GameObject[] LeftPage;
    public GameObject[] RightPage;
    public int idx = 0;
    bool activeState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if EDITOR
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //만약 인덱스가 3보다 작다면
            if (idx < 3)
            {
                PageRightControl();
                idx++;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (idx > 0)
            {
                PageLeftControl();
                idx--;
            }
        }
#elif VR
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0 && MYS_Inventory.Instance.book&& activeState)
        {
            //만약 인덱스가 3보다 작다면
            if (idx < 3)
            {
                PageRightControl();
                idx++;
            }
            activeState = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < 0 && MYS_Inventory.Instance.book&& activeState)
        {
            if (idx > 0)
            {
                PageLeftControl();
                idx--;
            }
            activeState = false;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x == 0 && activeState ==false)
        {
            activeState = true;
        }

#endif
    }

    private void PageLeftControl()
    {
        //다음 페이지를 킨다.
        LeftPage[idx - 1].SetActive(true);
        RightPage[idx - 1].SetActive(true);

        //전 페이지는 꺼주고
        LeftPage[idx].SetActive(false);
        RightPage[idx].SetActive(false);
    }

    public void PageRightControl()
    {
        //다음 페이지를 킨다.
        LeftPage[idx + 1].SetActive(true);
        RightPage[idx + 1].SetActive(true);

        //전 페이지는 꺼주고
        LeftPage[idx].SetActive(false);
        RightPage[idx].SetActive(false);

    }
}
