using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public enum CATEGORY
    {
        IMPORTANT1,
        IMPORTANT2,
        NON_IMPORTANT1,
        NON_IMPORTANT2,
        NON_IMPORTANT3,
    }

    public string label;
    public string caption;
    public Mesh mesh;
    public Sprite icon;
    public CATEGORY categories;
}
