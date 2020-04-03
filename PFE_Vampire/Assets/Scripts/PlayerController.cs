using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvements")]
    // pour les mouvements
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

    [Header("Raycasts")]
    // pour les origines des raycasts
    
    public GameObject upObj;
    public GameObject downObj;
    public GameObject leftObj;
    public GameObject rightObj;
    // longueur des raycasts
    public float rayLength = 2f;
    public float rayLengthWall = 2f;
    public float rayOrbLength = 10f;

    [Header("Pomme")]
    public GameObject render;
    public Rigidbody rb;

    [Header("Abilities")]
    // jump dash et pike
    public float jumpVelocity;
    public float pikeVelocity;
    public float dashVelocity;
    public float dashOverTime =0f;

    [Header("Bools")]
    public bool isGrounded = true;
    public bool canDash = true;

    //les raycasts
    Ray downRay;
    Ray rightRay;
    Ray leftRay;
    Ray upRay;
    Ray orbRightRay;
    Ray orbLeftRay;


    [Header("Blood")]
    // Bar de sang
    public int maxBlood = 100;
    public int currentBlood;
    public BloodBar bloodBar; // référence à notre script de bar de sang poser sur l'objet bar de sang

    [Header("Shoot")]
    // variables pour tirer
    public Transform firepoint;
    public GameObject bulletPrefab;

   

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();

        // dans le start on set notre sang au max et on dit à notre bloodbar de se mettre au max.
        currentBlood = maxBlood;
        bloodBar.SetMaxBlood(maxBlood);
    }
    
    // Update is called once per frame
    void Update()
    {
        #region RayCast
        //Système de Raycast pour avoir des informations
        downRay = new Ray(downObj.transform.position, Vector3.down*rayLength);
        Debug.DrawRay(downObj.transform.position, Vector3.down * rayLength);

        rightRay = new Ray(rightObj.transform.position, Vector3.right * rayLengthWall);
        Debug.DrawRay(rightObj.transform.position, Vector3.right * rayLengthWall);

        leftRay = new Ray(leftObj.transform.position, Vector3.left * rayLengthWall);
        Debug.DrawRay(leftObj.transform.position, Vector3.left * rayLengthWall);

        upRay = new Ray(upObj.transform.position, Vector3.up * rayLength);
        Debug.DrawRay(upObj.transform.position, Vector3.up * rayLength);

        orbRightRay = new Ray(rightObj.transform.position, Vector3.right * rayOrbLength);
        Debug.DrawRay(rightObj.transform.position, Vector3.right * rayOrbLength);

        orbLeftRay = new Ray(leftObj.transform.position, Vector3.left * rayOrbLength);
        Debug.DrawRay(leftObj.transform.position, Vector3.left * rayOrbLength);

        #endregion



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

        // bloodOrb Ray player absorb
        if (Physics.Raycast(orbRightRay, out RaycastHit orbRightInfo, rayOrbLength))
        {
            Debug.Log(orbRightInfo.collider.tag);
            if (orbRightInfo.collider.tag == "BloodOrb" && Input.GetButton("Fire2"))
            {
                BloodOrbMove(orbRightInfo.collider.GetComponent<BloodOrb>());
            }


        }

        if (Physics.Raycast(orbLeftRay, out RaycastHit orbLeftInfo, rayOrbLength))
        {
            Debug.Log(orbLeftInfo.collider.tag);
            if (orbLeftInfo.collider.tag == "BloodOrb" && Input.GetButton("Fire2"))
            {
                BloodOrbMove(orbLeftInfo.collider.GetComponent<BloodOrb>());
            }


        }
        //PlayerShoot
        if (Input.GetButtonDown("Fire1") && currentBlood >= 20 )
        {
            Shoot();
            UseBlood(20);
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
        if (Input.GetKeyDown(KeyCode.A) && isGrounded == false && currentBlood >= 20)
        {
            rb.velocity = Vector3.down * pikeVelocity;
            Debug.Log("piqué");
            UseBlood(20);

        }
     
        //Dash

        if (Input.GetKeyDown(KeyCode.E) && canDash == true && currentBlood >=20)
        {
            
            Debug.Log("dash");
            

            if (horizontalMovement <= -0.1)
            {
                StartCoroutine(SetVelocityDash(dashOverTime));
                rb.velocity = Vector3.left * dashVelocity;
                UseBlood(20);
            }

            if (horizontalMovement >= 0.1)
            {
                StartCoroutine(SetVelocityDash(dashOverTime));
                rb.velocity = Vector3.right * dashVelocity;
                UseBlood(20);
            }

            canDash = false;
            // else tu dash pas

            
        }

    }  
   public void UseBlood (int throwing)
    {
        // le sang actuel c'est celui - le throwing et on dit à la bloodbad de se mettre à current blood.
        currentBlood -= throwing;

        bloodBar.SetBlood(currentBlood);
    }

    public void GetBlood(int absorbing)
    {
        currentBlood += absorbing;

        // si jamais on dépasse le max on  set au max a nouveau.
        if (currentBlood > maxBlood)
        {
            currentBlood = maxBlood;
        }

        bloodBar.SetBlood(currentBlood);

    }

    void Shoot()
    {
        // Shooting logic
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

     public void BloodOrbMove (BloodOrb orb)
    {
       // rb.MovePosition(transform.position + new Vector3(movingSpeed,0,0) * Time.deltaTime);
        orb.rb.MovePosition(orb.transform.position + orb.direction * orb.movingSpeed * Time.deltaTime);
        
    }

    public IEnumerator SetVelocityDash (float TimeDash)
    {
        float time = TimeDash;
        while (time >0)
        {
            Vector3 vel = rb.velocity;
            rb.velocity = new Vector3(vel.x, 0, vel.z);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        



    }
}

