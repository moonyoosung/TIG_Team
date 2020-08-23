using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_LeftBlock : MonoBehaviour
{
    public MYS_PlayerClick pc;
    public CheckBlocks cb;
    bool corutineState;
    // 블럭이 퍼즐판에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuzzlePlane")
        {
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
            pc.PutDownGrapObj();
            //transform.gameObject.layer = LayerMask.NameToLayer("Default");
            //print("퍼즐 충돌");
            if (!corutineState)
            {
                StartCoroutine(SetPos());
            }
        }
    }
    IEnumerator SetPos()
    {
        corutineState = true;
        yield return new WaitForFixedUpdate();
        //gameObject.transform.position = cb.linePos.position;
        //print("퍼즐 이동 후");
        //transform.forward = cb.linePos.forward;
        //transform.up = cb.linePos.up;
        cb.AllCheck();
        corutineState = false;
    }
}
