using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Animator _animator;
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    private void Awake()
    {
        if (!_characterController) _characterController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        _animator.SetFloat(Velocity, _characterController.velocity.magnitude);
    }
}
