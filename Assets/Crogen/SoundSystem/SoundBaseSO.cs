using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundPair
{
    public string audioName;
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
            if (pair.audioName.Equals(string.Empty) && pair.audioClip != null)
            {
                pair.audioName = pair.audioClip.name;
                break;
            }
        }
    }
}
