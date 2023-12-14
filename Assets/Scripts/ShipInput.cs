using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [SerializeField] 
    private ShipCamera shipCam;
    private ShipMovement shipMove;

    [SerializeField]
    public float launchForce = 30;
    [SerializeField]
    public float returnDelay = 1;
    [SerializeField]
    public float returnIntervalSeconds = 5;
    [SerializeField]
    AnimationCurve curve;

    public bool readyToLaunch = true;
    private Rigidbody rb;
    private Transform ballStart;

    // Start is called before the first frame update
    void Start()
    {
        if (shipCam == null) shipCam = this.gameObject.GetComponent<ShipCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToLaunch)
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) shipCam.ZoomOut();
        if(Input.GetKeyUp(KeyCode.Mouse1)) shipCam.DefaultZoom();
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

        while (counter < 1)
        {
            counter += Time.deltaTime / returnIntervalSeconds;
            this.transform.position = Vector3.Lerp(startPosition, ballStart.position, curve.Evaluate(counter));
            this.transform.rotation = Quaternion.Lerp(startRotation, ballStart.rotation, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        readyToLaunch = true;
    }
}
