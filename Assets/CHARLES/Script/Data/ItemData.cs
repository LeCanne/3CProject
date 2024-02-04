using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public enum CATEGORY
    {
        IMPORTANT,
        NON_IMPORTANT
    }

    public string label;
    public string caption;
    public Mesh mesh;
    public Sprite sprite;
    public CATEGORY categories;
}
