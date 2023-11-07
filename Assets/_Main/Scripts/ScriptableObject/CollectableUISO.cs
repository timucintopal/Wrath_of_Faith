using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject
{
    [Serializable]
    public class CollectableUIClass
    {
        public Sprite Icon;
        public Vector2 IconSize;
        public string Name;
    }
    
    
    [CreateAssetMenu(fileName = "New Profile", menuName = "UI/Collectable")]
    public class CollectableUISO : UnityEngine.ScriptableObject
    {
        public List<CollectableUIClass> collectables = new List<CollectableUIClass>();
    }
}
