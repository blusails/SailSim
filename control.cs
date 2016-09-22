using UnityEngine;
using System.Collections;

public class control : MonoBehaviour {
    public Rigidbody rudderRB;
    private ConfigurableJoint rudderJoint;
    public float rudderSensitivity;
    public float input;
    public float lowerXLim;
    public float higherXLim;
    

    // Use this for initialization
    void Start() {
        GameObject rudderObj = GameObject.Find("Rudder");
        rudderRB = rudderObj.GetComponent<Rigidbody>();
        rudderJoint = rudderObj.GetComponent<ConfigurableJoint>();
    }
	
	// Update is called once per frame

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        input = moveHorizontal;
        float newAngle = rudderSensitivity * moveHorizontal + getRudderAngle();
        setRudderLimits(newAngle);
    }

    float getRudderAngle()
    {
        float lower = rudderJoint.lowAngularXLimit.limit;
        float upper = rudderJoint.highAngularXLimit.limit;
        float currentAngle = (lower + upper) / 2.0f;

        return currentAngle;
    }

    void setRudderLimits(float angle)
    {
        if (angle > lowerXLim && angle < higherXLim)
        {
            float limitDiff = .5f;
            SoftJointLimit newLow = new SoftJointLimit();
            SoftJointLimit newHigh = new SoftJointLimit();
            newLow.limit = angle - limitDiff;
            newHigh.limit = angle + limitDiff;
            rudderJoint.lowAngularXLimit = newLow;
            rudderJoint.highAngularXLimit = newHigh;
        }
    }
}
