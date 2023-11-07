using System.Collections.Generic;
using _Main.Scripts.ScriptableObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class CollectableUIController : MonoBehaviour
    {
        private CollectableUISO _collectableData;
        private GameObject _collectableUiObject;

        private readonly List<CollectableUI> _collectableUis = new List<CollectableUI>();


        private void Awake()
        {
            Init();
        }

        [Button]
        void Init()
        {
            if (!_collectableData) _collectableData = Resources.Load<CollectableUISO>("SO/CollectableUIData");
            if (!_collectableUiObject) _collectableUiObject = Resources.Load<GameObject>("Prefabs/UI/CollectableUI");

            foreach (var collectableUi in _collectableUis)
            {
                DestroyImmediate(collectableUi.gameObject);
            }
            _collectableUis.Clear();

            foreach (var collectable in _collectableData.collectables)
            {
                var script = Instantiate(_collectableUiObject, transform).GetComponent<CollectableUI>();
                _collectableUis.Add(script);
                script.CollectableUIClass = collectable;
            }
        }
    }
}
