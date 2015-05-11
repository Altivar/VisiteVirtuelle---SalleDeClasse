#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BezierPoint : MonoBehaviour {

	public Transform handle = null;
	[HideInInspector]
	public Transform inverseHandle = null;
//	[HideInInspector]
	public 	float 		totalDistance 	= 0f,
						localDistance 	= 0f;

	void Awake()
	{
		#if UNITY_EDITOR
		if (handle == null)
			CreateHandle ();
		#endif
	}

	void Update()
	{
		inverseHandle.localPosition = - handle.localPosition;
	}

	public Transform GetHandle()
	{
		return handle;
	}

	public Transform GetInverseHandle()
	{
		return inverseHandle;
	}

	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		//Draw Handler's Lines
		Handles.color = new Color(1f, 1f, 1f, 0.5f);
		Handles.DrawLine(GetHandle().position, GetInverseHandle().position);

		//Draw Points
		Handles.DotCap(0, GetHandle().position, Quaternion.identity, 0.5f);
//		Handles.DotCap(0, GetInverseHandle(), Quaternion.identity, 4f);
	}
	#endif

	#if UNITY_EDITOR
	void CreateHandle()
	{
		handle = new GameObject ().transform;
		handle.position = transform.position + transform.forward;
		handle.rotation = Quaternion.identity;
		handle.SetParent (transform);
		MeshRenderer handleRenderer = handle.gameObject.AddComponent<MeshRenderer> ();
		handleRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		handleRenderer.receiveShadows = false;
		handleRenderer.materials = new Material[0];
		handleRenderer.useLightProbes = false;
		handleRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;


		inverseHandle = new GameObject().transform;
		inverseHandle.position = transform.position - handle.position;
		inverseHandle.rotation = Quaternion.identity;
		inverseHandle.SetParent (transform);
		MeshRenderer inverseHandleRenderer = inverseHandle.gameObject.AddComponent<MeshRenderer> ();
		inverseHandleRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		inverseHandleRenderer.receiveShadows = false;
		inverseHandleRenderer.materials = new Material[0];
		inverseHandleRenderer.useLightProbes = false;
		inverseHandleRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
	}
	#endif
}
