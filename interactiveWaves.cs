using UnityEngine;
using System.Collections;



public class interactiveWaves : MonoBehaviour
{

    public int xRes;
    public int yRes;
    public float dq;
    public int n;
    public int P;
    public float[,] verticalDerivative;
    public float[,] heightMap;
    public float[,] prevHeightMap;
    public float[,] kernel;
    public float[,] source;
    float alpha;
    float dt;
    float g;
    private float sigma = 1;

    void Start()
    {
        calculateKernel();
    }

    void FixedUpdate()
    {
        updateHeightMap();

    }

    void Update()
    {
        water.setHeightmap(heightMap);
    }

    void updateSource()
    {
        for (int i = 0; i < xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                heightMap[i, j] = heightMap[i, j] + source[i, j];
            }
        }
    }

    void updateHeightMap()
    {
        updateSource();
        convolveVerticalDerivative();
        float temp;
        for (int i = 0; i < xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                
                temp = heightMap[i, j];
                heightMap[i, j] = heightMap[i, j] * (2.0f - alpha * dt) / (1.0f + alpha * dt) - prevHeightMap[i, j] / (1.0f + alpha * dt) - verticalDerivative[i, j] * g * dt * dt / (1.0f + alpha * dt);
                prevHeightMap[i, j] = temp;
            }
        }

    }

    void convolveVerticalDerivative()
    {
        

        for (int i = 0;i< xRes;i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                float temp = 0;
                for (int k = 0; k < P+1; k++)
                {
                    for (int l = k+1; l < P + 1; l++)
                    {
                        temp = temp + kernel[k, l] * (heightMap[i + k, j + l] + heightMap[i - k, j - l] + heightMap[i + k, j - l] + heightMap[i - k, j + l]);
                    }

                }
                verticalDerivative[i, j] = heightMap[i, j] + temp;

            }

        }

    }


    float[,] calculateKernel()
    {
        float[,] ans = new float[P + 1, P + 1];
        Vector2 r;
        float qnsq;
        float tempAns;
        float Gnorm = G0();
       
        for (int k=0; k<P+1; k++)
        {
            for (int l = 0; k < P + 1; l++)
            {
                tempAns = 0;
                for (int i =1; i<n; i++)
                {
                    r.x = k;
                    r.y = l;
                    qnsq = i*i*dq*dq;
                    tempAns = tempAns + qnsq * Mathf.Exp(-sigma * qnsq) * bessj0(i * dq * r.magnitude);
                }
                ans[k, l] = tempAns / Gnorm;

            }
        }
        return ans;

    }


    float G0()
    {
        float ans = 0;
        
        float qnsq;
        for (int i = 1; i<n;i ++)
        {
            qnsq = Mathf.Pow(i * dq, 2);
            ans = ans + qnsq * Mathf.Exp(-sigma * qnsq);
        }
        return ans;
    }


    float bessj0(float x)
    //Returns the Bessel function J0(x) for any real x.
    {
        float ax, z;
        double xx, y, ans, ans1, ans2; //Accumulate polynomials in double precision.
        if ((ax = Mathf.Abs(x)) < 8.0)
        { //Direct rational function fit.
            y = x * x;
            ans1 = 57568490574.0 + y * (-13362590354.0 + y * (651619640.7
        + y * (-11214424.18 + y * (77392.33017 + y * (-184.9052456)))));
            ans2 = 57568490411.0 + y * (1029532985.0 + y * (9494680.718
            + y * (59272.64853 + y * (267.8532712 + y * 1.0))));
            ans = ans1 / ans2;
        }
        else
        { 
            z = 8.0f / ax;
            y = z * z;
            xx = ax - 0.785398164;
            ans1 = 1.0 + y * (-0.1098628627e-2 + y * (0.2734510407e-4
            + y * (-0.2073370639e-5 + y * 0.2093887211e-6)));
            ans2 = -0.1562499995e-1 + y * (0.1430488765e-3
            + y * (-0.6911147651e-5 + y * (0.7621095161e-6
            - y * 0.934945152e-7)));
            ans = Mathf.Sqrt(0.636619772f / (float)ax) * (Mathf.Cos((float)xx) * ans1 - z * Mathf.Sin((float)xx) * ans2);
        }
        return (float)ans;
    }



}
