using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "ScriptableObject/Test/ScriptOBJ")]
    public class ScriptOBJ : ScriptableObject
    {
        public string NickName;
        public int Level;
        public int Exp;
    }
}