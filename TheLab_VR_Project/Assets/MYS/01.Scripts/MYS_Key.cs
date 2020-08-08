using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Key : MonoBehaviour
{
    //만약 키의 up방향이 문의 열쇠구멍의 up방향 과 같은 방향으로 들어갔고
    //만약 오른쪽으로 90도만큼 회전했다면
    //문이 열린다.
    GameObject hit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Key"))
        {
            //게임오브젝트의 레이어를 바꾼다.
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            //transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.forward = other.gameObject.transform.up;
            hit = other.gameObject;

            // 키돌아가는 애니메이션
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", other.transform.position ,
                "speed", 1,
                "easetype", iTween.EaseType.linear,
                "oncompletetarget", gameObject,
                "oncomplete", "RotateKey")) ;
            // 인벤토리에서 지워준다.
            MYS_Inventory.Instance.DeleteItemInven(gameObject);
        }
    }

    void RotateKey()
    {
        iTween.RotateTo(gameObject, iTween.Hash(
        "rotation", new Vector3(0, -90, -90),
        "speed", 30,
        "easetype", iTween.EaseType.linear,
                "oncompletetarget", gameObject,
                "oncomplete", "OnCompleteKeyOpen"));
    }
    void OnCompleteKeyOpen()
    {
        transform.parent = hit.transform;
        hit.transform.GetComponentInParent<MYS_Cabinet>().keyopen = true;        
    }
}
