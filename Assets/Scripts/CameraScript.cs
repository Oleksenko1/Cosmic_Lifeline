using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform target_object;
	public float follow_tightness;
	Vector3 wanted_position;
	void FixedUpdate()
	{
		wanted_position = target_object.position;
		wanted_position.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * follow_tightness);
	}
}
