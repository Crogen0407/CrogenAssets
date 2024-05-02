using UnityEngine;

namespace Crogen.AgentFSM.Editor
{
    public class AgentScriptData : ScriptableObject
    {
        //Path
        public string scriptPath;
        
        //Main Script
        public string mainScriptName;
        
        //Enum Script
        public string enumScriptName;
        public string[] enumElements;
    }
}