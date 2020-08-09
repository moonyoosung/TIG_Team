using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos_PuzzleIndex : MonoBehaviour
{
    public int value = 1;
    public MYS_PlayerClick pc;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PosPuzzle")
        {            
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
            pc.PutDownGrapObj();
            transform.position = other.transform.position;
            transform.up = other.transform.up;
            //print("인형"+other.gameObject.name);
        }
    }
}
