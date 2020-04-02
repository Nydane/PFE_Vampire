using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizescript : MonoBehaviour
{
    public GameObject cube;
    public GameObject player;
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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (getBig == true)
        {
            cube.gameObject.transform.localScale = new Vector3(5, 5, 5);
        }
        else if (getSmall == true)
        {
            cube.gameObject.transform.localScale = new Vector3(1, 1, 1);

        }

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
