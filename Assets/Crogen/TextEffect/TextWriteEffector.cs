using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public static class TextWriteEffector
{
	private static List<TMP_Text> _currentWritingTextList;

	public static void Write(this TMP_Text target, string text, float delay = 0.05f)
	{
		if(_currentWritingTextList.Find(x=>x==target))
		{
			target.StartCoroutine(CoroutineWrite(target, text, delay));
			_currentWritingTextList.Remove(target);
		}
		_currentWritingTextList.Add(target);
	}

	private static IEnumerator CoroutineWrite(TMP_Text target, string text, float delay = 0.05f)
	{
		float currentdelayTime = 0;
		for (int i = 0; i < text.Length; ++i)
		{
			while(currentdelayTime < delay)
			{
				currentdelayTime += Time.deltaTime;
				yield return null;
			}
		}
	}
}
