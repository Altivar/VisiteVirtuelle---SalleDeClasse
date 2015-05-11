using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class TestRail : MonoBehaviour {

	public Slider slider = null;

	public BezierRail rail = null;
	[Range(0f, 1f)]
	public float alpha = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		alpha += 0.01f * slider.value;
		if(alpha >= 1f)
			alpha = 0f;
		Transform trsf = rail.GetPosition (alpha, transform);
		transform.position = trsf.position;
		transform.rotation = trsf.rotation;
	}
}
