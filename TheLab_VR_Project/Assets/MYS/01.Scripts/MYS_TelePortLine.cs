using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYS_TelePortLine : MonoBehaviour
{
    //세점 : 컨트롤러, 텔레포트마커, 사이 높이 점 
    public Transform[] points;
    public LineRenderer line;
    [Range(0, 1.0f)]
    public float ratio = 0;

    Vector3[] linePoints = new Vector3[100];
    void Start()
    {
        line.positionCount = linePoints.Length;
    }

    void Update()
    {
        OnDrawLine();
    }
    public void OnDrawLine()
    {
        for (int i = 0; i < linePoints.Length; i++)
        {
            linePoints[i] = BezierCurve(points[0].position, points[1].position, points[2].position, i / (float)linePoints.Length);
            line.SetPosition(i, linePoints[i]);
        }
    }

    Vector3 BezierCurve(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 temp;
        Vector3 L1 = Vector3.Lerp(p1, p2, t);
        Vector3 L2 = Vector3.Lerp(p2, p3, t);
        temp = Vector3.Lerp(L1, L2, t);
        return temp;
    }
}
