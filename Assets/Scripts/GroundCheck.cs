using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider collider) 
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }
    private void OnTriggerExit(Collider collider) 
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }
}
