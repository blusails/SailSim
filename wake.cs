using UnityEngine;
using System.Collections;

public class wake : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    public float kx;
    public float kz;
    public float f;
    public float A;
    public float gamma;
    public float lerpt;
    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {

        vertices = mesh.vertices;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i].y = 0;
            vertices[i] = Vector3.Lerp(vertices[i], vertices[i]+Vector3.up * wavefunc(new Vector2(vertices[i].x+itemPos(), vertices[i].z)),lerpt);
            i++;
        }
        mesh.vertices = vertices;
    }


    float wavefunc(Vector2 relativeLoc)
    {

        float decay = Mathf.Exp(-gamma * (Mathf.Abs(relativeLoc.x)-Mathf.Abs(relativeLoc.y)*.4f));
        return A*Mathf.Cos(Mathf.Abs(relativeLoc.y)*.05f +kx * Mathf.Sqrt(Vector2.SqrMagnitude(relativeLoc)))*decay;
    }

    float itemPos()
    {
        return 10*Mathf.Sin(Time.time/4);
    }

}