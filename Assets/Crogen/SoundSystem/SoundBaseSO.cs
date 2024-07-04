using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundPair
{
    public string audioType;
    public AudioClip audioClip;
}

public class SoundBaseSO : ScriptableObject
{
    public List<SoundPair> pairs;
    
    public void PairInit()
    {
        if (pairs == null) return;
        
        foreach (var pair in pairs)
        {
            if (pair.audioType.Equals(string.Empty) && pair.audioClip != null)
            {
                pair.audioType = pair.audioClip.name;
                break;
            }
        }
    }
}
