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
    public GameObject upObj;
    public GameObject downObj;
    public GameObject leftObj;
    public GameObject rightObj;
    public GameObject render;

    public float jumpVelocity;
    public float pikeVelocity;
    public float dashVelocity;

    public bool isGrounded = true;
    public bool canDash = true;

    Ray downRay;
    Ray rightRay;
    Ray leftRay;
    Ray upRay;
   

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
        downRay = new Ray(downObj.transform.position, Vector3.down*rayLength);
        Debug.DrawRay(downObj.transform.position, Vector3.down * rayLength);

        rightRay = new Ray(rightObj.transform.position, Vector3.right * rayLengthWall);
        Debug.DrawRay(rightObj.transform.position, Vector3.right * rayLengthWall);

        leftRay = new Ray(leftObj.transform.position, Vector3.left * rayLengthWall);
        Debug.DrawRay(leftObj.transform.position, Vector3.left * rayLengthWall);

        upRay = new Ray(upObj.transform.position, Vector3.up * rayLength);
        Debug.DrawRay(upObj.transform.position, Vector3.up * rayLength);



        //Reset Jump & dash quand le Raycast touche le sol
        if (Physics.Raycast (downRay,out RaycastHit groundInfo,rayLength))
        {
            Debug.Log(groundInfo.collider.tag);
            if (groundInfo.collider.tag == "Ground")
                isGrounded = true;
                 canDash = true;

        }

        else isGrounded = false;

        //Reset Jump && dash quand le Raycast Touche un wall

    if (Physics.Raycast (rightRay, out RaycastHit wallInfo, rayLengthWall))
        {
            
            Debug.Log(wallInfo.collider.tag);
            if (wallInfo.collider.tag == "Ground")
            {
                isGrounded = true;
                canDash = true;
                //speed = 5; // Quand raycast touche un mur speed = 0 pour pas passer à travers
            }
        }

        if (Physics.Raycast(leftRay, out RaycastHit wallInfoLeft, rayLengthWall))
        {
            Debug.Log(wallInfoLeft.collider.tag);
            if (wallInfoLeft.collider.tag == "Ground")
                isGrounded = true;
                canDash = true;
            //speed = 5; // Quand raycast touche un mur speed = 0 pour pas passer à travers
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
        
        //direction du personnage : on fait rotate le render et non le player en tant que tel
        if (horizontalMovement <=-0.1)
        {
            render.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (horizontalMovement >= 0.1)
        {
            render.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 

        }

       
        //jump du personnage
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = Vector3.up * jumpVelocity;
            Debug.Log("jump");
           
        }

        //Piqué
        if (Input.GetKeyDown(KeyCode.A) && isGrounded == false)
        {
            rb.velocity = Vector3.down * pikeVelocity;
            Debug.Log("piqué");
            
        }
     
        //Dash

        if (Input.GetKeyDown(KeyCode.E) && canDash == true)
        {
            
            Debug.Log("dash");
            if (horizontalMovement <= -0.1)
            {
                rb.velocity = Vector3.left * dashVelocity;
            }

            if (horizontalMovement >= 0.1)
            {
                rb.velocity = Vector3.right * dashVelocity;
            }

            canDash = false;
            // else tu dash pas

        }
    }  
    
  
}
