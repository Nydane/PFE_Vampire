using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOrb : MonoBehaviour
{
    public BloodBar bloodBar;
    public PlayerController playerController;
    public Rigidbody rb;

    private Vector3 playerPosition;
    public Transform playerTransform;
    public Vector3 direction;
    public float movingSpeed =20f;

    public int bloodValue = 20;
    // Start is called before the first frame update



   
    private void Update()
    {
        playerPosition = playerTransform.position;
        direction = (playerPosition - transform.position).normalized;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.GetBlood(bloodValue);
            Destroy(gameObject);
        }
    }

 
}
