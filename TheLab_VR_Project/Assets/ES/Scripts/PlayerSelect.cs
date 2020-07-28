using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{


    // 플레이어가 아이템과 충돌하면, 아이템이 눈 앞에 온다.
    // 플레이어가 아이템을 앞뒤좌우로 돌려본다.

    public GameObject playerEquipPoint;
    public GameObject playerCam;
    //public GameObject itemEquipPoint;
    GameObject equipItem;
   // MYS_PlayerClick playerClick;
    private void Start()
    {
        //playerClick = GetComponent<MYS_PlayerClick>();
    }
    private void Awake()
    {


    }

    void Update()
    {
        if (equipItem)
        {
            //if (Input.GetButtonDown("Fire2"))
            //{
            //    DownItem(equipItem);
            //    //playerClick.PutDownGrapObj();
            //}
        }
    }

    Vector3 itemPos;
    //public void OnControllerColliderHit(ControllerColliderHit col)
    //{

    //    // 열쇠에 닿으면, 
    //    if (col.gameObject.tag == "Item" || col.gameObject.tag == "Possesion")
    //    {
    //        itemPos = col.transform.position;
    //        //열쇠를 playerEquipPoint로 이동
    //        col.transform.position = playerEquipPoint.transform.position;

    //        //아이템을 EquipPoint의 자식으로 이동
    //        col.transform.parent = playerEquipPoint.transform;
    //        //리지드바디의 키네메틱을 껐다 켰다
    //        col.transform.GetComponent<Rigidbody>().isKinematic = true;

    //        //equipItem에 col Item을 넣어주고,
    //        equipItem = col.gameObject;

    //        //Player의 카메라의 캠 로테이트를 꺼주고, 아이템의 아이템 로테이트 스크립만 켜준다.
    //        playerCam.GetComponent<CamRotate>().enabled = false;
    //        col.gameObject.GetComponent<ItemRotate>().enabled = true;
    //        //인벤토리에 저장
    //        //MYS_Inventory.Instance.SaveItemToInven(col.transform.gameObject);
    //        print(col.gameObject.name);
    //    }
    //}




    void DownItem(GameObject item)
    {
        //아이템이 본래 있던 위치로 돌아간다.
        item.transform.position = itemPos;
        //Player의 카메라의 캠 로테이트를 켜주고, 아이템의 아이템 로테이트 스크립만 꺼준다.
        playerCam.GetComponent<CamRotate>().enabled = true;
        item.gameObject.GetComponent<ItemRotate>().enabled = false;
        item.transform.GetComponent<Rigidbody>().isKinematic = false;
        //아이템의 상속관계를 해제
        item.transform.parent = null;
        equipItem = null;
        //아이템이 원래 있던 위치로 돌아간다.
        print(item.gameObject.name + "을 내려놓다");

    }

}
