using UnityEngine;
using System.Collections;

public class motorForce : MonoBehaviour {
    public Rigidbody hullRB;
    public Vector3 heading;
    public float power;
    private hullHydrodynamics hullHD;
    // Use this for initialization
    void Start () {
        GameObject hullObj = GameObject.Find("Hull");
        hullRB = hullObj.GetComponent<Rigidbody>();
        hullHD = hullObj.GetComponent<hullHydrodynamics>();
        heading = hullHD.Heading;

	}
	
	// Update is called once per frame
	void Update () {
        heading = hullHD.Heading;
        hullRB.AddForce(heading * power);


    }
}
