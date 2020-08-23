using UnityEngine;

public class StageGenerator : MonoBehaviour
{
	public enum BuildingType
	{
		A
	}

	[SerializeField] GameObject parent;

	[SerializeField]
	private GameObject[]
		roof,
		wall,
		insideObjects,
		floor;

	private void Awake()
	{
		for (int i = 0; i < 1; i++)
		{
			Transform temp = Instantiate(parent, transform.position + Vector3.right * 10 * i, Quaternion.identity).transform;

			Vector3 tPos = temp.position;
			GameObject r = Instantiate(roof[Random.Range(0, roof.Length)], tPos, Quaternion.identity, temp);
			GameObject w = Instantiate(wall[Random.Range(0, wall.Length)], tPos, Quaternion.identity, temp);
			GameObject insideO = Instantiate(insideObjects[Random.Range(0, insideObjects.Length)], tPos, Quaternion.identity, temp);
			GameObject f = Instantiate(floor[Random.Range(0, floor.Length)], tPos, Quaternion.identity, temp);

			temp.gameObject.GetComponent<BuildingScript>().SetComponents(r, w, insideO, f);
		}
	}

	void Start()
	{

	}
}
