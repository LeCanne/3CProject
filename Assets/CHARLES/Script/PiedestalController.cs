using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiedestalController : MonoBehaviour
{
    public ItemDatabase database;
    public InventoryController theInventory;
    public AudioManager theAudio;

    public enum CATEGORY
    {
        PIEDESTAL1,
        PIEDESTAL2
    }
    public CATEGORY category;
    public Transform trPlacement;
    public GameObject prefabItem;

    [Header("Put/Take")]
    public GameObject putObject;
    public Image imgPutObject;
    public TextMeshProUGUI txtPutObject;
    public GameObject takeObject;
    public Image imgTakeObject;
    public Image imgIcon;
    public TextMeshProUGUI txtTakeObject;
    public static bool canPut1, havePut1;
    public static bool canPut2, havePut2;
    public static bool clear1, clear2;

    [Header("Yeux")]
    public GameObject oeilDroite;
    public GameObject oeilGauche;

    public Material matInit, matClear;

    private void Awake()
    {
        theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // On ajoute l'objet qu'on a sélectionne depuis l'inventaire en donnant ses données
    public void PutObject(ItemController data)
    {
        theAudio.Scrapping();

        putObject.SetActive(false);
        imgIcon.color = new Color(1, 1, 1, 0f);

        GameObject newItem = Instantiate(prefabItem);
        newItem.transform.position = trPlacement.position;
        newItem.transform.parent = gameObject.transform;

        newItem.GetComponent<ItemController>().category = data.category;
        newItem.GetComponent<ItemController>().takeObject = takeObject;
        newItem.GetComponent<ItemController>().imgTakeObject = imgTakeObject;
        newItem.GetComponent<ItemController>().txtTakeObject = txtTakeObject;
        newItem.GetComponent<ItemController>().theInventory = theInventory;

        foreach (var database in database.datas)
        {
            if (newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT1 && database.categories == ItemData.CATEGORY.IMPORTANT1)
            {   
                newItem.GetComponent<ItemController>().objectMesh.sharedMesh = database.mesh;
                newItem.GetComponent<ItemController>().sp = database.icon;
                newItem.GetComponent<ItemController>().label = database.label;
                newItem.GetComponent<ItemController>().caption = database.caption;
            }

            if (newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT2 && database.categories == ItemData.CATEGORY.IMPORTANT2)
            {   
                newItem.GetComponent<ItemController>().objectMesh.sharedMesh = database.mesh;
                newItem.GetComponent<ItemController>().sp = database.icon;
                newItem.GetComponent<ItemController>().label = database.label;
                newItem.GetComponent<ItemController>().caption = database.caption;
            }
        }

        if (category == CATEGORY.PIEDESTAL1 && newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT1 || 
            category == CATEGORY.PIEDESTAL2 && newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT1)
        {
            newItem.GetComponent<SphereCollider>().enabled = false;
            clear1 = true;

            MeshRenderer meshDroite =  oeilDroite.GetComponent<MeshRenderer>();
            meshDroite.material = matClear;
        }

        if (category == CATEGORY.PIEDESTAL2 && newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT2 ||
            category == CATEGORY.PIEDESTAL1 && newItem.GetComponent<ItemController>().category == ItemController.CATEGORY.IMPORTANT2)
        {
            newItem.GetComponent<SphereCollider>().enabled = false;
            clear2 = true;

            MeshRenderer meshGauche = oeilGauche.GetComponent<MeshRenderer>();
            meshGauche.material = matClear;
        }
    }

    public IEnumerator PutobjectOn()
    {
        imgPutObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtPutObject.color = new Color(0, 0, 0, 0.2f);
        imgIcon.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtPutObject.color = new Color(0, 0, 0, 0.4f);
        imgIcon.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtPutObject.color = new Color(0, 0, 0, 0.6f);
        imgIcon.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtPutObject.color = new Color(0, 0, 0, 0.8f);
        imgIcon.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 1f);
        txtPutObject.color = new Color(0, 0, 0, 1f);
        imgIcon.color = new Color(1, 1, 1, 1f);
    }

    public IEnumerator PutobjectOf()
    {
        imgPutObject.color = new Color(0, 0.8f, 1, 1f);
        txtPutObject.color = new Color(0, 0, 0, 1f);
        imgIcon.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtPutObject.color = new Color(0, 0, 0, 0.8f);
        imgIcon.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtPutObject.color = new Color(0, 0, 0, 0.6f);
        imgIcon.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtPutObject.color = new Color(0, 0, 0, 0.4f);
        imgIcon.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtPutObject.color = new Color(0, 0, 0, 0.2f);
        imgIcon.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0);
        txtPutObject.color = new Color(0, 0, 0, 0f);
        imgIcon.color = new Color(1, 1, 1, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (category == CATEGORY.PIEDESTAL1 && !havePut1)
            {
                putObject.SetActive(true);
                canPut1 = true;
                StartCoroutine(PutobjectOn());
            }

            if (category == CATEGORY.PIEDESTAL2 && !havePut2)
            {
                putObject.SetActive(true);
                canPut2 = true;
                StartCoroutine(PutobjectOn());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !havePut1 || other.CompareTag("Player") && !havePut2)
        {
            canPut1 = false;
            canPut2 = false;
            StartCoroutine(PutobjectOf());
        }
    }
}
