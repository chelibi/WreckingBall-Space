using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    [SerializeField]
    public float returnDelay = 1;
    [SerializeField]
    public float launchForce = 30;
    [SerializeField]
    public float returnIntervalSeconds = 5; 
    [SerializeField]
    AnimationCurve curve;

    private Rigidbody rb;
    private Transform ballStart;
    public bool readyToLaunch = true;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballStart = GameObject.Find("BallStart").transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && readyToLaunch)
        {
            Launch();
        }
        if(readyToLaunch)
        {
            this.transform.position = ballStart.position;
            this.transform.rotation = ballStart.rotation;
        }
    }
    public void Launch()
    {
        Debug.Log("Launching!");
        readyToLaunch = false;
        StartCoroutine(Return());
        rb.isKinematic = false;
        rb.AddForce(ballStart.forward * launchForce, ForceMode.Impulse);
    }

    private IEnumerator Return()
    {
        yield return new WaitForSeconds(returnDelay);
        rb.isKinematic = true;
        //this.transform.localPosition = new Vector3(0, 0, 0);
        //this.transform.localRotation = Quaternion.identity;

        float counter = 0;
        Vector3 startPosition = this.transform.position;
        //Vector3 endPosition = ballStart.position;

        Quaternion startRotation = this.transform.rotation;

        while(counter < 1)
        {
            counter += Time.deltaTime / returnIntervalSeconds;
            this.transform.position = Vector3.Lerp(startPosition, ballStart.position, curve.Evaluate(counter));
            this.transform.rotation = Quaternion.Lerp(startRotation, ballStart.rotation, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        readyToLaunch = true;
    }
}
