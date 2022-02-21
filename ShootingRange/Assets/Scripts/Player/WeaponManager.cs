using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private ControlPanelUI controlPanelUI;

    [SerializeField] private ScriptableWeapon weaponData;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletSpawn;

    private string weaponName = "";
    private float weaponDamage = 0;
    private float weaponMuzzleVelocity = 0;
    private float weaponReloadTime = 0;
    private float reloadTimer = 0;
    private bool isRealoading = false;
    private int weaponMagazineCapacity = 0;
    private int currentAmountOfBulletsInMagazine = 0;
    private int maxScopeZoom = 0;
    
    [SerializeField] private WindManager windManager;
    [SerializeField] private float gravity = 9.81F;
    [SerializeField] private float bulletLifeTime;

    private float bulletDiameter; // mm
    private float bulletMass; // gramm
    private float weaponBarrelLength; // meter
    private float weaponGasPressure; // bar

    private bool isCheatModeOn = false;

    void Awake()
    {
        InitializeWeaponData();
        // PrintWeaponData();
    }
    void Start()
    {
        controlPanelUI = ControlPanelUI.Instance;
        controlPanelUI.UpdateBallisticsUI(bulletDiameter, bulletMass, weaponBarrelLength, weaponGasPressure);
        controlPanelUI.UpdateMagazineText(currentAmountOfBulletsInMagazine, weaponMagazineCapacity);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRealoading && !controlPanelUI.GetControlPanelState())
        {
            print($"MuzzleVelocity: {FormulaBallistics.MuzzleVelocity(bulletDiameter * 0.001f, weaponGasPressure * 100000, bulletMass * 0.001f, weaponBarrelLength)}");
            Shoot();
        }
        Reload();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, bulletSpawn.position, bulletSpawn.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript)
        {
            bulletScript.Initialize(bulletSpawn, weaponMuzzleVelocity, gravity, windManager.GetWind());
            if (!isCheatModeOn)
            {
                currentAmountOfBulletsInMagazine--;
            }
            controlPanelUI.UpdateMagazineText(currentAmountOfBulletsInMagazine, weaponMagazineCapacity);
        }
        Destroy(bullet, bulletLifeTime);
    }

    private void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) && currentAmountOfBulletsInMagazine < weaponMagazineCapacity) || (currentAmountOfBulletsInMagazine <= 0))
        {
            isRealoading = true;
        }
        if (isRealoading)
        {
            reloadTimer -= Time.deltaTime;
            controlPanelUI.ShowReloadTimer($"Reloading {Mathf.RoundToInt(reloadTimer)}");
            if (reloadTimer <= 0)
            {
                controlPanelUI.ShowReloadTimer("");
                isRealoading = false;
                reloadTimer = weaponReloadTime;
                currentAmountOfBulletsInMagazine = weaponMagazineCapacity;
                controlPanelUI.UpdateMagazineText(currentAmountOfBulletsInMagazine, weaponMagazineCapacity);
            }
        }
    }

    private void InitializeWeaponData()
    {
        weaponName = weaponData.WeaponName;
        weaponDamage = weaponData.Damage;
        weaponBarrelLength = weaponData.BarrelLength;
        weaponGasPressure = weaponData.GasPressure;
        weaponReloadTime = weaponData.ReloadTime;
        reloadTimer = weaponReloadTime;
        weaponMagazineCapacity = weaponData.MagazineCapacity;
        currentAmountOfBulletsInMagazine = weaponMagazineCapacity;

        maxScopeZoom = weaponData.MaxScopeZoom;

        bulletDiameter = weaponData.Ammonation.Caliber;
        bulletMass = weaponData.Ammonation.ProjectileMassInGram;
        weaponMuzzleVelocity = FormulaBallistics.MuzzleVelocity(bulletDiameter * 0.001f, weaponGasPressure * 100000, bulletMass * 0.001f, weaponBarrelLength);
    }

    public int GetWeaponMaxScopeZoom ()
    {
        return maxScopeZoom;
    }

    private void PrintWeaponData()
    {
        print($"Name: {weaponName}, Damage: {weaponDamage}, MuzzleEnergy: {weaponMuzzleVelocity}, " +
              $"Reload: {weaponReloadTime}, Magazine: {weaponMagazineCapacity}, MaxZoom: {maxScopeZoom}");
    }

    public void OverrideBallistics()
    {
        bulletDiameter = controlPanelUI.GetBulletDiameterFromUI();
        bulletMass = controlPanelUI.GetBulletMassFromUI();
        weaponBarrelLength = controlPanelUI.GetGunBarrelLengthFromUI();
        weaponGasPressure = controlPanelUI.GetGunGasPressureFromUI();
        weaponMuzzleVelocity = FormulaBallistics.MuzzleVelocity(bulletDiameter * 0.001f, weaponGasPressure * 100000, bulletMass * 0.001f, weaponBarrelLength);

        controlPanelUI.UpdateBallisticsUI(bulletDiameter, bulletMass, weaponBarrelLength, weaponGasPressure);
    }

    public void ChangeCheatModeState()
    {
        isCheatModeOn = !isCheatModeOn;
    }
}
