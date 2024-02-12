using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public MeshFilter ObjectMesh;

    public GameObject takeObject;
    public Image imgTakeObject;
    public TextMeshProUGUI txtTakeObject;
    public static bool canTake;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in database.datas)
        {
            if (category == CATEGORY.NON_IMPORTANT1 && data.categories == ItemData.CATEGORY.NON_IMPORTANT1) 
            {
                ObjectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.NON_IMPORTANT2 && data.categories == ItemData.CATEGORY.NON_IMPORTANT2)
            {
                ObjectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.NON_IMPORTANT3 && data.categories == ItemData.CATEGORY.NON_IMPORTANT3)
            {
                ObjectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.IMPORTANT1 && data.categories == ItemData.CATEGORY.IMPORTANT1)
            {
                ObjectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
                Debug.Log(label);
            }

            if (category == CATEGORY.IMPORTANT2 && data.categories == ItemData.CATEGORY.IMPORTANT2)
            {
                ObjectMesh.sharedMesh = data.mesh;
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

    public void TakeObject()
    {
        takeObject.SetActive(false);
        canTake = false;
        theInventory.AddSlot(this);
        Destroy(gameObject);
    }

    public IEnumerator TakeobjectOn()
    {
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtTakeObject.color = new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtTakeObject.color = new Color(0, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtTakeObject.color = new Color(0, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtTakeObject.color = new Color(0, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 1f);
        txtTakeObject.color = new Color(0, 0, 0, 1f);
    }

    public IEnumerator TakeobjectOf()
    {
        imgTakeObject.color = new Color(0, 0.8f, 1, 1f);
        txtTakeObject.color = new Color(0, 0, 0, 1f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtTakeObject.color = new Color(0, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtTakeObject.color = new Color(0, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtTakeObject.color = new Color(0, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtTakeObject.color = new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0);
        txtTakeObject.color = new Color(0, 0, 0, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            takeObject.SetActive(true);
            canTake = true;
            StartCoroutine(TakeobjectOn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTake = false;
            StartCoroutine(TakeobjectOf());
        }
    }
}
