using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private int cubeSpeed = 0;
    public int riseSpeed = 0;

    private CubeAudio cubeAudio;
    void Start()
    {
        cubeSpeed = Random.Range(1, 10);
        cubeAudio = this.GetComponent<CubeAudio>();
    }

    void Update()
    {
        this.transform.Translate(0, riseSpeed * Time.deltaTime, 0);
    }

    public void GetCollected()
    {
        Debug.Log("This Cube Is Getting Collected, Their Speed Is " + cubeSpeed);
        
        if (cubeSpeed > 6)
        {
            this.GetComponent<Renderer>().material.color = Color.green;

            riseSpeed = 5;

            this.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(this.GetComponent<Collider>());

            Destroy(this.gameObject, 5f);

            cubeAudio.PlayCollectionAudio(true);
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.red;

            Destroy(this.gameObject, 1f);

            cubeAudio.PlayCollectionAudio(false);
        }
    }
}
