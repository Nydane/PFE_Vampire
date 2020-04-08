using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Doors[] linkedDoors;
    public bool isOn = false;
    public PlayerController playerController;

    [Header("Colors")]
    public Renderer rend;
    public Material mRed;
    public Material originalColor;

    // Start is called before the first frame update
    void Start()
    {
        if (linkedDoors.Length == 0) linkedDoors = new Doors[0];
    }
    /*private void Update()
    {
        if (Physics.Raycast(playerController.orbRightRay, out RaycastHit orbRightInfo, rayOrbLength))
        {
            Debug.Log(orbRightInfo.collider.tag);
            if (orbRightInfo.collider.tag == "BloodOrb" && Input.GetButton("Fire2"))
            {
                BloodOrbMove(orbRightInfo.collider.GetComponent<BloodOrb>());
            }
        }

    }*/

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Blood")
        {
            SetOn();
         }
    }

    public void SetOn()
    {
        isOn = true;
        rend.material = mRed;
        foreach (Doors door in linkedDoors)
        {
            door.Open();

        }
    }

    public void SetOff()
    {
        isOn = false;
        rend.material = originalColor;
        foreach (Doors door in linkedDoors)
        {
            door.Close();

        }
    }

    /*public void Toggle()
    {
        isOn = !isOn;
    }*/
}
