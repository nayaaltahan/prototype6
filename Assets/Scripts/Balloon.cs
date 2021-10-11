using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{
    public float g = 0.1f;
    public float maxYSpeed = 0.5f;
    public float maxXSpeed = 0.5f;

    public float breakVelocity = 0.2f;

    public Vector2 accelerationFactor;

    IXAccelerator xacc;
    IYAccelerator yacc;

    Vector2 velocity;

    void Start()
    {
        xacc = GetComponent<IXAccelerator>();
        yacc = GetComponent<IYAccelerator>();
    }

    void Update()
    {
        float nyacc = Mathf.Lerp(-g, accelerationFactor.y, yacc.YAcceleration);

        velocity.y = nyacc;
        velocity.x = xacc.XAcceleration * accelerationFactor.x;

        velocity.y = Mathf.Clamp(velocity.y, -maxYSpeed, maxYSpeed);
        velocity.x = Mathf.Clamp(velocity.x, -maxXSpeed, maxXSpeed);

        Vector2 oldpos = transform.position;
        
        transform.position = transform.position + (Vector3)velocity * Time.deltaTime;

        //velocity = transform.position - (Vector3)oldpos / Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            other.gameObject.SetActive(false);
            BananaCollection.Instance.Bananas++;
        }

        if (other.gameObject.tag == "Breakable")
        {
            if (velocity.y >= breakVelocity)
            {
                other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                return;
            }
        }

        if (other.gameObject.tag == "Spikes")
        {
            SceneManager.LoadScene("Failure");
        }
    }
}