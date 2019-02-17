using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public float dampTime = 0.15f;
	Vector3 velocity = Vector3.zero;
	Camera thisCamera;
	public Transform target;


	void Start()
	{
		thisCamera = GetComponent<Camera>();
		OnReset();
		
	}

	public void OnReset()
	{
		if (target)
		{
			transform.position = new Vector3(target.position.x, target.position.y,-10);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (target)
		{
			Vector3 delta = target.position - transform.position;
			delta = new Vector3(delta.x, delta.y, 0);
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}