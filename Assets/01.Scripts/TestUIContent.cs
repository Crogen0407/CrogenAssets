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
            _text.Write("하루에팔굽혀펴기300개실천중...", TextType.Uni);
        }
    }
}
