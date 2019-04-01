using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipGauge : MonoBehaviour {

    Slider spaceSlider;

    int meterCount = 0;

    public int maxMeterCount;

    // Use this for initialization
    void Start () {
        spaceSlider = GameObject.Find("SpaceShipRepairLevelBar").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        meterCount++;
        if(maxMeterCount < meterCount)
        {
            meterCount = maxMeterCount;
        }

        if(meterCount < 0)
        {
            meterCount = 0;
        }

        spaceSlider.value = meterCount;
	}
}
