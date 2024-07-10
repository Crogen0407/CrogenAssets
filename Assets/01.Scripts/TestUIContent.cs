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
            _text.Write("하루에 팔굽혀펴기 300개 실천 중...", TextType.Basic);
        }
    }
}
