using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOrb : MonoBehaviour
{
    [Header("Pomme")]
    public BloodBar bloodBar;
    public PlayerController playerController;
    public Rigidbody rb;
    public Transform playerTransform;
    public Collider sCollider;



    private Vector3 playerPosition;

    [Header("Speed & Direction of Orb")]
    public Vector3 direction; // don't tuch ! :d
    public float movingSpeed =20f;

    [Header("IncreasePlayerSpeed")]
    public float speedIncreaseTime = 3f;
    public float increasePlayerSpeedBy = 20f;

    [Header("Blood")]
    public int bloodValue = 20;

    [Header ("Colors")]
    public Material mRed;
    public Material mBlue;
    private Renderer rend;


    [Header (" Bools")]
    public bool isRed = true;
    public bool isBlue = false;
    // Start is called before the first frame update


    private void Start()
    {
        rend = GetComponent<Renderer>();
        sCollider = GetComponent<Collider>();

        if (isRed == true)
        {
            rend.material = mRed;
        }

        if (isBlue == true)
        {
            rend.material = mBlue;
        }
    }

    private void Update()
    {
        playerPosition = playerTransform.position;
        direction = (playerPosition - transform.position).normalized;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isRed == true)
        {
            playerController.GetBlood(bloodValue);
            Destroy(gameObject);
        }

        if (other.tag == "Player" && isBlue == true)
        {
            
            
            StartCoroutine(SetTimeSpeed(speedIncreaseTime));
            rend.enabled = false;
            sCollider.enabled = !sCollider.enabled;



        }
    }
    // coroutine pour la durée du increase speed
    public IEnumerator SetTimeSpeed(float TimeSpeed)
    {
        float speedTime = TimeSpeed;
        

        if (speedTime > 0)
        {
            playerController.maxSpeed += increasePlayerSpeedBy;
            speedTime -= Time.deltaTime;
            yield return new WaitForSeconds(2);
            Debug.Log("jesuisla");
            Destroy(gameObject);
            playerController.maxSpeed -= increasePlayerSpeedBy;

        }
        




    }

}
