using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemDatabase database;
    public InventoryController theInventory;
    public PiedestalController piedestal;
    private AudioManager theAudio;
    public ShowObject theObject;

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
    public UnityEngine.Sprite sp;
    public MeshFilter objectMesh;

    public GameObject takeObject;
    public Image imgTakeObject;
    public Image imgIcon;
    public TextMeshProUGUI txtTakeObject;
    public static bool canTake;

    public GameObject objectCamera;

    private void Awake()
    {
        theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // On assigne les datas selon la categorie de l'objet
        foreach (var data in database.datas)
        {
            if (category == CATEGORY.IMPORTANT1 && data.categories == ItemData.CATEGORY.IMPORTANT1)
            {
                objectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
            }

            if (category == CATEGORY.IMPORTANT2 && data.categories == ItemData.CATEGORY.IMPORTANT2)
            {
                objectMesh.sharedMesh = data.mesh;
                sp = data.icon;
                label = data.label;
                caption = data.caption;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        takeObject.transform.LookAt(objectCamera.transform.position);
    }

    // Procédure activée depuis le script "PlayerController"
    public void TakeObject()
    {
        theAudio.PickUp();
        takeObject.SetActive(false);
        imgIcon.color = new Color(1, 1, 1, 0f);
        canTake = false;
        theInventory.AddSlot(this);

        theObject.gameObject.SetActive(true);
        
        if (category == CATEGORY.IMPORTANT1)
            theObject.isRightEye = true;

        theObject.AddObject(sp, label);

        
    }

    // Procédure activée depuis le script "PlayerController"
    public IEnumerator TakeobjectOn()
    {
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtTakeObject.color = new Color(0, 0, 0, 0.2f);
        imgIcon.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtTakeObject.color = new Color(0, 0, 0, 0.4f);
        imgIcon.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtTakeObject.color = new Color(0, 0, 0, 0.6f);
        imgIcon.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtTakeObject.color = new Color(0, 0, 0, 0.8f);
        imgIcon.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 1f);
        txtTakeObject.color = new Color(0, 0, 0, 1f);
        imgIcon.color = new Color(1, 1, 1, 1f);
    }

    public IEnumerator TakeobjectOf()
    {
        imgTakeObject.color = new Color(0, 0.8f, 1, 1f);
        txtTakeObject.color = new Color(0, 0, 0, 1f);
        imgIcon.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtTakeObject.color = new Color(0, 0, 0, 0.8f);
        imgIcon.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtTakeObject.color = new Color(0, 0, 0, 0.6f);
        imgIcon.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtTakeObject.color = new Color(0, 0, 0, 0.4f);
        imgIcon.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtTakeObject.color = new Color(0, 0, 0, 0.2f);
        imgIcon.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgTakeObject.color = new Color(0, 0.8f, 1, 0);
        txtTakeObject.color = new Color(0, 0, 0, 0f);
        imgIcon.color = new Color(1, 1, 1, 0f);
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
