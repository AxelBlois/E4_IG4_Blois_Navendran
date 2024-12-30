using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneWaterManager : MonoBehaviour
{

    public float maxWater = 100f;
    public float refillRate = 20f;
    public float releaseRate = 50f;
    public Slider waterGauge;
    public Transform waterReleasePoint;
    public GameObject waterEffect;

    private float currentWater = 0f;
    private bool isRefilling = false;


    // Update is called once per frame
    void Update()
    {
        HandleWaterReffil();
        HandleWaterRelease();
        UpdateWaterGauge();
    }

    void HandleWaterReffil()
    {
        if(isRefilling && currentWater < maxWater)
        {
            currentWater += refillRate * Time.deltaTime;
            currentWater = Mathf.Clamp(currentWater, 0f, maxWater);
        }
    }
    
    void HandleWaterRelease()
    {
        if(Input.GetKey(KeyCode.Space)&& currentWater > 0f)
        {
            currentWater -= releaseRate * Time.deltaTime;
            if (waterEffect != null && waterReleasePoint != null)
            {
                Instantiate(waterEffect, waterReleasePoint.position, Quaternion.identity);
            }
            currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        }
    }

    void UpdateWaterGauge()
    {
        if(waterGauge != null)
        {
            waterGauge.value = currentWater / maxWater;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            isRefilling = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            isRefilling = false;
        }
    }

}
