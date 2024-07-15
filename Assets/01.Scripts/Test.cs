using System.Collections;
using UnityEngine;
using Crogen.CrogenPooling;

public class Test : MonoBehaviour, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnPop()
	{
		Debug.Log("OnPop");
		StartCoroutine(CoroutineDie());
	}

	public void OnPush()
	{
		Debug.Log("OnPush");
	}

	private IEnumerator CoroutineDie()
	{
		yield return new WaitForSeconds(1);
		this.Push();
	}
}
