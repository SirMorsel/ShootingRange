using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanelUI : MonoBehaviour
{
    private static ControlPanelUI _instance;
    public static ControlPanelUI Instance { get { return _instance; } }


    [SerializeField] GameObject windControlPanel = null;
    [SerializeField] GameObject ballisticsControlPanel = null;
    [SerializeField] GameObject weaponControlPanel = null;
    private bool isControlPanelActive = false;

    [SerializeField] TMP_InputField windPeakMagnitudeInputField = null;

    [SerializeField] private TMP_InputField diameterInputField = null; // mm
    [SerializeField] private TMP_InputField massInputField = null; // gramm
    [SerializeField] private TMP_InputField barrelLengthInputField = null; // meter
    [SerializeField] private TMP_InputField gasPressureInputField = null; // bar

    [SerializeField] private TMP_Text magazineTextField = null;
    [SerializeField] private TMP_Text showTargetHitTangeTextField = null;




    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangePanelsState(isControlPanelActive);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isControlPanelActive = !isControlPanelActive;
            ChangePanelsState(isControlPanelActive);
        }
    }

    private void ChangePanelsState(bool state)
    {
        windControlPanel.SetActive(state);
        ballisticsControlPanel.SetActive(state);
        weaponControlPanel.SetActive(state);
    }

    public bool CheckIfControlPanelIsActive()
    {
        return isControlPanelActive;
    }

    public void UpdateWindUI(float windPeakMagnitude)
    {
        windPeakMagnitudeInputField.text = windPeakMagnitude.ToString();
    }

    public void UpdateBallisticsUI(float bulletDiameter, float bulletMass, float gunBarrelLength, float gunGasPressure)
    {
        diameterInputField.text = bulletDiameter.ToString();
        massInputField.text = bulletMass.ToString();
        barrelLengthInputField.text = gunBarrelLength.ToString();
        gasPressureInputField.text = gunGasPressure.ToString();
    }

    public void UpdateMagazineText(int bulletsInMagazine, int magazineCapacity)
    {
        magazineTextField.text = $"Mag: {bulletsInMagazine} / {magazineCapacity}";
    }

    public void ShowTargteHitRange(float range)
    {
        showTargetHitTangeTextField.text = $"Range: {range.ToString("N3")} m";
    }

    public float GetWindPeakMagnitudeFromUI()
    {
        return float.Parse(windPeakMagnitudeInputField.text);
    }

    public float GetBulletDiameterFromUI()
    {
        return float.Parse(diameterInputField.text);
    }

    public float GetBulletMassFromUI()
    {
        return float.Parse(massInputField.text);
    }

    public float GetGunBarrelLengthFromUI()
    {
        return float.Parse(barrelLengthInputField.text);
    }

    public float GetGunGasPressureFromUI()
    {
        return float.Parse(gasPressureInputField.text);
    }

    public bool GetControlPanelState()
    {
        return isControlPanelActive;
    }
}
