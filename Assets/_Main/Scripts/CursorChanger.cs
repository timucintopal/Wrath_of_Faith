using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] CursorTypes _cursor;

    public CursorTypes GetCursorType => _cursor;
}
