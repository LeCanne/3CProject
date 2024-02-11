using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemDatabase database;
    public InventoryController theInventory;

    public enum CATEGORY
    {
        IMPORTANT1,
        IMPORTANT2,
        NON_IMPORTANT1,
        NON_IMPORTANT2,
        NON_IMPORTANT3
    }

    public CATEGORY category;
    public string label;
    public string caption;
    public Sprite sp;
    public Mesh mainMesh;
    public MeshFilter ObjectMesh;

    private void Awake()
    {
        theInventory = GameObject.FindAnyObjectByType<InventoryController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in database.datas)
        {

            if (category == CATEGORY.NON_IMPORTANT1 && data.categories == ItemData.CATEGORY.NON_IMPORTANT1) 
            {
                mainMesh = data.mesh;
                ObjectMesh.sharedMesh = mainMesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.NON_IMPORTANT2 && data.categories == ItemData.CATEGORY.NON_IMPORTANT2)
            {
                mainMesh = data.mesh;
                ObjectMesh.sharedMesh = mainMesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.NON_IMPORTANT3 && data.categories == ItemData.CATEGORY.NON_IMPORTANT3)
            {
                mainMesh = data.mesh;
                ObjectMesh.sharedMesh = mainMesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.IMPORTANT1 && data.categories == ItemData.CATEGORY.IMPORTANT1)
            {
                mainMesh = data.mesh;
                ObjectMesh.sharedMesh = mainMesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.IMPORTANT2 && data.categories == ItemData.CATEGORY.IMPORTANT2)
            {
                mainMesh = data.mesh;
                ObjectMesh.sharedMesh = mainMesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
