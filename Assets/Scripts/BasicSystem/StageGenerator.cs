using UnityEngine;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour
{
	// Buildings
	private const int numBuilding = 10;
	public enum BuildingType
	{
		A, B, C,
		RevivalSpot,
		SealedSpot
	}

	[SerializeField] private GameObject buildingBase;

	[System.Serializable]
	private class BuildingComponents
	{
		public BuildingType type;
		public GameObject[] roofs, walls, insideObjects, floors;
	}

	[SerializeField] private BuildingComponents[] buildings;

	// Items
	private int sealedCharmCount = 4;   //封印のお札の数
	private int revivalCharmCount = 2;  //復活のお札の数

	private void Awake()
	{
		// buildingCount棟の建物のうち、ランダムにアイテムを分布させる
		int[] itemPos = new int[numBuilding]; //0：からっぽ 1:封印のお札 2:復活のお札

		for (int i = 0; i < sealedCharmCount + revivalCharmCount; i++)
			if (i < sealedCharmCount) itemPos[i] = 1;
			else if (i < sealedCharmCount + revivalCharmCount) itemPos[i] = 2;

		for (int i = 0; i < itemPos.Length; i++)
		{
			int temp = itemPos[i];                              //現在の要素を預けておく
			int randomIndex = Random.Range(0, itemPos.Length);  //入れ替える先をランダムに選ぶ
			itemPos[i] = itemPos[randomIndex];                  //現在の要素に上書き
			itemPos[randomIndex] = temp;                        //入れ替え元に預けておいた要素を与える
		}

		// buildingを生成する
		for (int i = 0; i < numBuilding; i++)
		{
			BuildingComponents buildType = buildings[Random.Range(0, buildings.Length)]; //buildingの種類を決める

			Vector3 offset = Vector3.right * 20 * (float)(i % 4) + Vector3.up * 20 * (float)(i % 5); //生成位置をずらす
			Transform building = Instantiate(buildingBase, transform.position + offset, Quaternion.identity).transform; //buildingの基礎を生成
			Vector3 bPos = building.position;

			GameObject r = Instantiate(buildType.roofs[Random.Range(0, buildType.roofs.Length)], bPos, Quaternion.identity, building);                         //屋根パーツ
			GameObject w = Instantiate(buildType.walls[Random.Range(0, buildType.walls.Length)], bPos, Quaternion.identity, building);                         //壁パーツ
			GameObject insideO = Instantiate(buildType.insideObjects[Random.Range(0, buildType.insideObjects.Length)], bPos, Quaternion.identity, building);    //中身パーツ
			GameObject f = Instantiate(buildType.floors[Random.Range(0, buildType.floors.Length)], bPos, Quaternion.identity, building);                       //床パーツ

			if (itemPos[i] != 0) // アイテム担当の建物にPointを設定する
			{
				int numChild = Random.Range(0, insideO.transform.childCount);
				GameObject itemPoint = insideO.transform.GetChild(numChild).gameObject;

				if (itemPos[i] == 1) itemPoint.GetComponent<ItemPoint>().isSealedCharmContain = true;
				if (itemPos[i] == 2) itemPoint.GetComponent<ItemPoint>().isRevivalCharmContain = true;
			}

			building.gameObject.GetComponent<BuildingScript>().SetComponents(r, w, insideO, f); //　生成したパーツを親に関連づける
		}
	}

	void Start()
	{

	}
}
