using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    public Rigidbody rb;
    public float jumpVelocity;

    public Collider groundCollider;
    public bool isGrounded = true;

    Ray groundRay;
    Ray wallRay;
    Ray wallRayLeft;
    public float rayLength = 2f;
    public float rayLengthWall = 2f;

    

    // Start is called before the first frame update
    void Start()
    {

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
                isGrounded = true;
        }

        if (Physics.Raycast(wallRayLeft, out RaycastHit wallInfoLeft, rayLengthWall))
        {
            Debug.Log(wallInfoLeft.collider.tag);
            if (wallInfoLeft.collider.tag == "Wall")
                isGrounded = true;
        }



        //Movement et rotation du personnage
        float horizontalMovement = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalMovement, 0, 0) * Time.deltaTime * speed,Space.World);

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
