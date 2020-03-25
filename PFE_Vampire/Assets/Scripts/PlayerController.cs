using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    public Rigidbody rb;
    public float jumpVelocity;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.MovePosition(transform.position + Vector3.right * -speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
        }

        //Jump
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpVelocity;
            
        }
       
    }
}
