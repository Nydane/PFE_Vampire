using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodWall : MonoBehaviour
{
    public PlayerController playerController;
    public float bloodWallVelocity = 20f;
    public BloodBar bloodBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GiveMaxBlood(); // should give maxblood everytime, if not you suck.if not not put a highter number huehue.
            
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        
        
        if (other.tag == "Player" && Input.GetKey(KeyCode.D))
        {
            playerController.rb.velocity = Vector3.right * bloodWallVelocity;
            
        }

        if (other.tag == "Player" && Input.GetKey(KeyCode.Q))
        {
            playerController.rb.velocity = Vector3.left * bloodWallVelocity;
            
        }

        if (other.tag == "Player" && Input.GetKey(KeyCode.Z))
        {
            playerController.rb.velocity = Vector3.up * bloodWallVelocity;
            
        }

        if (other.tag == "Player" && Input.GetKey(KeyCode.S))
        {
            playerController.rb.velocity = Vector3.down * bloodWallVelocity;
            
        }
        
    
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.isGrounded = true;
        playerController.canDash = true;
    }
}
