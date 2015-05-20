using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	public float holdingDistance = 2.0f;
	public float maxHoldingDistance = 10.0f;
	public float minHoldingDistance = 2.0f;
	public float dampSmoothing = 0.25f;
	public float grabDistance = 10.0f;
	
	private bool _objectLocked;
	private bool _colliding = true;
	
	//private float holdingIncriment = 0.5f;
	
	private Vector3 _velocity = Vector3.zero;
	
	public enum TypeOfObject 
	{
		holdable,
		slidable,
		rollable,
		lever,
		button
	}
	
	public TypeOfObject objectType = TypeOfObject.holdable;

	void FixedUpdate () 
	{
		if(Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, grabDistance))
			{
				Debug.DrawLine(ray.origin, hit.point);
				if(hit.transform.gameObject == this.gameObject && FPSInputController.holdingObject == false)
					_objectLocked = true;
			}
			if(_objectLocked) ControlObject();
			
		}
		else ReleaseObject();
	}	
	
	void ControlObject()
	{
		FPSInputController.holdingObject = true;
		switch (objectType)
		{
		case TypeOfObject.holdable:
			PickUpObject();
			break;
		case TypeOfObject.slidable:
			//SlideObject();
			break;
		case TypeOfObject.rollable:
			//RollObject();
			break;
		case TypeOfObject.lever:
			//ActivateLever();
			break;
		case TypeOfObject.button:
			//PushButton();
			break;
		}	
	}	
	
	void PickUpObject()
	{
		rigidbody.useGravity = false;
		
		if (!_colliding) 
		transform.rigidbody.MoveRotation(Quaternion.Euler(0, 0, 0));
		
		rigidbody.MovePosition(CameraPosition()) ;
	}
	
	void ReleaseObject()
	{
		rigidbody.useGravity = true;
		FPSInputController.holdingObject = false;
		_objectLocked = false;
		holdingDistance = minHoldingDistance;
		//rigidbody.isKinematic = false;		
	}
	
	Vector3 CameraPosition()
	{
		var cameraCenter =	Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CalculatedHoldingDistance()));
		cameraCenter.x = Mathf.Round (cameraCenter.x);
		cameraCenter.y = Mathf.Round (cameraCenter.y);
		//cameraCenter.z = Mathf.Round (cameraCenter.z);
		
		return Vector3.SmoothDamp(transform.position, cameraCenter, ref _velocity, dampSmoothing);
	}
	
	float CalculatedHoldingDistance()
	{
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && rigidbody.detectCollisions) holdingDistance++;
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && rigidbody.detectCollisions) holdingDistance--;
		return Mathf.Clamp (holdingDistance, minHoldingDistance, maxHoldingDistance);
	}
	
	void OnCollisionEnter(Collision c)
	{
		_colliding = true;
	}
	
	void OnCollisionExit(Collision c)
	{
		_colliding =  false;
	}
}
