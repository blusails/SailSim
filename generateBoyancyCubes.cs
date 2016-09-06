using UnityEngine;
using UnityEditor;
using System.Collections;

public class proceduralBoyancy {

	// Use this for initialization
    [MenuItem("GameObject/Create Other/Boyancy Cubes...")]
	static public void generateBoyancyCubes () {
        GameObject hull = GameObject.Find("Hull");
        Mesh hullMesh = hull.GetComponent<MeshFilter>().mesh;
        Vector3[] verticies = hullMesh.vertices;

        foreach (Vector3 vertex in verticies)
        {
            createBoyantCube(hull.transform.TransformPoint(vertex));
        }
    }

    static void createBoyantCube(Vector3 loc)
    {
        GameObject hull = GameObject.Find("Hull");
        GameObject container = GameObject.Find("BoyancyCubes");
        GameObject boyCube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        boyCube.transform.parent = container.transform;
        boyCube.transform.position = loc;
        boyCube.AddComponent<Rigidbody>();
        boyCube.AddComponent<boyancy>();
        boyCube.AddComponent<FixedJoint>();
        Joint weld = boyCube.GetComponent<FixedJoint>();
        weld.connectedBody = hull.GetComponent<Rigidbody>();
        boyancy cubeScript = boyCube.GetComponent<boyancy>();
        cubeScript.waterLine = 25;
        cubeScript.density = 1;
        cubeScript.gravity = 1; 
        


    }


	

}
