using UnityEngine;
using System.Collections;


public class hullHydrodynamics : MonoBehaviour {
    private float headingAngle;
    private Vector3 Velocity;
    public Vector3 Heading;
    public float trackingAdj;
    public Rigidbody hullRB;
    private Vector3 trackingForce;
    private float forceMag;
    private float angleBetween;

    // First version written 9/5/2016
    // Basic first piece of physics here is to simply apply a force that makes the boat track along its keel
    // if the angle between the boats velocity and hull centerline is nonzero a torque is applied which models the tracking force boat hulls create


    void Start () {
        GameObject hullObj = GameObject.Find("Hull");
        hullRB = hullObj.GetComponent<Rigidbody>();
        hullRB.ResetCenterOfMass();


    }
	
	// calculate and apply tracking force
	void Update () {
        
        // Get current velocity, restrict it to 2D
        Velocity = hullRB.velocity;
        Velocity.y = 00.0f;
        
        // Get euler angle of hull
        headingAngle = hullRB.transform.eulerAngles.y;
        // Calculate heading, This may need to be edited depending on models and meshes used.  This particle hull is rotated -90 degrees
        //  Maybe that is why I had to make the z component negative? Given that Sin is odd and Cos is even I suppose that makes sense
        // it would be good to edit this heading definition to be more generally applicable, it feels situational. 
        Heading = new Vector3(Mathf.Cos((headingAngle+180)*2*Mathf.PI/360), 0.0f, -Mathf.Sin((headingAngle+180) * 2 * Mathf.PI / 360));

        // F ~ |V|*trackingADJ
        forceMag = Velocity.sqrMagnitude * trackingAdj;

        // Calculate angle between
        angleBetween = Vector3.Angle(Heading, Velocity);
        // use cross product to calculate polarity 
        Vector3 cross = Vector3.Cross(Heading, Velocity);
        if (cross.y < 0) angleBetween = -angleBetween;
        // define force vector
        trackingForce = new Vector3(0.0f, 0.0f, 0.0f);
        trackingForce.y = forceMag*(angleBetween);       

        // apply torque
        hullRB.AddTorque(trackingForce);


	
	}
}
