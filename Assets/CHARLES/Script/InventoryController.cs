using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static List<ItemData> slotDatas = new();

    [Header("Inventaire")]
    public GameObject slotInventaire;
    public GameObject parentSlotInventaire;

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
                newSlot.GetComponent<Button>().onClick.AddListener(() => OnSlotSelected(item));

                InitSlotBuy(sc, item);
            }
        }
    }

    private void InitSlotBuy(SlotController slot, ItemController data)
    {
        SetMaskableGraphicValue(ref slot.imgItem, data.sp);
        //slot.imgItem.color = data.color;
        //SetMaskableGraphicValue(ref slot.txtLabel, data.label);
    }

    public void OnSlotSelected(ItemController data)
    {
        //slotDatas.Clear();

        //slotDatas.Add(data);

        //SetSelectableValue(ref ifQuantity, nbQuantity <= 0 ? 1 : nbQuantity);

        ShowDataInfo(data);
    }

    private void ShowDataInfo(ItemController data)
    {
        SetMaskableGraphicValue(ref txtTitleInfo, data.label);
        SetMaskableGraphicValue(ref txtInfoCaption, data.caption);
        SetMaskableGraphicValue(ref imgInfoCaption, data.sp);
        //imgInfoCaption.color = data.color;
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
}
