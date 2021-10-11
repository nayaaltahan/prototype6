using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour, IYAccelerator
{
    public float adder = 0.1f;

    public float changeOverTime = 0.1f;

    public float delay = 0.1f;
    private float timer = 0;

    float acc = 0;

    public float YAcceleration => acc;

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            acc += adder;
            timer = 0;
        }

        if (timer > delay)
        {
            acc -= changeOverTime * Time.deltaTime;
        }

        acc=Mathf.Clamp01(acc);
    }
}