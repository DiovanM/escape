using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{

    public string displayName;
    public string itemKey;
    public Sprite icon;
    public Vector3 holdPositionOffset;
    public Quaternion holdRotation;

}
