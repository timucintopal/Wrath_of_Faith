using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    public int speed = 5;
    public int rotationSpeed = 5;

    Vector3 movementDirection;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        M_Input.DirectionInput += Move;
        M_Input.MouseGroundInput += LookAt;

    }

    public void Move(Vector3 moveDir)
    {
        Debug.Log("Dir " + moveDir);

        movementDirection = (moveDir.z * transform.forward) + (moveDir.x * transform.right);
        
        _characterController.Move(movementDirection * Time.deltaTime * speed);
    }

    void LookAt(Vector3 lookPos)
    {
        // Debug.Log("LOOK POS " + lookPos);
        // transform.LookAt(new Vector3(lookPos.x, transform.position.y, lookPos.z));
        // transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        
        Vector3 targetDirection = lookPos - transform.position;

        targetDirection -= Vector3.up * targetDirection.y;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
