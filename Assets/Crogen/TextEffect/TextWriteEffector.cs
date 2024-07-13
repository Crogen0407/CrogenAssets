using System;
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
		Popping
	}

	public static class TextWriteEffector
	{
		private static List<TMP_Text> _currentWritingTextList;

		public static void Write(this TMP_Text target, string text, TextType textType, float delay = 0.05f, Action endEvent = null)
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
					target.StartCoroutine(CoroutineBasicWrite(target, text, delay, endEvent));
					break;
				case TextType.Popping:
					target.StartCoroutine(CoroutinePoppingWrite(target, text, delay, endEvent));
					break;
			}
		}

		public static void SetColor(this TMP_Text target, int min, int max, Color color)
		{
			for (int i = 0; i < target.textInfo.characterCount; ++i)
			{
				if (min > i || i < max)
					target.textInfo.characterInfo[i].color = color;

				target.ForceMeshUpdate();
			}
		}

		private static IEnumerator CoroutineBasicWrite(TMP_Text target, string text, float delay = 0.05f, Action endEvent = null)
		{
			StringBuilder stringBuilder = new StringBuilder();

			for (int i = 0; i < text.Length; ++i)
			{
				stringBuilder.Append(text[i]);
				target.text = stringBuilder.ToString();

				if (text[i].Equals(' ') == false || text[i].Equals('\t') == false)
					yield return new WaitForSeconds(delay);
			}
			endEvent?.Invoke();
			_currentWritingTextList.Remove(target);
		}

		private static IEnumerator CoroutinePoppingWrite(TMP_Text target, string text, float delay = 0.05f, Action endEvent = null)
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
				endEvent?.Invoke();
				textInfo.meshInfo[charInfo.materialReferenceIndex].vertices = verts;
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

		#region 

		public static void SetMathState(this TMP_Text target, Func<float, Vector3> func)
		{
			target.StartCoroutine(CoroutineMathWrite(target, func));
		}

		public static void SetMathState(this TMP_Text target, Func<float, Vector3> func, int min, int max)
		{
			target.StartCoroutine(CoroutineMathWrite(target, func, min, max));
		}

		public static void RemoveMathState(this TMP_Text target)
		{
			target.StopAllCoroutines();
		}

		private static IEnumerator CoroutineMathWrite(TMP_Text target, Func<float, Vector3> func, int min, int max)
		{
			while (true)
			{
				target.ForceMeshUpdate();
				var mesh = target.mesh;
				var vertices = mesh.vertices;

				for (int i = 0; i < target.textInfo.characterCount; i++)
				{
					if (min > i || i < max)
					{
						TMP_CharacterInfo c = target.textInfo.characterInfo[i];

						int index = c.vertexIndex;

						Vector3 offset = func.Invoke(Time.time + i);
						vertices[index] += offset;
						vertices[index + 1] += offset;
						vertices[index + 2] += offset;
						vertices[index + 3] += offset;
					}
				}

				mesh.vertices = vertices;
				target?.canvasRenderer.SetMesh(mesh);

				yield return null;
			}
		}

		private static IEnumerator CoroutineMathWrite(TMP_Text target, Func<float, Vector3> func)
		{
			while(true)
			{

				target.ForceMeshUpdate();
				var mesh = target.mesh;
				var vertices = mesh.vertices;

				for (int i = 0; i < target.textInfo.characterCount; i++)
				{
					TMP_CharacterInfo c = target.textInfo.characterInfo[i];

					int index = c.vertexIndex;

					Vector3 offset = func.Invoke(Time.time + i);
					vertices[index] += offset;
					vertices[index + 1] += offset;
					vertices[index + 2] += offset;
					vertices[index + 3] += offset;
				}

				mesh.vertices = vertices;
				target?.canvasRenderer.SetMesh(mesh);
				
				yield return null;
			}
		}

		#endregion
	}

}
