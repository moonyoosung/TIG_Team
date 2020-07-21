using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_PlayerMove : MonoBehaviour
{
    // 이동속도
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        transform.position += dir * speed * Time.deltaTime;
    }
}
