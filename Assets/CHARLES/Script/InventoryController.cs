using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static List<ItemData> slotDatas = new();
    public PlayerController thePlayer;
    BaseEventData eventData;

    [Header("Inventaire")]
    public GameObject slotInventaire;
    public GameObject parentSlotInventaire;
    public GameObject buttonUse;
    private bool canUse;
    public ItemController theItem;
    public GameObject theNewSlot;

    [Header("Info")]
    [SerializeField] MaskableGraphic txtTitleInfo;
    [SerializeField] MaskableGraphic txtInfoCaption;
    [SerializeField] MaskableGraphic imgInfoCaption;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire4") && canUse)
        {
            if (PiedestalController.canPut1)
            {
                UseObject1(theNewSlot, theItem);
            }
            else if (Input.GetButtonDown("Fire4") && PiedestalController.canPut2)
            {
                UseObject2(theNewSlot, theItem);
            }
        }
    }

    public void AddSlot(ItemController item)
    {
        if (slotDatas.Exists(x => x.label == item.label))
        {

        }
        else
        {
            var newSlot = Instantiate(slotInventaire, parentSlotInventaire.transform);
            if (newSlot.TryGetComponent<SlotController>(out var sc))
            {
                newSlot.GetComponent<Button>().onClick.AddListener(() => OnSlotSelected(item, newSlot));

                InitSlotBuy(sc, item);

                sc.onSelect += () => OnSlotSelected(item, newSlot);
                sc.onDeselect += () =>
                {
                    RefreshInfo();
                };
            }
        }
    }

    private void InitSlotBuy(SlotController slot, ItemController data)
    {
        SetMaskableGraphicValue(ref slot.imgItem, data.sp);
        //slot.imgItem.color = data.color;
        //SetMaskableGraphicValue(ref slot.txtLabel, data.label);
    }

    public void OnSlotSelected(ItemController data, GameObject newSlot)
    {
        if (data.category == ItemController.CATEGORY.IMPORTANT1 && PiedestalController.canPut1 || data.category == ItemController.CATEGORY.IMPORTANT2 && PiedestalController.canPut1)
        {
            canUse = true;
            theItem = data;
            theNewSlot = newSlot;
            buttonUse.SetActive(true);
            buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonUse.GetComponent<Button>().onClick.AddListener(() => UseObject1(newSlot, data));
        }
        else if (data.category == ItemController.CATEGORY.IMPORTANT1 && PiedestalController.canPut2 || data.category == ItemController.CATEGORY.IMPORTANT2 && PiedestalController.canPut2)
        {
            canUse = true;
            theItem = data;
            theNewSlot = newSlot;
            buttonUse.SetActive(true);
            buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonUse.GetComponent<Button>().onClick.AddListener(() => UseObject2(newSlot, data));
        }
        else
        {
            canUse = false;
            theItem = null;
            theNewSlot = null;
            buttonUse.SetActive(false);
        }

        ShowDataInfo(data);
    }

    private void ShowDataInfo(ItemController data)
    {
        SetMaskableGraphicValue(ref txtTitleInfo, data.label);
        SetMaskableGraphicValue(ref txtInfoCaption, data.caption);
        SetMaskableGraphicValue(ref imgInfoCaption, data.sp);
        //imgInfoCaption.color = data.color;
    }

    private void RefreshInfo()
    {
        RefreshMaskableGraphicValue(ref txtTitleInfo);
        RefreshMaskableGraphicValue(ref txtInfoCaption);
        RefreshMaskableGraphicValue(ref imgInfoCaption);
    }

    public void UseObject1(GameObject newSlot, ItemController data)
    {
        thePlayer.piedestal.PutObject(data);
        Destroy(newSlot);
        PiedestalController.canPut1 = false;
        PiedestalController.havePut1 = true;
        CameraController.noUseCamera = false;
        canUse = false;
        theItem = null;
        theNewSlot = null;
        gameObject.SetActive(false);
        buttonUse.SetActive(false);
        Time.timeScale = 1f;
        buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log(PiedestalController.canPut1);
    }

    public void UseObject2(GameObject newSlot, ItemController data)
    {
        thePlayer.piedestal.PutObject(data);
        Destroy(newSlot);
        PiedestalController.canPut2 = false;
        PiedestalController.havePut2 = true;
        CameraController.noUseCamera = false;
        canUse = false;
        theItem = null;
        theNewSlot = null;
        gameObject.SetActive(false);
        buttonUse.SetActive(false);
        Time.timeScale = 1f;
        buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log(PiedestalController.canPut2);
    }

    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            case TextMeshProUGUI txt: txt.text = value.ToString(); break;
            case TMP_Text txt: txt.text = value.ToString(); break;
            case Text txt: txt.text = value.ToString(); break;


            case Image img: img.sprite = value as Sprite; break;
            case RawImage img: img.texture = value as Texture; break;
        }
    }

    private void RefreshMaskableGraphicValue(ref MaskableGraphic mg)
    {
        switch (mg)
        {
            case TextMeshProUGUI txt: txt.text = ""; break;
            case TMP_Text txt: txt.text = ""; break;
            case Text txt: txt.text = ""; break;


            case Image img: img.sprite = null; break;
            case RawImage img: img.texture = null; break;
        }
    }
}
