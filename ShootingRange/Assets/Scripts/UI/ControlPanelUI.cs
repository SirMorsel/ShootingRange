using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanelUI : MonoBehaviour
{
    private static ControlPanelUI _instance;
    public static ControlPanelUI Instance { get { return _instance; } }


    [SerializeField] private GameObject windControlPanel = null;
    [SerializeField] private GameObject ballisticsControlPanel = null;
    [SerializeField] private GameObject weaponControlPanel = null;
    private bool isControlPanelActive = false;

    [SerializeField] TMP_InputField windPeakMagnitudeInputField = null;

    [SerializeField] private TMP_InputField diameterInputField = null; // mm
    [SerializeField] private TMP_InputField massInputField = null; // gramm
    [SerializeField] private TMP_InputField barrelLengthInputField = null; // meter
    [SerializeField] private TMP_InputField gasPressureInputField = null; // bar

    [SerializeField] private TMP_Text magazineTextField = null;
    [SerializeField] private TMP_Text reloadTextField = null;

    [SerializeField] private TMP_Text showTargetHitRangeTextField = null;
    [SerializeField] private TMP_Text showTargetHorizontalImpactTextField = null;
    [SerializeField] private TMP_Text showTargetVecrticalImpactTextField = null;

    [SerializeField] private GameObject closeButton = null;



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
        closeButton.SetActive(state);
    }

    private float CheckIfValidInput(float value)
    {
        if (value < 0)
        {
            value = 0;
        }
        return value;
    }

    private string TraslateImpactPosition(float impactValue, bool isHorizontalValue)
    {
        string direction = "";
        if (impactValue < 0.1f && impactValue > -0.1f)
        {
            return "Center";
        }
        else if (impactValue < -0.1f)
        {
            if (isHorizontalValue)
            {
                direction = "Right";
            }
            else
            {
                direction = "Top";
            }
            return $"{direction} {(impactValue * -1).ToString("N3")}"; // Top || Right
        }
        else
        {
            if (isHorizontalValue)
            {
                direction = "Left";
            }
            else
            {
                direction = "Bottom";
            }
            return $"{direction} {(impactValue).ToString("N3")}"; // Bottom || Left
        }
    }

    public bool CheckIfControlPanelIsActive()
    {
        return isControlPanelActive;
    }

    public void UpdateWindUI(float windPeakMagnitude)
    {
        windPeakMagnitudeInputField.text = CheckIfValidInput(windPeakMagnitude).ToString();
    }

    public void UpdateBallisticsUI(float bulletDiameter, float bulletMass, float gunBarrelLength, float gunGasPressure)
    {
        diameterInputField.text = CheckIfValidInput(bulletDiameter).ToString();
        massInputField.text = CheckIfValidInput(bulletMass).ToString();
        barrelLengthInputField.text = CheckIfValidInput(gunBarrelLength).ToString();
        gasPressureInputField.text = CheckIfValidInput(gunGasPressure).ToString();
    }

    public void UpdateMagazineText(int bulletsInMagazine, int magazineCapacity)
    {
        magazineTextField.text = $"Mag: {bulletsInMagazine} / {magazineCapacity}";
    }

    public void ShowTargteHitRange(float range)
    {
        showTargetHitRangeTextField.text = $"Range: {range.ToString("N3")} m";
    }

    public void ShowReloadTimer(string content)
    {
        reloadTextField.text = content;
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

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void SetImpactPosition(Vector3 impactPosition)
    {
        showTargetHorizontalImpactTextField.text = $"H: {TraslateImpactPosition(impactPosition.x, true)}";
        showTargetVecrticalImpactTextField.text = $"V: {TraslateImpactPosition(impactPosition.y, false)}";
    }
}
