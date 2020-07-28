using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_PuzzleIdx : MonoBehaviour
{
    public bool correct;
    //나한테 충돌한게 있으면 1 없으면 1을 던져준다.
    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name); 
        if(other.gameObject.name == "0" && other.gameObject.name == "1"&& other.gameObject.name == "2")
        {
            print("라인블럭 맞음");
        }
    }
}
