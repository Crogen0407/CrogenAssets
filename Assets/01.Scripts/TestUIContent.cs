using UnityEngine;
using Crogen.TextEffect;
using TMPro;

public class TestUIContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    void Update()
    {
        if(Input.anyKeyDown)
		{
            _text.Write("�Ϸ翡 �ȱ������ 300�� ��õ ��...", TextType.Basic);
        }
    }
}
