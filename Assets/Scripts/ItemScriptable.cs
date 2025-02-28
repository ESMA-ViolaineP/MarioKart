using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MarioKart", menuName = "New Item", order = 0)]
public class ItemScriptable : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    public ItemType ItemType;
}
