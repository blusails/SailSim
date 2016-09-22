using UnityEngine;
using System.Collections;

public class rbAxis : MonoBehaviour {

    public Rigidbody frontRB;
    public Rigidbody backRB;
    public Vector3 initialAxis;
    public Vector3 axis;
    public Vector3 axis2;
    public float yEuler;
    public float qAngle = 0.0f;
    public Vector3 qAxis = Vector3.zero;
    Transform objTrans;
	// Use this for initialization
	void Start () {
        Vector3 frontPos = frontRB.transform.position;
        Vector3 backPos = backRB.transform.position;
        axis = new Vector3(frontPos.x - backPos.x, frontPos.y - backPos.y, frontPos.z - backPos.z);
        objTrans = GetComponent<Transform>();
        yEuler = objTrans.eulerAngles.y;
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 frontPos = frontRB.transform.position;
        Vector3 backPos = backRB.transform.position;
        axis = new Vector3(frontPos.x-backPos.x, frontPos.y-backPos.y,frontPos.z-backPos.z);
        axis.Normalize();
        yEuler = objTrans.eulerAngles.y;
        axis2 = new Vector3(Mathf.Cos((yEuler) * 2 * Mathf.PI / 360), 0.0f, -Mathf.Sin((yEuler) * 2 * Mathf.PI / 360));
        axis2 = axis2 - initialAxis;
        objTrans.rotation.ToAngleAxis(out qAngle, out qAxis);
    }
}
