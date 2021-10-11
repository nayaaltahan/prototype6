using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour, IXAccelerator
{
    public float monkeyMove = 0.1f;

    public List<GameObject> monkeyPoints;
    public GameObject monkey;

    float monkeyPosition = 0;
    private float lastMokeyPos = 0;
    float acc = 0;

    public float XAcceleration => acc;

    public AnimationCurve xAccelerationCurve;

    public SpriteRenderer monsprite;
    public Animator monanim;

    void Update()
    {
        float accChange = Input.GetAxis("Horizontal");

        monkeyPosition += accChange * monkeyMove;

        monkeyPosition = Mathf.Clamp(monkeyPosition, -1, 1);

        acc = xAccelerationCurve.Evaluate(Mathf.Abs(monkeyPosition));
        if (monkeyPosition < 0)
            acc *= -1;

        if (monkeyPosition != lastMokeyPos)
        {
            monanim.gameObject.SetActive((true));
            monsprite.gameObject.SetActive((false));
        }
        else
        {
            monanim.gameObject.SetActive((false));
            monsprite.gameObject.SetActive((true));
        }

        if (monkeyPosition > 0)
        {
            monkey.transform.position = Vector3.Lerp
                (monkeyPoints[1].transform.position, monkeyPoints[2].transform.position, monkeyPosition);
        } else if (monkeyPosition < 0)
        {
            monkey.transform.position = Vector3.Lerp
                (monkeyPoints[1].transform.position, monkeyPoints[0].transform.position, Mathf.Abs(monkeyPosition));
        }
        else
        {
            monkey.transform.position = monkeyPoints[1].transform.position;
        }

        if (lastMokeyPos - monkeyPosition < 0)
        {
            monkey.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            monkey.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        lastMokeyPos = monkeyPosition;
    }
}