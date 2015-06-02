using UnityEngine;
using System.Collections;

public class ResizeQuadToText : MonoBehaviour {

	public MeshRenderer text = null;
	public float margin = 1f;
	
	// Update is called once per frame
	void Update () {
		ResizeQuad ();
	}

	Vector3 ResizeQuad()
	{
		Quaternion temp = text.transform.rotation;
		text.transform.rotation = Quaternion.identity;
		Vector3 result = new Vector3 (text.bounds.size.x+margin, text.bounds.size.y+margin, 1f);
		transform.localScale = result;
		text.transform.rotation = temp;
		return result;
	}
}
