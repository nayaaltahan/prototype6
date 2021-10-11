using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    // Start is called before the first frame update
    private float HorizentalMovementMultiplier=0.1f;
    
    private float _X;
    private float _Y;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _X = Input.GetAxis("Horizontal");
        _Y = Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _Y *= 5;
            gameObject.transform.Translate(0,_Y,0);
        }
        gameObject.transform.Translate(HorizentalMovementMultiplier*_X,_Y,0);
        // gameObject.transform.position = new Vector3(gameObject.transform.position.x,
        //     gameObject.transform.position.y + Time.deltaTime, gameObject.transform.position.z);
    }
}
