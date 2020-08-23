using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_Inventory : MonoBehaviour
{
    public static MYS_Inventory Instance;
    MYS_CamRotate cm;
    private void Start()
    {
        cm = Camera.main.transform.GetComponent<MYS_CamRotate>();

    }
    private void Awake()
    {
        Instance = this;
    }
    //아이템을 넣을 List필요
    public List<GameObject> inven = new List<GameObject>();
    public GameObject book;
    public void SaveItemToInven(GameObject item)
    {
        //만약 인벤토리에 해당 아이템이 없다면
        if (!inven.Contains(item))
        {
            // 인벤토리에 아이템을 저장시킨다.
            inven.Add(item);
            if (item.tag.Contains("Book"))
            {
                book = item;
            }
        }
    }
    public void DeleteItemInven(GameObject item)
    {
        if (inven.Contains(item))
        {
            inven.Remove(item);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            book.SetActive(!book.activeSelf);
        }
        if (book != null&& book.activeSelf)
        {
            //만약 키보드 왼쪽 Alt키를 누르면
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                //카메라 회전을 멈추고 아이템을 회전시킨다.
                cm.enabled = false;
                book.transform.GetComponent<ItemRotate>().enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                //아이템 회전을 멈추고 카메라를 회전시킨다.
                cm.enabled = true;
                book.transform.GetComponent<ItemRotate>().enabled = false;
            }
        }
    }
}
