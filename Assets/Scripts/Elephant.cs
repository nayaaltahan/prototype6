using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Elephant : NetworkBehaviour, IYAccelerator
{
    public float adder = 0.1f;

    public float changeOverTime = 0.1f;

    public float delay = 0.1f;
    private float timer = 0;

    float acc = 0;

    public float YAcceleration => acc;

    public bool isElephant = false;
    
    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    public float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;
    

    private void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    void Update()
    {
        if (Information.type == Type.Elephant || Information.type == Type.Server)
        {
            isElephant = true;
        }
        
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;
        
        timer += Time.deltaTime;
        if (isElephant)
        {
            if (Input.GetKeyDown(KeyCode.Space) || deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
            {
                acc += adder;
                timer = 0;
            }
        }
        

        if (timer > delay)
        {
            acc -= changeOverTime * Time.deltaTime;
        }

        acc=Mathf.Clamp01(acc);
    }
}