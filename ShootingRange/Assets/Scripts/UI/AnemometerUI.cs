using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnemometerUI : MonoBehaviour
{
    [SerializeField] WindManager windManager;
    [SerializeField] Slider slider;

    private Transform player;
    private float maxRangeForSlider = 100; // km/h

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnemoMeter();
    }

    private void UpdateAnemoMeter()
    {
        Vector2 playerVector = new Vector2(player.right.x, player.right.z);
        float windProjection = Vector2.Dot(playerVector, windManager.GetWind());
        slider.value = windProjection / (maxRangeForSlider * 2) + 0.5F;
    }
}
