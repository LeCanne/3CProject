using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("COMMON :")]
    public MaskableGraphic imgItem;
    public MaskableGraphic txtLabel;
    public MaskableGraphic txtQuantity;
    public MaskableGraphic txtNbQuantity;
    public TextMeshProUGUI txtNbUnit;
    public TextMeshProUGUI txtLabelInit;

    [Header("SHOP :")]
    public MaskableGraphic txtPrice;

    public Action onSelect;
    public Action onDeselect;

    public void OnDeselect(BaseEventData eventData)
    {
        onDeselect?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelect?.Invoke();
    }
}
