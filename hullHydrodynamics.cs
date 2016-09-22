using UnityEngine;
using System.Collections;


public class hullHydrodynamics : MonoBehaviour {
    private float headingAngle;
    private Vector3 Velocity;
    public Vector3 Heading;
    public Vector3 rudderAxis;
    public float trackingAdj;
    public float rudderAdj;
    public Rigidbody hullRB;
    public Rigidbody rudderRB;
    public Rigidbody rudderHardPointRB;
    private Vector3 trackingForce;
    private float forceMag;
    public float angleBetween;
    public float angleBetweenRudder;
    public Vector3 rudderForce;
    private ConfigurableJoint rudderJoint;

    // First version written 9/5/2016
    // Basic first piece of physics here is to simply apply a force that makes the boat track along its keel
    // if the angle between the boats velocity and hull centerline is nonzero a torque is applied which models the tracking force boat hulls create


    void Start () {
        GameObject hullObj = GameObject.Find("Hull");
        hullRB = hullObj.GetComponent<Rigidbody>();
        hullRB.ResetCenterOfMass();
        GameObject rudderObj = GameObject.Find("Rudder");
        rudderRB = rudderObj.GetComponent<Rigidbody>();
        rudderJoint = rudderObj.GetComponent<ConfigurableJoint>();
        GameObject rudderHPObj = GameObject.Find("rudderHardPoint");
        rudderHardPointRB = rudderHPObj.GetComponent<Rigidbody>();


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
        Heading = new Vector3(Mathf.Cos((headingAngle)*2*Mathf.PI/360), 0.0f, -Mathf.Sin((headingAngle) * 2 * Mathf.PI / 360));
        Heading = Quaternion.Euler(0, 180, 0) * Heading;
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

        // apply torque to hull for tracking
        hullRB.AddTorque(trackingForce);

        // Rudder physics section
        float rudderAngle = getRudderAngle();
        rudderAxis = new Vector3(Mathf.Cos((rudderAngle) * 2 * Mathf.PI / 360), 0.0f, -Mathf.Sin((rudderAngle) * 2 * Mathf.PI / 360));
        //rudderAxis = rudderRB.transform.TransformDirection(rudderAxis);
        rudderAxis = Quaternion.Euler(0, headingAngle, 0) * rudderAxis;

        // Calculate angle between
        angleBetweenRudder = Vector3.Angle(rudderAxis, Velocity);
        // use cross product to calculate polarity 
        cross = Vector3.Cross(rudderAxis, Velocity);
        if (cross.y < 0) angleBetweenRudder = -angleBetweenRudder;
        float rudderForceMag = Mathf.Abs(Mathf.Sin(Mathf.Abs(angleBetweenRudder) *2*Mathf.PI/360)) * rudderAdj;
        if (angleBetweenRudder > 0)
        {
            rudderForce = Quaternion.Euler(0, -45, 0) * Velocity *Velocity.sqrMagnitude* rudderForceMag;
        }

        if (angleBetweenRudder < 0)
        {
            rudderForce = Quaternion.Euler(0, 45, 0) * Velocity *Velocity.sqrMagnitude* rudderForceMag;
        }

        rudderHardPointRB.AddForce(rudderForce);


    }
    float getRudderAngle()
    {
        float lower = rudderJoint.lowAngularXLimit.limit;
        float upper = rudderJoint.highAngularXLimit.limit;
        float currentAngle = (lower + upper) / 2.0f;

        return currentAngle;
    }
}
