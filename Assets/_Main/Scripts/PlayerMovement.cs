using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum PlayerStatus
{
    Idle,
    Moving,
    SwitchingMode,
    God
}

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] AnimationController _animationController;
    
    CharacterController _characterController;
    

    [SerializeField] ProphetModes currentMode = ProphetModes.Human;

    PlayerStatus _playerStatus;
    public int speed = 5;
    public int rotationSpeed = 5;
    Vector3 movementDirection;

    public PlayerStatus PlayerStatus
    {
        get => _playerStatus;
        set
        {
            _playerStatus = value;
            
        }
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        M_Input.DirectionInput += Move;
        M_Input.MouseGroundInput += LookAt;

        M_ModeBar.ModeChanged += SwitchMode;
    }

    public void Move(Vector3 moveDir)
    {
        if(PlayerStatus != PlayerStatus.SwitchingMode)
        {
            movementDirection = (moveDir.z * transform.forward) + (moveDir.x * transform.right);

            _characterController.Move(movementDirection * Time.deltaTime * speed);
        }
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

    void SwitchMode(ProphetModes newMode)
    {
        currentMode = newMode;
        switch (currentMode)
        {
            case ProphetModes.Human:
                StartCoroutine(ActivateProphet());
                
                break;
            case ProphetModes.God:
                StartCoroutine(ActivateGodMode());
                break;
        }
    }

    IEnumerator ActivateProphet()
    {
        Debug.Log("ACTIVE PROPHET");
        bool isStanded = false;
        _animationController.Standed += ()=> isStanded = true;
        
        transform.DOMoveY(20, 0);
        
        transform.DOMoveY(1, .5f).SetEase(Ease.Linear);
        
        // yield return new WaitUntil()
        
        yield return new WaitForSeconds(.05f);
        _animationController.SkyDive();
        
        // Debug.Log("ACTIVE " );

        yield return new WaitUntil(()=> isStanded);

        PlayerStatus = PlayerStatus.Idle;
    }

    
    IEnumerator ActivateGodMode()
    {
        bool jumpStart = false;
        
        _animationController.OnJumpStart += ()=> jumpStart = true;
        
        PlayerStatus = PlayerStatus.SwitchingMode;

        yield return new WaitUntil(() => _characterController.velocity == Vector3.zero);
        
        bool isGround = true;
        
        _animationController.SkyJump();

        yield return new WaitUntil(() => jumpStart);
        

        transform.DOMoveY(20, .5f).SetDelay(.1f).OnComplete(()=> PlayerStatus = PlayerStatus.God);
        // yield return new WaitForSeconds(.75f);

        PlayerStatus = PlayerStatus.God;
    }
}
