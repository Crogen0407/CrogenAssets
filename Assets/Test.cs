using System;
using Crogen.JsamJson;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        JsamJson.Save(new SaveData()
        {
            characterLevel = new []{4,4,4,4},
            userName = "Crogen",
            currentStoryIndex = 4,
            currentMaxStageIndex = 0
        });
        
        
        JsamJson.Save(new SoundData()
        {
            Master = 10,
            Background = 5,
            Effect = 2
        });
    }

    private void Start()
    {
        SoundData sb = JsamJson.Load<SoundData>();
        Debug.Log(sb.Master);
        Debug.Log(sb.Background);
        Debug.Log(sb.Effect);
    }
}
