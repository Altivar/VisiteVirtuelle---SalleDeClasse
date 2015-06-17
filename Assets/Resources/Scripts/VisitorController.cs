using UnityEngine;
using System.Collections;

public class VisitorController : MonoBehaviour {
	
	public float rotationX = 0.0f;
	public float lookSpeed = 5.0f;
	public float moveSpeed = 5.0f;
	public float YPosition = 1.0f;
	
	private Rigidbody rb;
	private Vector3 _continuousVelocity = new Vector3(0,0,0);

	public Transform DiveTrsf;
	
	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody>();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	#if UNITY_EDITOR

		rotationX += Input.GetAxis("Mouse X")*lookSpeed;
		
		while(rotationX > 180.0f)
			rotationX -= 360.0f;
		while(rotationX < -180.0f)
			rotationX += 360.0f;
		
		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		
		
		///[Keyboard Gesture]///
		if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0))
		{
			//this.transform.Translate( new Vector3(0,0, moveSpeed * Time.deltaTime), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(0,0, moveSpeed * 1000.0f * Time.deltaTime) );
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(1))
		{
			//this.transform.Translate( new Vector3(0,0, -moveSpeed * Time.deltaTime), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(0,0, -moveSpeed * 1000.0f * Time.deltaTime) );
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			//this.transform.Translate( new Vector3(moveSpeed * Time.deltaTime, 0, 0), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(moveSpeed * 1000.0f * Time.deltaTime, 0, 0) );
		}
		else if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
		{
			//this.transform.Translate( new Vector3(-moveSpeed * Time.deltaTime, 0, 0), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(-moveSpeed * 1000.0f * Time.deltaTime, 0, 0) );
		}
		///[Keyboard Gesture]///


		#elif UNITY_ANDROID

		if( Input.GetMouseButton(0) )
		{
			//this.transform.Translate( new Vector3(0,0, moveSpeed * Time.deltaTime), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(0,0, moveSpeed * 1000.0f * Time.deltaTime) );
		}
		else if( Input.GetMouseButton(1) )
		{
			//this.transform.Translate( new Vector3(0,0, -moveSpeed * Time.deltaTime), Space.Self);
			rb.AddForce( DiveTrsf.rotation * new Vector3(0,0, -moveSpeed * 1000.0f * Time.deltaTime) );
		}

		#endif
		
		// keep the rigidbody velocity at (0,0,0)
		rb.velocity = _continuousVelocity;
		
	}
}
