using UnityEngine;
using System.Collections;

public class sailForce : MonoBehaviour {
    public GameObject windObj;
    public GameObject sails;
    public GameObject leadingObj;
    public int numSails;
    public Rigidbody leadingSailTile;
    public Rigidbody trailingSailTile;
    public Rigidbody mastRB;
    public Rigidbody hullRB;
    public Vector3 sailAxisVec;
    public float wsTheta;  // angle between sail axis, defined by line between leading and trail sail tiles, and the current wind vector
    public Vector3 sailForceVec;
    public float pressure;
    public float forceAdj;

	// primary task here is to find the leading and trailing sail tiles
	void Start () {

        // get wind,sails and leading objects
        // defining the Mast as the leading object helps identify leading and trailing sail tiles as well
        // as direction of travel
        windObj = GameObject.Find("windObj");
        leadingObj = GameObject.Find("Mast");
        sails = GameObject.Find("SailContainer");
        mastRB = leadingObj.GetComponent<Rigidbody>();
        GameObject hullObj = GameObject.Find("Hull");
        hullRB = hullObj.GetComponent<Rigidbody>();
        // get number of sail panels
        Transform sailTrans = sails.GetComponent<Transform>();
        int numSails = sailTrans.childCount;

        // get leading object position
        Vector3 mastPos = leadingObj.GetComponent<Rigidbody>().position;

        // create vector3[numSails] containing each position of each sail panel
        Rigidbody[] sailRBs = sails.GetComponentsInChildren<Rigidbody>();
        int i = 0;
        Vector3[] sailLocs = new Vector3[numSails];
        Vector3 tempPos;
        foreach (Rigidbody rb in sailRBs)
        {
            tempPos = rb.position;

            sailLocs[i] = tempPos;
            i += 1;

        }

        float[] sailDist = new float[numSails];
        i = 0;
      
        float maxDist = 0.0f;
        float minDist = 1 / 0.0f;
        float tempDist;
        foreach (Vector3 posVec in sailLocs)
        {
            tempDist = Vector3.Distance(posVec, mastPos);
            sailDist[i] = tempDist;
            if (tempDist < minDist)
            {
                minDist = tempDist;
                leadingSailTile = sailRBs[i];
            }

            if (tempDist > maxDist)
            {
                maxDist =  tempDist;
                trailingSailTile = sailRBs[i];
            }
            i = i + 1;
        }
    }

    // Update is called once per frame
    void Update () {
        Vector3  windVec = windObj.GetComponent<wind>().windVec;
        sailAxisVec = new Vector3(leadingSailTile.position.x - trailingSailTile.position.x, leadingSailTile.position.y - trailingSailTile.position.y, leadingSailTile.position.z - trailingSailTile.position.z);
        wsTheta = Vector3.Angle(sailAxisVec, windVec);

        wsTheta = Mathf.Abs(wsTheta-180.0f);

        if (wsTheta<30.0f)  // sail is luffing
        {
            sailForceVec = new Vector3(0.0f, 0.0f, 0.0f);

        }
        if (wsTheta > 30.0f && wsTheta <90)  // pressure force
        {
            pressure = Vector3.Magnitude(windVec);
            sailForceVec = sailAxisVec*pressure*forceAdj;
            hullRB.AddForce(sailForceVec);
        }


    }
}
