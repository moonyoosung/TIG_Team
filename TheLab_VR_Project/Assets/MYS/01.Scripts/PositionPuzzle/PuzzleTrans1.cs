using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrans1 : MonoBehaviour
{
    public int index = 1;
    public CheckPuzzle cp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Doll")
        {
            //print("퍼즐위치");
            // 배열 위치에 인덱스값 넘겨줘야함
            cp.CheckInit(index, other.gameObject.transform.GetComponent<Pos_PuzzleIndex>().value);

        }
    }
}
