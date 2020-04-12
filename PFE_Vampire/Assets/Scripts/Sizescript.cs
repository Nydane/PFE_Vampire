using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizescript : MonoBehaviour
{
    [Header("Pomme")]
    public GameObject cube;
    public GameObject player;

    [Header ("Colors")]
    public Renderer rend;
    public Material mRed;
    public Material originalColor;

    [Header("Size")]
    public Vector3 smallSize;
    public Vector3 bigSize;

    [Header("Bools")]
    public bool getBig = true;
    public bool getSmall = false;
    public bool isMoving = false;
    public bool hasBlood = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (getBig == true)
        {
            cube.gameObject.transform.localScale = bigSize;
            getSmall = false;
            rend.material = mRed;
        }

        if (getSmall == true)
        {
            cube.gameObject.transform.localScale = smallSize;
            getBig = false;
            rend.material = originalColor;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blood")
        {
            SetSizeBig();
        }
        
        // script pas use pour l'instant
        if (other.gameObject == player && isMoving == true)
        {
            player.transform.parent = transform; // lorsque la plateforme se déplace, met le joueur en parent pour que les deux bougent ensemble
        }
            
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && isMoving == false)
        {
            player.transform.parent = null; 
        }
    }

    public void SetSizeBig ()
    {
        getBig = true;
        getSmall = false;
        hasBlood = true;
    }

    public void SetSizeSmall ()
    {
        getBig = false;
        getSmall = true;
        hasBlood = false;
    }
}
