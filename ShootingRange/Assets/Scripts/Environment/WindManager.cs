using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindManager : MonoBehaviour
{
    private ControlPanelUI controlPanelUI;
    [SerializeField] private float windDelta;
    [SerializeField] private float windUpdateTime;
    [SerializeField] private float windPeakMagnitude;

    [SerializeField] TMP_InputField windPeakMagnitudeInputField = null;
    
    private Vector2 wind;

    private Vector2 windUpdated;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        controlPanelUI = ControlPanelUI.Instance;
        controlPanelUI.UpdateWindUI(windPeakMagnitude);
        SetWind(Random.insideUnitCircle * windPeakMagnitude);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWind();
        time += Time.deltaTime;

        if (time > windUpdateTime)
        {
            time = 0F;

            windUpdated = GetUpdatedWind();
        }
    }

    private void UpdateWind()
    {
        wind = Vector2.Lerp(wind, windUpdated, Time.deltaTime);
    }

    private Vector2 GetUpdatedWind()
    {
        Vector2 result = wind + Random.insideUnitCircle * windDelta;
        result = result.normalized * Mathf.Min(result.magnitude, windPeakMagnitude);
        return result;
    }

    public Vector2 GetWind()
    {
        return wind;
    }

    public float GetWindPeakMagnitude()
    {
        return windPeakMagnitude;
    }

    public void SetWind(Vector2 newWindData)
    {
        wind = newWindData;
        windUpdated = wind;
        time = 0F;

    }

    public void OverrideCurrentWindData()
    {
        windPeakMagnitude = controlPanelUI.GetWindPeakMagnitudeFromUI();
        SetWind(Random.insideUnitCircle * windPeakMagnitude);
    }
}
