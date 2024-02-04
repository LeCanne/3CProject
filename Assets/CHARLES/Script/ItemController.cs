using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemDatabase database;
    public InventoryController theInventory;

    public enum CATEGORY
    {
        IMPORTANT,
        NON_IMPORTANT
    }

    public CATEGORY category;
    public string label;
    public Sprite sp;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in database.datas)
        {

            if (category == CATEGORY.NON_IMPORTANT && data.categories == ItemData.CATEGORY.NON_IMPORTANT) 
            {
                sp = data.sprite;
                label = data.label;
                Debug.Log(label);
            }

            if (category == CATEGORY.IMPORTANT && data.categories == ItemData.CATEGORY.IMPORTANT)
            {
                label = data.label;
                Debug.Log(label);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
