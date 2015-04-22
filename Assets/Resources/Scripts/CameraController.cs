using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	
	private float rotationY = 0.0f;
	public float lookSpeed = 5.0f;

	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{


		#if UNITY_EDITOR

		rotationY += Input.GetAxis("Mouse Y")*lookSpeed;

		while(rotationY > 85.0f)
			rotationY = 85.0f;
		while(rotationY < -85.0f)
			rotationY = -85.0f;

		transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.left);

		#elif UNITY_ANDROID
		transform.Rotate(Vector3.up, 25.0f*Time.deltaTime, Space.World);

		#endif
	}
}
