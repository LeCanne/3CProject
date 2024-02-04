using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static List<ItemData> slotDatas = new();
    public GameObject slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSlot(ItemController item)
    {
        if (slotDatas.Exists(x => x.label == item.label))
        {

        }
        else
        {
            slot.gameObject.GetComponent<Image>().sprite = item.sp;
        }
    }
}
