using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MYS_Key : MonoBehaviour
{
    //만약 키의 up방향이 문의 열쇠구멍의 up방향 과 같은 방향으로 들어갔고
    //만약 오른쪽으로 90도만큼 회전했다면
    //문이 열린다.

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject);
    }
}
