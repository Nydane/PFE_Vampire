using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizescript : MonoBehaviour
{
    [Header("Pomme")]
    public GameObject cube;
    public GameObject player;

    [Header("Size")]
    public Vector3 smallSize;
    public Vector3 bigSize;

    [Header("Bools")]
    public bool getBig = true;
    public bool getSmall = false;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (getBig == true)
        {
            cube.gameObject.transform.localScale = bigSize;
            getSmall = false;
        }

        if (getSmall == true)
        {
            cube.gameObject.transform.localScale = smallSize;
            getBig = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        getBig = true;

        /*else if (getSmall == true)
        {
            cube.gameObject.transform.localScale = smallSize;

        }*/

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


}
