using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;


		transform.LookAt(target);
	}

	public void ChangeTarget(Transform newTarget)
    {
		target = newTarget;
    }

    private void OnDisable()
    {
		transform.position = new Vector3(7f, 10f, 8f);
    }

} 