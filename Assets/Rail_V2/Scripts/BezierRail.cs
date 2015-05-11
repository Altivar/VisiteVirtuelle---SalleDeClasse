#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BezierRail : MonoBehaviour {

	#region Properties
	public GameObject pointPrefab = null;
	public List<BezierPoint> points = new List<BezierPoint>();
	[HideInInspector]
	public List<Bezier> curves = new List<Bezier> ();

	internal float distance
	{
		get
		{
			return points [points.Count - 1].totalDistance;
		}
	}
	#endregion

	#region Object Management
	void Start()
	{
		#if UNITY_EDITOR
		InitRail ();
		#endif
	}

	void Update()
	{
		#if UNITY_EDITOR
		UpdateLengths();
		#endif
	}
	#endregion

	#region Rail Methods
	internal Transform GetPosition(float alpha, Transform trsf)
	{
		alpha = Mathf.Clamp01 (alpha);
		if(alpha == 1f)
		{
			trsf.position = points[points.Count-1].transform.position;
			trsf.rotation = points[points.Count-1].transform.rotation;
			return trsf;
		}
		
		//Check if a line exist
		if(points.Count < 2)
		{
			trsf.position = points[0].transform.position;
			trsf.rotation = points[0].transform.rotation;
			return trsf;
		}
		
		float curDist = distance * alpha;
		int index = GetCurrentRailPoint(curDist);
		BezierPoint curPoint = points[index];
		BezierPoint nextPoint = points[index+1];
		
		curDist = curDist - curPoint.totalDistance;
		float localAlpha = curDist / nextPoint.localDistance;

		trsf.position = curPoint.gameObject.GetComponent<Bezier>().GetPointAtTime(localAlpha);
//		trsf.position = Vector3.Lerp (curPoint.transform.position, nextPoint.transform.position, localAlpha);
//		trsf.rotation = Quaternion.Lerp (curPoint.transform.rotation, nextPoint.transform.rotation, localAlpha);
		return trsf;
		//		return Vector3.Lerp (curPoint.transform.position, nextPoint.transform.position, localAlpha);
	}
	
	int GetCurrentRailPoint(float a_dist, int index = -1, int fieldOfSearch = -1)
	{
		//Init
		if(index == -1)
			index = points.Count/2;
		if(fieldOfSearch == -1)
			fieldOfSearch = index;
		
		//Si on a trouvé le bon RailPoint
		if (points [index].totalDistance <= a_dist && points [index + 1].totalDistance >= a_dist)
		{
			return index;
		}
		else
		{
			fieldOfSearch /= 2;
			if(fieldOfSearch == 0)
				fieldOfSearch = 1;
			//Si notre RailPoint est plus loin dans la list
			if(points [index].totalDistance < a_dist)
			{
				return GetCurrentRailPoint(a_dist, index+fieldOfSearch, fieldOfSearch);
			}
			//Si notre RailPoint est moins loin dans la list
			else
			{
				return GetCurrentRailPoint(a_dist, index-fieldOfSearch, fieldOfSearch);
			}
		}
	}

	internal void UpdateLengths()
	{
		if (points.Count < 2)
			return;
		for(int i=1; i<points.Count; i++)
		{
			points[i].localDistance = curves[i-1].ComputeLenght();
			points[i].totalDistance = points[i-1].totalDistance + points[i].localDistance;
		}
	}
	#endregion

	#region Editor Methods
	#if UNITY_EDITOR
	internal void InitRail()
	{
		//On vérifie si il y a des points
		if (points == null || points.Count > 0)
			return;
		//Si il n'y a aucun point, on en crée
		points.Clear ();
		curves.Clear ();
		GameObject newPoint = (GameObject)Instantiate (pointPrefab, transform.position, transform.rotation);
		newPoint.transform.SetParent (transform);
		points.Add (newPoint.GetComponent<BezierPoint>());
		points[0].localDistance = 0f;
		points[0].totalDistance = 0f;
	}
	
	internal void CreatePointEditor(Vector2 pos2D)
	{
		//Compute Coord
		Vector3 pos;
//		Quaternion rot;
		//Vector3.Distance (SceneView.lastActiveSceneView.camera.transform.position, points [points.Count - 1].position
		Plane plane = new Plane(-SceneView.lastActiveSceneView.camera.transform.forward,
		                        points [points.Count - 1].transform.position);
		float posZ = 0f;
		Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(new Vector2(pos2D.x,
		                                                                            SceneView.lastActiveSceneView.camera.pixelHeight - pos2D.y));
		plane.Raycast(ray, out posZ);
		pos = ray.GetPoint(posZ);
//		rot = Quaternion.LookRotation (pos-points[points.Count-1].transform.position);
//		points[points.Count-1].transform.rotation = rot;
		
		
		//Instanciate new Point
		GameObject newPoint = (GameObject)Instantiate (pointPrefab, pos, Quaternion.identity);
		newPoint.transform.SetParent (transform);
		points.Add (newPoint.GetComponent<BezierPoint>());
		BezierPoint p0 = points [points.Count - 2];
		BezierPoint p1 = points [points.Count - 1];
		Bezier newBezier = p0.gameObject.AddComponent<Bezier>();
		newBezier.SetBezierParam(p0.transform, p0.GetInverseHandle(), p1.GetHandle(), p1.transform);
		curves.Add (newBezier);
		
		//Calcul distance
//		float dist = Vector3.Distance(points[points.Count-2].transform.position, points[points.Count-1].transform.position);
		float dist = newBezier.ComputeLenght ();
		points[points.Count-1].localDistance = dist;
		points[points.Count-1].totalDistance = points[points.Count-2].totalDistance + dist;
	}

	internal void DeletePointEditor()
	{
		foreach(UnityEngine.Object objInit in Selection.objects)
		{
			if(objInit == null)
				continue;
			GameObject obj = (GameObject)objInit;
			BezierPoint railPoint = obj.GetComponent<BezierPoint>();
			//Si ce n'est pas un RailPoint, on passe au suivant
			if(railPoint == null)
				continue;
			//Sinon on l'efface de la liste
			int index = points.IndexOf(railPoint);
			//Si c'est le dernier point de la courbe, on le supprime simplement le point et le dernier bezier
			if(index == points.Count-1)
			{
				DestroyImmediate(curves[index-1]);
				curves.RemoveAt(index-1);
				points.RemoveAt(index);
				DestroyImmediate(obj);
				continue;
			}
			//Sinon on rebind la courbe de bezier
			curves[index-1].p3 = points[index+1].transform;
			curves[index-1].p2 = points[index+1].handle.transform;
			//On supprime le Bezier Inutile
			curves.RemoveAt(index);
			//Et enfin on supprime le point
			points.RemoveAt(index);
			DestroyImmediate(obj);
		}
//		UpdateLengths ();
	}
	#endif
	#endregion
}

//CustomEditor
#if UNITY_EDITOR
[CustomEditor(typeof(BezierRail))]
public class BezierRailInspector : Editor
{
	#region Enum
	internal enum EBezierRailInspectorState
	{
		Idle,
		CreatePoint,
		RemovePoint
	}
	#endregion
	
	#region Properties
	EBezierRailInspectorState state = EBezierRailInspectorState.Idle;
	BezierRail myScript;
	#endregion
	
	#region Methods
	public override void OnInspectorGUI ()
	{
		//Init
		myScript = (BezierRail)target;
		
		//Draw Default
		DrawDefaultInspector();
		
		
		//Button Create
		if(GUILayout.Button("Create Points"))
		{
			state = EBezierRailInspectorState.CreatePoint;
		}
		
		//Button Reset
		if(GUILayout.Button("Reset"))
		{
			for(int i=0; i<myScript.points.Count; i++)
			{
				DestroyImmediate(myScript.points[i].gameObject);
			}
			myScript.points.Clear();
			myScript.InitRail();
		}
		
		if (state != EBezierRailInspectorState.Idle)
		{
			if(GUILayout.Button("Quit Rail Editor"))
			{
				state = EBezierRailInspectorState.Idle;
			}
		}
		else
		{
			if(GUILayout.Button("Delete Selected Point(s)"))
			{
				myScript.DeletePointEditor();
			}
		}
	}
	
	public void OnSceneGUI()
	{
		//Init
		int controlID = GUIUtility.GetControlID (FocusType.Passive);
		myScript = (BezierRail)target;
		Event e = Event.current;
		
		if (e.type == EventType.MouseDown && e.button == 0)
		{
			if(state == EBezierRailInspectorState.CreatePoint)
			{
				myScript.CreatePointEditor(Event.current.mousePosition);
				GUIUtility.hotControl = controlID;
			}
		}
	}
	#endregion
}
#endif