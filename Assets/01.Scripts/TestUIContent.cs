using UnityEngine;
using Crogen.TextEffect;
using TMPro;

public class TestUIContent : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

	private void Start()
	{
		_text.Write("���ع��� ��λ��� ������ �⵵�� �ϴ����� �����ϻ� �츮���� ����", TextType.Basic, 0.05f, ()=>_text.SetColor(0, 4, Color.red));
		
		_text.SetMathState(Wobble, 0, 4);
	}

	Vector3 Wobble(float time)
    {
        return new Vector2(Mathf.Cos(time * 500f) - Mathf.Sin(time * 100f) + Mathf.Cos(time * 200.5f) - Mathf.Sin(time * 200.5f), Mathf.Cos(time * 22.5f) - Mathf.Sin(time * -234.5f));
    }
}
