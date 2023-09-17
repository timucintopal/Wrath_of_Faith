using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool isMouseOver = false;

    public bool IsMouseOverUIElement => isMouseOver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
