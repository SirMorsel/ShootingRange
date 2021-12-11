using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public string WeaponName;
    public float Damage;
    public float BarrelLength;
    public float GasPressure;
    public float ReloadTime;
    public int MagazineCapacity;
    public int MaxScopeZoom;

    public ScriptableAmmo Ammonation;
}