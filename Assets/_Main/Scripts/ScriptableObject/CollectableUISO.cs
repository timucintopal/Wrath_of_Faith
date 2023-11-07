using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.ScriptableObject
{
    [SerializeField]
    public class CollectableUIClass
    {
        public Sprite Icon;
        public Vector2 IconSize;
        public string Name;
    }
    
    
    [CreateAssetMenu(fileName = "New Profile", menuName = "UI/Collectable")]
    public class CollectableUISO : UnityEngine.ScriptableObject
    {
        public List<CollectableUIClass> Collectables = new List<CollectableUIClass>();
    }
}
