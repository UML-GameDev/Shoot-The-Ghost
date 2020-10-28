using System.Collections.Generic;
using UnityEngine;

public class BezierPath
{
	public List<Vector3> pathPoints;
	public LineRenderer path;
	private int segments;
	public int pointCount = 100;

	//Constructor
	//iv = InitialVelocity, ip = InitialPosition, g = gravity
	public BezierPath()
	{
		path = new LineRenderer();
		path.sortingOrder = -1;

		//define the list of vector3
		//set the count to 100
		pathPoints = new List<Vector3>();
		pointCount = 100;
		path.positionCount = pointCount;
	}
	public BezierPath(LineRenderer r)
	{
		if (r == null) path = new LineRenderer();
		else path = r;

		path.sortingOrder = -1;

		pathPoints = new List<Vector3>();
		path.positionCount = pointCount;
	}

	public void ResetPath()
	{
		pathPoints.Clear();
	}



	Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		//B(t) =[(1-t)^3 * p0] + [3*(1-t)^2 * t * p1] + [3*(1-t) * t^2 * p2] + [(1-t)^3 * p3]
		//From https://en.wikipedia.org/wiki/Bézier_curve#Cubic_Bézier_curves
		
		float tt = t * t;   // t^2
		float ttt = t * tt; // t^3
		float u = 1.0f - t; // 1-t
		float uu = u * u;   // (1-t)^2
		float uuu = u * uu; // (1-t)^3

		Vector3 B = new Vector3();
		B = uuu * p0;               //(1-t)^3 * p0
		B += 3.0f * uu * t * p1;    //3*(1-t)^2 * t * p1
		B += 3.0f * u * tt * p2;    //3*(1-t) * t^2 * p2
		B += ttt * p3;              //(1-t)^3 * p3


		return (Vector2)B;
	}

	public void FindBezierPath(List<Vector3> controlPoints)
	{
		//divide the points into 4 pts for each segments
		segments = controlPoints.Count / 3;

		//iterate through each control points p0, p1, p2, p3;
		for (int s = 0; s < controlPoints.Count - 3; s += 3)
		{
			Vector3 p0 = controlPoints[s];
			Vector3 p1 = controlPoints[s + 1];
			Vector3 p2 = controlPoints[s + 2];
			Vector3 p3 = controlPoints[s + 3];

			for (int p = 0; p < (pointCount / segments); p++)
			{
				float t = (1.0f / (pointCount / segments)) * p;
				pathPoints.Add(BezierPathCalculation(p0, p1, p2, p3, t));
			}
		}
		path.SetPositions(pathPoints.ToArray());
	}
}
