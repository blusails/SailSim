using UnityEngine;
using System.Collections;



public class boyancy : MonoBehaviour {
    public float waterLine;
    public Rigidbody rb;
    public Vector3 com;
    public float radius;
    public float density;
    public float gravity;
    public Vector3 loc;
    public float waterHeight;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        com = rb.centerOfMass;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3 radVert = vertices[1];
        radius = Vector3.Distance(radVert, com);

    
}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;
        
        rb = GetComponent<Rigidbody>();
        loc = GetComponent<Rigidbody>().position;
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        Physics.Raycast(loc, dwn,out hit, 100);
        Vector3 textureLoc = hit.point;
        waterHeight = textureLoc.y;
        com = rb.centerOfMass+loc;
        float diff = com.y - waterLine;
        if (diff <= radius) {
            if (diff>2.0f*radius)
            {
                diff = 2.0f * radius;
            }
            float volume = Mathf.Abs(Mathf.PI / 3.0f * Mathf.Pow(diff, 2) * (3.0f * radius - diff));
            float boyantForce = volume * density * gravity;
            rb.AddForce(new Vector3(0.0f,boyantForce,0.0f));
        }
	}
}
