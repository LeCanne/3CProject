using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
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

    public GameObject content;
    public Sprite sp1, sp2;
    public static bool haveButton;
    public static bool noUseInventory;

    public DepthOfField blur;
    public Volume volume;
    public ClampedFloatParameter cfp;

    public float timerBlur;

    [Header("Info")]
    [SerializeField] MaskableGraphic txtTitleInfo;
    [SerializeField] MaskableGraphic txtInfoCaption;
    [SerializeField] MaskableGraphic imgInfoCaption;
    [SerializeField] Image imgInfo;

    // Start is called before the first frame update
    void Awake()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            blur = dof;
        }
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
            else if (PiedestalController.canPut2)
            {
                UseObject2(theNewSlot, theItem);
            }
        }

        if (content.transform.childCount > 0)
        {
            if (content.GetComponentInChildren<SlotController>().gameObject == null)
            {
                RefreshInfo();
            }

            if (imgInfo.sprite == sp1 && PiedestalController.canPut1 && haveButton
            || imgInfo.sprite == sp1 && PiedestalController.canPut2 && haveButton
            || imgInfo.sprite == sp2 && PiedestalController.canPut1 && haveButton
            || imgInfo.sprite == sp2 && PiedestalController.canPut2 && haveButton)
            {
                buttonUse.SetActive(true);
                haveButton = false;
            }
            else
            {
                haveButton = false;
            }
        }
    }

    public IEnumerator DoBlur()
    {
        timerBlur = 0f;
        DepthOfField startBlur = blur;
        startBlur.focalLength.value = 1;
        DepthOfField endBlur = blur;
        endBlur.focalLength.value = 20;

        while (timerBlur < 0.5f)
        {
            timerBlur += Time.deltaTime;
            blur.focalLength.value = Mathf.Lerp(1, 40, timerBlur / 0.5f);

            yield return null;
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
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
            //content = data.gameObject;
            canUse = true;
            theItem = data;
            theNewSlot = newSlot;
            buttonUse.SetActive(true);
            buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonUse.GetComponent<Button>().onClick.AddListener(() => UseObject1(newSlot, data));
        }
        else if (data.category == ItemController.CATEGORY.IMPORTANT1 && PiedestalController.canPut2 || data.category == ItemController.CATEGORY.IMPORTANT2 && PiedestalController.canPut2)
        {
            //content = data.gameObject;
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
        InventoryController.noUseInventory = false;
        canUse = false;
        theItem = null;
        theNewSlot = null;
        gameObject.SetActive(false);
        buttonUse.SetActive(false);
        Time.timeScale = 1f;
        buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void UseObject2(GameObject newSlot, ItemController data)
    {
        thePlayer.piedestal.PutObject(data);
        Destroy(newSlot);
        PiedestalController.canPut2 = false;
        PiedestalController.havePut2 = true;
        CameraController.noUseCamera = false;
        InventoryController.noUseInventory = false;
        canUse = false;
        theItem = null;
        theNewSlot = null;
        gameObject.SetActive(false);
        buttonUse.SetActive(false);
        Time.timeScale = 1f;
        buttonUse.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            case TextMeshProUGUI txt: txt.text = value.ToString(); break;
            case TMP_Text txt: txt.text = value.ToString(); break;
            case Text txt: txt.text = value.ToString(); break;


            case Image img: img.sprite = value as Sprite; imgInfo = img; break;
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


            case Image img: img.sprite = null; imgInfo = null; break;
            case RawImage img: img.texture = null; break;
        }
    }
}
