#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
[ExecuteInEditMode]
public class Bezier : MonoBehaviour
{
	
	public Transform p0;
	public Transform p1;
	public Transform p2;
	public Transform p3;
	
	public float ti = 0f;

	[HideInInspector]
	public float lenght = -1f;
	
//	private Vector3 b0 = Vector3.zero;
//	private Vector3 b1 = Vector3.zero;
//	private Vector3 b2 = Vector3.zero;
	//	private Vector3 b3 = Vector3.zero;;
	
	private float Ax;
	private float Ay;
	private float Az;
	
	private float Bx;
	private float By;
	private float Bz;
	
	private float Cx;
	private float Cy;
	private float Cz;

	private const int LENGHT_STEP = 100;

	void Update()
	{
		#if UNITY_EDITOR
		ComputeLenght();
		#endif
	}

	// Init function v0 = 1st point, v1 = handle of the 1st point , v2 = handle of the 2nd point, v3 = 2nd point
	// handle1 = v0 + v1
	// handle2 = v3 + v2
	public Bezier( Transform v0, Transform v1, Transform v2, Transform v3 )
	{
		p0 = v0;
		p1 = v1;
		p2 = v2;
		p3 = v3;
	}

	public void SetBezierParam( Transform v0, Transform v1, Transform v2, Transform v3 )
	{
		p0 = v0;
		p1 = v1;
		p2 = v2;
		p3 = v3;
	}
	
	// 0.0 >= t <= 1.0
	public Vector3 GetPointAtTime( float t )
	{
		float u = 1 - t;
		float tt = t*t;
		float uu = u*u;
		float uuu = uu * u;
		float ttt = tt * t;
		
		Vector3 p = uuu * p0.position; //first term
		p += 3 * uu * t * p1.position; //second term
		p += 3 * u * tt * p2.position; //third term
		p += ttt * p3.position; //fourth term
		
		return p;
	}

	public float ComputeLenght()
	{
		lenght = 0f;
		Vector3 lastPointPosition = p0.position;
		for(int i=1; i<LENGHT_STEP; i++)
		{
			Vector3 curPoint = GetPointAtTime((float)i/(float)LENGHT_STEP);
			lenght += Vector3.Distance(lastPointPosition, curPoint);
			lastPointPosition = curPoint;
		}
		return lenght;
	}

	#if UNITY_EDITOR
	public void OnDrawGizmos()
	{
		try
		{
			//Draw Bezier Curve
			double width = 4;
			Handles.DrawBezier(p0.position, 
			                   p3.position, 
			                   p1.position, 
			                   p2.position, 
			                   Color.red, 
			                   null,
			                   (float)width);

//			//Draw Handler's Lines
//			Handles.DrawLine(p0.position, p1.position);
//			Handles.DrawLine(p2.position, p3.position);
//
//			//Draw Points
			Handles.color = Color.red;
			Handles.DotCap(0, p0.position, p0.rotation, 0.5f);
//			Handles.DotCap(0, p1.position, p1.rotation, 4f);
//			Handles.DotCap(0, p2.position, p2.rotation, 4f);
			Handles.DotCap(0, p3.position, p3.rotation, 0.5f);
		}
		catch(Exception e)
		{
			Debug.LogWarning("Il manque un point sur une courbe de Bezier, espèce de magicarpe " + e.ToString());
		}
	}
	#endif
}


////CustomEditor
//[CustomEditor(typeof(Bezier))]
//public class BezierEditor : Editor
//{
//
//}