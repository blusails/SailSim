using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    public Transform camTrans;
    public GameObject boatObj;
	// Use this for initialization
	void Start () {
        GameObject mainCamObj = GameObject.Find("Main Camera");
        boatObj = GameObject.Find("BoatContainer");
        camTrans = mainCamObj.GetComponent<Transform>();
        

    }
	
	// Update is called once per frame
	void Update () {
        Transform boatTrans = boatObj.GetComponent<Transform>();
    }
}
