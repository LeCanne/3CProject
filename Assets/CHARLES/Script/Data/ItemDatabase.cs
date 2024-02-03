using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Database/Item", order = 1)]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> datas = new();
}
