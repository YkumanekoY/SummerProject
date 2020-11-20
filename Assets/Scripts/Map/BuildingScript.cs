using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BuildingScript : MonoBehaviourPunCallbacks
{
	StageManager.BuildingType buildingType;
	private PhotonView bld_photonView = null;

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

    private void Awake()
    {
		bld_photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy") || other.CompareTag("Child")) //自分だったら屋根を見えなくする
		{
			bld_photonView.RPC("Deacivate", RpcTarget.MasterClient);
			//roof.SetActive(false);
			//insideObject.SetActive(true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("out");
		if (other.CompareTag("Enemy") || other.CompareTag("Child")) //自分だったら屋根を戻す
		{
			bld_photonView.RPC("Acivate", RpcTarget.MasterClient);
			//roof.SetActive(true);
			//insideObject.SetActive(false);
		}
	}

	[PunRPC]
	private void Deactivate()
    {
		roof.SetActive(false);
		insideObject.SetActive(true);
	}

	[PunRPC]
	private void Activate()
	{
		roof.SetActive(true);
		insideObject.SetActive(false);
	}
}
