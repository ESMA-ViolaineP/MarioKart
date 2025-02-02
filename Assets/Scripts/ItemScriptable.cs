using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MarioKart", menuName = "New Item", order = 0)]
public class ItemScriptable : ScriptableObject
{
    public Image ItemImage;
    public string ItemName;
    public ItemType ItemType;
}
