using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
	StageManager.BuildingType buildingType;

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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy") || other.CompareTag("Child")) //自分だったら屋根を見えなくする
		{
			roof.SetActive(false);
			insideObject.SetActive(true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("out");
		if (other.CompareTag("Enemy") || other.CompareTag("Child")) //自分だったら屋根を戻す
		{
			roof.SetActive(true);
			insideObject.SetActive(false);
		}
	}
}
