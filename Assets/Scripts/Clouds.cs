using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour 
{
	//Скрипт отвечающий за движующиеся блоки облаков

	public GameObject[] CloudBlocks;
	private Transform BLockPosToCreate;

	void Start () 
	{
		BLockPosToCreate = GameObject.Find("CloudBlockRespPlace").transform;
	}

	public void CreateBlock()
	{
		int Rnumber = (int) Random.Range(0.0f,CloudBlocks.Length);
		GameObject newCloudBlock = Instantiate(CloudBlocks[Rnumber],BLockPosToCreate.position,Quaternion.identity) as GameObject;
		newCloudBlock.transform.parent = this.transform;
	}
}
