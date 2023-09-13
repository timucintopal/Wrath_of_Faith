using System;
using UnityEngine;
using UnityEngine.Events;

public class M_Input : MonoBehaviour
{
    ProphetModes _currentMode;

    public bool isActive = true;

    Vector3Int _movementDirection;

    public static UnityAction<Vector3> DirectionInput;
    public static UnityAction<Vector3> MouseGroundInput;

    RaycastHit _hit;
    Vector3 mouseGroundPos;

    private void OnEnable()
    {
        M_ModeBar.ModeChanged += ChangeMode;
    }

    void ChangeMode(ProphetModes newMode)
    {
        _currentMode = newMode;
    }

    private void Update()
    {
        if(!isActive) return;

        switch (_currentMode)
        {
            case ProphetModes.Human:
                _movementDirection = Vector3Int.zero;                
                if (Input.GetKey(KeyCode.W))
                    _movementDirection += Vector3Int.forward;
                if (Input.GetKey(KeyCode.A))
                    _movementDirection += Vector3Int.left;
                if (Input.GetKey(KeyCode.S))
                    _movementDirection += Vector3Int.back;
                if (Input.GetKey(KeyCode.D))
                    _movementDirection += Vector3Int.right;
                
                DirectionInput?.Invoke(_movementDirection);
                
                if(Input.GetMouseButton(0))
                    if(Ray())
                        MouseGroundInput?.Invoke(mouseGroundPos);
                
                
                
                break;
            case ProphetModes.God:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    bool Ray()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out _hit, 100, LayerMask.GetMask("Ground")))
        {
            mouseGroundPos = _hit.point;
            return true;
        }

        return false;
    }
}
