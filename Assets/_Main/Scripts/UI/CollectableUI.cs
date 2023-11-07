using _Main.Scripts.ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.UI
{
    public class CollectableUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        private RectTransform _imgRect;
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI amountTxt;

        private CollectableUIClass _collectableUIClass;
        public CollectableUIClass CollectableUIClass
        {
            get => _collectableUIClass;
            set
            {
                _collectableUIClass = value;
                image.sprite = _collectableUIClass.Icon;
                
                _imgRect = image.GetComponent<RectTransform>();
                _imgRect.sizeDelta = _collectableUIClass.IconSize;
                
                nameTxt.text = _collectableUIClass.Name;
            }
        }
    }
}
