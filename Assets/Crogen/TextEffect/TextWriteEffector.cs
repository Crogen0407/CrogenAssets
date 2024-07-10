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

				if (text[i].Equals(' ') == false || text[i].Equals('\t') == false)
					yield return new WaitForSeconds(delay);
			}

			_currentWritingTextList.Remove(target);
		}

		private static IEnumerator CoroutineUniWrite(TMP_Text target, string text, float delay = 0.05f)
		{
			target.ForceMeshUpdate();

			var textInfo = target.textInfo;
			target.text = text;
			for (int i = 0; i < textInfo.characterCount; ++i)
			{
				var charInfo = textInfo.characterInfo[i];

				if (!charInfo.isVisible)
					continue;

				var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

				for (int j = 0; j < 4; ++j)
				{
					var origin = verts[charInfo.vertexIndex + j];
					Debug.Log(verts[charInfo.vertexIndex + j]);
					verts[charInfo.vertexIndex + j] = origin + new Vector3(100, 100, 100);
					Debug.Log(verts[charInfo.vertexIndex + j]);
				}
			}

			for (int i = 0; i < textInfo.meshInfo.Length; ++i)
			{
				var meshInfo = textInfo.meshInfo[i];
				meshInfo.mesh.vertices = meshInfo.vertices;
				target.UpdateGeometry(meshInfo.mesh, i);
				target.UpdateVertexData();
			}

			target.ForceMeshUpdate();

			yield return new WaitForSeconds(10);

			_currentWritingTextList.Remove(target);
		}
	}

}
