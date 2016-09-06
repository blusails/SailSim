using UnityEngine;
using System.Collections;

public class wind : MonoBehaviour {
    public Transform arrowTrans;
    public Vector3 windVec;
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 arrow = new Vector3(Mathf.Sin(arrowTrans.eulerAngles.y), 0, Mathf.Cos(arrowTrans.eulerAngles.y));
        //arrowTrans.Rotate(-Vector3.up * Vector3.Angle(arrow, windVec));
	}
}
