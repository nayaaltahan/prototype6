using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
