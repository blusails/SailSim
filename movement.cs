using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    // Use this for initialization
    public Rigidbody rb;
    public float speed;
    public GameObject windObj;
    public Vector3 windVec;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        windObj = GameObject.Find("windObj");
        windVec = windObj.GetComponent<wind>().windVec;

    }
    void FixedUpdate()
    {
      //  float moveHorizontal = Input.GetAxis("Horizontal");
       // float moveVertical = Input.GetAxis("Vertical");

        Vector3 rotation = GetComponent<Transform>().eulerAngles;
        Vector3 normal = new Vector3(Mathf.Sin(rotation.y), Mathf.Sin(rotation.x)*Mathf.Cos(rotation.z), Mathf.Cos(rotation.x)*Mathf.Cos(rotation.y));
        Vector3 toMove = Vector3.Project(windVec, normal);
        rb.AddForce(toMove*speed);

    } 
}
