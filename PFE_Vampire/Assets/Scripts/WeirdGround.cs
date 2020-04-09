using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdGround : MonoBehaviour
{

    [SerializeField]
    private float bouncyPower = 50f;

    [Header("bounce direction")]
    public bool leftBounce;
    public bool rightBounce;
    public bool topBounce;
    public bool downBounce;

    public bool isBouncy;

    public PlayerController playerController;

    public Renderer rend;
    public Material mRed;
    public Material originalColor;

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
        if (other.tag == "Blood")
        {
            BouncyPlatformOn();
        }
        if (other.tag == "Player" && isBouncy == true)
        {
            Bouncing();

        }
    }
    public void BouncyPlatformOn ()
    {
        isBouncy = true;
        rend.material = mRed;
        
    }

    public void BouncyPlatformOff()
    {
        isBouncy = false;
        rend.material = originalColor;
    }

    public void Bouncing ()
    {
        if (topBounce == true)
        {
           playerController.rb.velocity = Vector3.up * bouncyPower;
        }

        if (downBounce == true)
        {
            playerController.rb.velocity = Vector3.down * bouncyPower;
        }

        if (leftBounce == true)
        {
            playerController.rb.velocity = Vector3.left * bouncyPower;
        }

        if (rightBounce == true)
        {
            playerController.rb.velocity = Vector3.right * bouncyPower;
        }

    }
}
