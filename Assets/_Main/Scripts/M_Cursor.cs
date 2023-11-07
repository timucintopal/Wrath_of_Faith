using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Cursor;


public enum CursorTypes
{
    Normal,
    Wood,
    Rock
}

[Serializable]
public class Cursor
{
    public CursorTypes Type;
    public LayerMask Mask;
    public Sprite Icon;
}

public class M_Cursor : MonoBehaviour
{
    [SerializeField] SpriteRenderer _cursorSprite;

    Camera _camera;
    Transform _cameraTransform;

    [SerializeField] float z = -10;
    [SerializeField] List<Cursor> Cursors;

    [SerializeField] private LayerMask targetMasks;

    RaycastHit hit;

    Ray _ray;

    [SerializeField] List<Texture2D> cursorTextures = new List<Texture2D>();

    private void Awake()
    {
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
    }

    private void Update()
    {
        transform.position = GetWorldPositionOnPlane(Input.mousePosition, z);

        Ray();
    }


    Rigidbody rb;
    void Ray()
    {
        Debug.DrawRay(transform.position, _cursorSprite.transform.forward, Color.magenta,1);
        if (Physics.Raycast(transform.position, _cursorSprite.transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity, targetMasks))
        {
            rb = hit.collider.attachedRigidbody;
            if(rb)
                if(rb.TryGetComponent(out CursorChanger script))
                    RefreshCursor((int)script.GetCursorType);
        }
        else
            RefreshCursor();
    }

    void RefreshCursor(int index)
    {
        SetCursor(cursorTextures[index], Vector2.zero, CursorMode.Auto);
        // _cursorSprite.sprite = Cursors[index].Icon;
    }

    void RefreshCursor()
    {
        Debug.Log("Refresh Normal");
        SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
        // _cursorSprite.sprite = Cursors[0].Icon;
    }
    
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        // Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        Plane xy = new Plane(_cameraTransform.forward , new Vector3(0, 0, z));
        // transform.LookAt(_cameraTransform);
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
