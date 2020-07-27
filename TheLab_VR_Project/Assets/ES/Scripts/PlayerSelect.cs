using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{


    // 플레이어가 아이템을 만지면, getTrigger하면 아이템이 눈앞으로 이동해 온다.
    // 플레이어가 아이템을 앞뒤좌우로 돌려본다.
  
    public GameObject playerEquipPoint;

    
    

    private void Awake()
    {


    } 

    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider col)
    {
        // 열쇠에 닿으면, 
        if (col.gameObject.tag == "Item")
        {
            //열쇠를 playerEquipPoint로 이동
            col.transform.position = playerEquipPoint.transform.position;
            //근데, 여기에서 문제는, 아이템이 EquipPoint로 왔다가 떨어짐..
            //계속 붙여놓는 방법은?

            ObjectRotate();
            
            print(col.gameObject.name);
        }
         
    }

    void ObjectRotate()
    {
        //오브젝트를 돌려 본다.

    }

    /*private void OnTriggerExit(Collider col)
    {
        if (Input.GetButtonDown("Fire1"))
        {
          //아이템이 원래 있던 위치로 돌아간다.
        }
    }
    */
}
