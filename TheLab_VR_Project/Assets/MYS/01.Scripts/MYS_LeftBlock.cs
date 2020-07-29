using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_LeftBlock : MonoBehaviour
{
    public MYS_PlayerClick pc;
    public CheckBlocks cb;
    // 블럭이 퍼즐판에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuzzlePlane")
        {
            gameObject.transform.position = cb.leftPos.position;
            transform.up = cb.leftPos.up;
            transform.forward = cb.leftPos.forward;
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
            pc.PutDownGrapObj();
            transform.gameObject.layer = LayerMask.NameToLayer("Default");

        }
    }
}
