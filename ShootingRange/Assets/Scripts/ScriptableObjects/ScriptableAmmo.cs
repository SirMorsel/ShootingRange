using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AmmoType", menuName = "ScriptableObjects/AmmoType")]
public class ScriptableAmmo : ScriptableObject
{
    public float Caliber;
    public string Description;
    public float ProjectileMassInGram;
}
