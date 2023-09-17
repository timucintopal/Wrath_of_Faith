using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class M_Input : MonoBehaviour
{
    ProphetModes _currentMode;

    public bool isActive = true;

    [SerializeField]Vector3Int movementDirection;

    public static UnityAction<Vector3> DirectionInput;
    public static UnityAction<Vector3> MouseGroundInput;

    RaycastHit _hit;
    Vector3 _mouseGroundPos;

    [SerializeReference] Vector3 lastDir = Vector3.zero;
    [SerializeField] float inputThreshold = .1f;

    public float speed = 5; 

    private void OnEnable()
    {
        M_ModeBar.ModeChanged += ChangeMode;
    }

    void ChangeMode(ProphetModes newMode)
    {
        _currentMode = newMode;
    }

    Vector3 refValue = Vector3.zero;
    
    private void Update()
    {
        if(!isActive) return;

        switch (_currentMode)
        {
            case ProphetModes.Human:

                #region Movement
                
                movementDirection = Vector3Int.zero;                
                if (Input.GetKey(KeyCode.W))
                    movementDirection += Vector3Int.forward;
                if (Input.GetKey(KeyCode.A))
                    movementDirection += Vector3Int.left;
                if (Input.GetKey(KeyCode.S))
                    movementDirection += Vector3Int.back;
                if (Input.GetKey(KeyCode.D))
                    movementDirection += Vector3Int.right;

                lastDir = Vector3.Lerp(lastDir, movementDirection, speed * Time.deltaTime);
                DirectionInput?.Invoke(lastDir);
                #endregion

                #region Rotation

                if(Input.GetMouseButton(0) && !IsMouseOverUI())
                    if(Ray())
                        MouseGroundInput?.Invoke(_mouseGroundPos);

                if (Mathf.Abs(lastDir.x) < inputThreshold)
                    lastDir -= Vector3.right *  lastDir.x;
                if (Mathf.Abs(lastDir.z) < inputThreshold)
                    lastDir -= Vector3.forward *  lastDir.z;

                #endregion
                
                break;
            case ProphetModes.God:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    bool Ray()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out _hit, 100, LayerMask.GetMask("Ground")))
        {
            _mouseGroundPos = _hit.point;
            return true;
        }

        return false;
    }
}
