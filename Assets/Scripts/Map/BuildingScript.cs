using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
	StageGenerator.BuildingType buildingType;

	[SerializeField]
	private GameObject
		roof,
		wall,
		insideObject,
		floor;

	public void SetComponents(
		GameObject r,
		GameObject w,
		GameObject insideO,
		GameObject f)
	{
		roof = r;
		wall = w;
		insideObject = insideO;
		floor = f;

		insideObject.SetActive(false);
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("atack");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("in");
		if (other.CompareTag("Player"))
		{
			roof.SetActive(false);
			insideObject.SetActive(true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("out");
		if (other.CompareTag("Player"))
		{
			roof.SetActive(true);
			insideObject.SetActive(false);
		}
	}
}
