using UnityEngine;
using System.Collections;

public class ControllerRotate : MonoBehaviour {
	public Transform sphere;
	private Material mat; 
	private float lastDist;
	private Vector3 startScale;
	private float startDist;
	// Use this for initialization
	void Start () {
		mat = sphere.GetComponent<MeshRenderer>().material;
		startScale = sphere.localScale;
		startDist = Vector3.Distance(Camera.main.transform.position, sphere.position);
		lastDist = startDist;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = GvrController.Orientation;
		if (GvrController.IsTouching) {
			RaycastHit hitInfo;
			Vector3 rayDirection = GvrController.Orientation * Vector3.forward;
			if (Physics.Raycast(transform.position, rayDirection, out hitInfo)) {
				if (hitInfo.collider && hitInfo.collider.gameObject) {
					sphere.position = hitInfo.point;
					var dist = Vector3.Distance(Camera.main.transform.position, sphere.position);
					var newColor = Color.LerpUnclamped(Color.blue, Color.red, dist/ 75.0f);
					mat.color = newColor;
					if (dist != lastDist) {
						sphere.localScale = startScale / startDist * dist;
						lastDist = dist;
					}
				}
			}
		}
	}
}
