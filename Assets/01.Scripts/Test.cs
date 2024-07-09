using System.Collections;
using UnityEngine;
using Crogen.ObjectPooling;

public class Test : MonoBehaviour, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnPop()
	{
		Debug.Log("Pop");
		StartCoroutine(CoroutineDie());
	}

	public void OnPush()
	{
		Debug.Log("Push");
	}

	IEnumerator CoroutineDie()
	{
		yield return new WaitForSeconds(5);
		this.Push();
	}
}

