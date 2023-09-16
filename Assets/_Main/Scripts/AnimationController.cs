using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Animator _animator;
    [SerializeField] ParticleController _particleController;
    private static readonly int VelocityX = Animator.StringToHash("VelocityX");
    private static readonly int VelocityZ = Animator.StringToHash("VelocityZ");

    float VelocityThreshold = 0.05f;

    private float divideVelo = 1;
    
    private static readonly int Fall = Animator.StringToHash("SkyFall");
    private static readonly int Jump = Animator.StringToHash("SkyJump");

    public UnityAction Standed;

    public UnityAction OnJumpStart;

    private void Awake()
    {
        if (!_characterController) _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        M_Input.DirectionInput += Move;
    }

    void JumpCall()
    {
        Debug.Log("Jump Call!");
        M_Camera.I.Shake();
        _particleController.PlayParticle(0);
    }

    void JumpStart()
    {
        OnJumpStart?.Invoke();
        M_Camera.I.Shake();
        _particleController.PlayParticle(0);
    }

    public void Move(Vector3 newVal)
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
        
        _animator.SetFloat(VelocityX, value.x);
        _animator.SetFloat(VelocityZ, value.z);
    }

    public void SkyDive()
    {
        _animator.SetTrigger(Fall);
    }
    
    public void SkyJump()
    {
        _animator.SetTrigger(Jump);
    }

    public void Stand()
    {
        Standed?.Invoke();
    }
    
   
}
