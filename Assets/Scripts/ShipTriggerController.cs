using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTriggerController : MonoBehaviour
{
    private int totalCubesCollected = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cube"))
        {
            //Destroy(other.gameObject, 1);
            other.GetComponent<CubeController>().GetCollected();
            totalCubesCollected += 1;
                Debug.Log("We have collected " + totalCubesCollected + " cubes.");
        }
    }
}
