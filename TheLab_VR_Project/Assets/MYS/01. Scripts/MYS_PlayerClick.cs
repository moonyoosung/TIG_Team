using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MYS_PlayerClick : MonoBehaviour
{
    public Image pointer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();
        // 마우스를 왼쪽 클릭 했을 때 
        
        if (Input.GetMouseButtonDown(0))
        {
            // 레이를 쏴서 부딪힌 녀석이 있다면
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name.Contains("Ball"))
                {
                    pointer.color = Color.red;
                }
                else if (hit.transform.gameObject.name.Contains("Box"))
                {
                    pointer.color = Color.blue;
                }
                else if (hit.transform.gameObject.name.Contains("Can"))
                {
                    pointer.color = Color.green;
                }
                else
                {
                    pointer.color = Color.white;
                }
            }
        }
    }
}
