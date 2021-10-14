using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.NetworkVariable;
using UnityEngine;

public class Elephant : NetworkBehaviour, IYAccelerator
{
    public float adder = 0.1f;

    public float changeOverTime = 0.1f;

    public float delay = 0.1f;
    private float timer = 0;
    
    [SerializeField] public NetworkVariable<float> ElephantVariable = new NetworkVariable<float>();

    public float YAcceleration => ElephantVariable.Value;

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
        ElephantVariable.Settings.WritePermission = NetworkVariablePermission.ServerOnly;
        ElephantVariable.Settings.ReadPermission = NetworkVariablePermission.Everyone;

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
                ElephantVariable.Value += adder;
                timer = 0;
            }
            if (timer > delay)
            {
                ElephantVariable.Value -= changeOverTime * Time.deltaTime;
            }

            ElephantVariable.Value=Mathf.Clamp01(ElephantVariable.Value);
        }

    }
}