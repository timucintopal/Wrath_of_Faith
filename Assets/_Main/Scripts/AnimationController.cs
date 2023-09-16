using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Animator _animator;
    private static readonly int VelocityX = Animator.StringToHash("VelocityX");
    private static readonly int VelocityZ = Animator.StringToHash("VelocityZ");

    [SerializeField] float VelocityThreshold = 0.05f;


    private float divideVelo = 1;
    
    

    private void Awake()
    {
        if (!_characterController) _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        M_Input.DirectionInput += MoveX;
    }

    private void Update()
    {
        // MoveX();
    }

    public void MoveX(Vector3 newVal)
    {
        // var value = _characterController.velocity;
        var value = newVal;

        value /= divideVelo;
        
        if(value != Vector3.zero)
            Debug.Log("Velocity " + value);

        if (Mathf.Abs(value.x) < VelocityThreshold)
            value -= value.x * Vector3.right;
        
        if (Mathf.Abs(value.z) < VelocityThreshold)
            value -= value.z * Vector3.forward;

        
        // _animator.SetFloat(VelocityX, Mathf.Clamp(value.x, VelocityThreshold, 1));
        // _animator.SetFloat(VelocityZ, Mathf.Clamp(value.z, VelocityThreshold, 1));
        
        _animator.SetFloat(VelocityX, value.x);
        _animator.SetFloat(VelocityZ, value.z);
    }
    
    public void MoveZ(float value)
    {
        
    }
}
