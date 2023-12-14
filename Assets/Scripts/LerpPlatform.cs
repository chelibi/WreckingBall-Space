using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPlatform : MonoBehaviour
{
    LineRenderer lr;
    Transform platformBase, start, end;

    [SerializeField]
    float intervalInSeconds = 2;
    
    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    bool looping = true;

    [SerializeField]
    bool updateBeam = false;

    // Start is called before the first frame update
    void Start()
    {
        platformBase = this.transform.GetChild(0);
        start = this.transform.GetChild(1);
        end = this.transform.GetChild(2);

        lr = this.gameObject.GetComponent<LineRenderer>();
        if(lr)
        {
            lr.SetPosition(0, start.position);
            lr.SetPosition(1, end.position);
        }

        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if(updateBeam)
        {
            lr.SetPosition(1, platformBase.position);
        }
    }

    IEnumerator Move()
    {
        float counter = 0;
        
        yield return new WaitForSeconds(1);

        while (counter < intervalInSeconds)
        {
            counter += Time.deltaTime / intervalInSeconds;
            platformBase.position = Vector3.Lerp(start.position, end.position, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        while (counter > 0)
        {
            counter -= Time.deltaTime / intervalInSeconds;
            platformBase.position = Vector3.Lerp(start.position, end.position, curve.Evaluate(counter));
            yield return new WaitForEndOfFrame();
        }

        if (looping)
        {
            StartCoroutine(Move());
        }
    }
}
