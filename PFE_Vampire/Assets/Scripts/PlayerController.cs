using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float maxSpeed = 1;
    [SerializeField]
    private float speedIncr = 1;
    [SerializeField]
    private float speedDecr = 0.999f;
    [SerializeField]
    private float stopLimite = 5f;


    private float mouvementDcr;

    public Rigidbody rb;
    public float jumpVelocity;

    public bool isGrounded = true;

    Ray groundRay;
    Ray wallRay;
    Ray wallRayLeft;
    public float rayLength = 2f;
    public float rayLengthWall = 2f;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //Système de Raycast pour avoir des informations
        groundRay = new Ray(transform.position, Vector3.down*rayLength);
        Debug.DrawRay(transform.position, Vector3.down * rayLength);
        wallRay = new Ray(transform.position, Vector3.right * rayLengthWall);
        Debug.DrawRay(transform.position, Vector3.right * rayLengthWall);
        wallRayLeft = new Ray(transform.position, Vector3.left * rayLengthWall);
        Debug.DrawRay(transform.position, Vector3.left * rayLengthWall);

        //Reset Jump quand le Raycast touche le sol
        if (Physics.Raycast (groundRay,out RaycastHit groundInfo,rayLength))
        {
            Debug.Log(groundInfo.collider.tag);
            if (groundInfo.collider.tag == "Ground")
                isGrounded = true;

        }

        else isGrounded = false;

        //Reset Jump quand le Raycast Touche un wall

    if (Physics.Raycast (wallRay, out RaycastHit wallInfo, rayLengthWall))
        {
            
            Debug.Log(wallInfo.collider.tag);
            if (wallInfo.collider.tag == "Wall")
            {
                isGrounded = true;
                speed = 0; // Quand raycast touche un mur speed = 0 pour pas passer à travers
            }
        }

        if (Physics.Raycast(wallRayLeft, out RaycastHit wallInfoLeft, rayLengthWall))
        {
            Debug.Log(wallInfoLeft.collider.tag);
            if (wallInfoLeft.collider.tag == "Wall")
                isGrounded = true;
        }

        

        //Movement et rotation du personnage
        float horizontalMovement = Input.GetAxis("Horizontal");
        


        // incrementation et décrementation de vitesse
        if (horizontalMovement > 0)
        {
            if (speed < 0) speed = 0;

            if (Mathf.Abs(speed) < maxSpeed)
            {
                speed += (Time.deltaTime * speedIncr);
                if (Mathf.Abs(speed) > maxSpeed)
                {
                    speed = maxSpeed;
                }
            }

        }
        else if (horizontalMovement < 0)
        {
            if (speed > 0) speed = 0;


            if (Mathf.Abs(speed) < maxSpeed)
            {
                speed -= (Time.deltaTime * speedIncr);
                if (Mathf.Abs(speed) > maxSpeed)
                {
                    speed = -maxSpeed;
                }
            }

        }
        else 
        {
            speed *= speedDecr;   // speedDecr entre 0 et 1
            if (Mathf.Abs(speed) < stopLimite)
            {
                speed = 0;
            }
        }

                       
        //ce qui fait bouger le perso
        rb.MovePosition(transform.position + new Vector3(speed, 0, 0) * Time.deltaTime);
        
        //direction du personnage
        if (horizontalMovement <=-0.1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (horizontalMovement >= 0.1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); 

        }

       
        //jump du personnage
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            Debug.Log("jump");
           
        }

     
    }  
    
  
}
