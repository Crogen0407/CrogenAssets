using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Crogen.TextEffect
{
	public enum TextType
	{
		Basic,
		Uni
	}

	public static class TextWriteEffector
	{
		private static List<TMP_Text> _currentWritingTextList;

		public static void Write(this TMP_Text target, string text, TextType textType, float delay = 0.05f)
		{
			_currentWritingTextList ??= new List<TMP_Text>();
			if (_currentWritingTextList.Find(x => x == target))
			{
				target.StopAllCoroutines();
			}
			else
			{
				_currentWritingTextList.Add(target);
			}
			target.text = string.Empty;
			switch (textType)
			{
				case TextType.Basic:
					target.StartCoroutine(CoroutineBasicWrite(target, text, delay));
					break;
				case TextType.Uni:
					target.StartCoroutine(CoroutineUniWrite(target, text, delay));
					break;
			}
		}

		private static IEnumerator CoroutineBasicWrite(TMP_Text target, string text, float delay = 0.05f)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; ++i)
			{
				stringBuilder.Append(text[i]);
				target.text = stringBuilder.ToString();
				yield return new WaitForSeconds(delay);
			}
			_currentWritingTextList.Remove(target);
		}
		private static IEnumerator CoroutineUniWrite(TMP_Text target, string text, float delay = 0.05f)
		{
			yield return new WaitForSeconds(delay);
		}
	}

}
