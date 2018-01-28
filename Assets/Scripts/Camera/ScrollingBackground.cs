using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	/**
	* Attributes
	*/
	[SerializeField] private float parallaxSpeedH;
	[SerializeField] private float parallaxSpeedV;

	private Transform cameraTransform;
	private float LastCameraX;
	private float LastCameraY;

	private Transform[] layers;
	private SpriteRenderer[] layersSpriteRenderer;

	private float backgroundSize;
	private float viewZone = 10;
	private int leftIndex, rightIndex;

	[SerializeField] private bool canSoloMove = false;

	/**
	* Monobehavior methods
	*/
	protected void Start () {
		cameraTransform = Camera.main.transform;

		LastCameraX = cameraTransform.position.x;
		LastCameraY = cameraTransform.position.y;

		layers = new Transform[transform.childCount];
		layersSpriteRenderer = new SpriteRenderer[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			layers[i] = transform.GetChild(i);
			layersSpriteRenderer[i] = layers[i].GetComponent<SpriteRenderer>();
		}

		if (layersSpriteRenderer[0])
			backgroundSize = layersSpriteRenderer[0].bounds.size.x;
		else
			backgroundSize = transform.parent.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	protected void Update() {
		float deltaX = 0;
		deltaX = cameraTransform.position.x - LastCameraX;
		LastCameraX = cameraTransform.position.x;

		float deltaY = 0;
		deltaY = cameraTransform.position.y - LastCameraY;
		LastCameraY = cameraTransform.position.y;

		Vector2 newPos = transform.position;

		if (canSoloMove)
		{
			newPos = new Vector2(deltaX, transform.position.y) +  (Vector2.right * Time.time * parallaxSpeedH);
		}
		else
		{
			newPos += Vector2.right * (deltaX * parallaxSpeedH);
		}


		newPos += Vector2.up * deltaY * parallaxSpeedV;

		Debug.Log(newPos);

		transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

		if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
			ScrollLeft();
		if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
			ScrollRight();

		LastCameraY = cameraTransform.position.y;
	}

	/**
	* Personal methods
	*/
	private void ScrollLeft() {
		int lastRight = rightIndex;

		layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize) + Vector3.up * layers[leftIndex].position.y + Vector3.forward * transform.position.z;

		if (layersSpriteRenderer[0])
			layersSpriteRenderer[rightIndex].flipX = !layersSpriteRenderer[rightIndex].flipX;

		leftIndex = rightIndex;
		rightIndex--;

		if (rightIndex < 0)
			rightIndex = layers.Length - 1;
	}

	void ScrollRight() {
		int lastLeft = leftIndex;

		layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize) + Vector3.up* layers[rightIndex].position.y + Vector3.forward * transform.position.z;

		if(layersSpriteRenderer[0])
			layersSpriteRenderer[leftIndex].flipX = !layersSpriteRenderer[leftIndex].flipX;

		rightIndex = leftIndex;
		leftIndex++;

		if (leftIndex == layers.Length)
			leftIndex = 0;
	}
}
