using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITeamInterface
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private float moveSpeed = 20f;
    
    [SerializeField] private float maxMoveSpeed = 80f;
    [SerializeField] private float minMoveSpeed = 10f;
    private Camera mainCam;
    private CameraController cameraController;
    [SerializeField] private int TeamID = 1;
    [SerializeField] private MovementComponent MovementComponent;

    internal void AddMoveSpeed ( float boostAmt )
    {
        moveSpeed += boostAmt ;
        moveSpeed = Mathf.Clamp ( moveSpeed , minMoveSpeed , maxMoveSpeed ) ;
    }
  //  [SerializeField] private float TurnSpeed = 30f;

    private Animator _animator;

    private float animatorTurnSpeed;
    [SerializeField] private float animTurnSpeed = 30f;

    [Header("Inventory")] 
    [SerializeField] private InventoryComponent _inventoryComponent;

    [Header("HealthAndDamage")] 
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private PlayerValueGuage HealthBar;

    [Header("UI")] 
    [SerializeField] private UIManager uiManager;

    [Header("AbilityAndStamina")] 
    [SerializeField]
    private AbilityComponent AbilityComponent;
    [SerializeField] private PlayerValueGuage StaminaBar;

    
    private Vector2 moveInput;
    private Vector2 aimInput;
    public int GetTeamID()
    {
        return TeamID;
    }
    
    
    private void Start()
    {
        moveStick.OnStickValueUpdated += MoveStickUpdated;
        aimStick.OnStickValueUpdated += aimStickUpdated;
        aimStick.onStickTap += StartSwitchWeapon;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        _animator = GetComponent<Animator>();
        healthComponent.onHealthChange += HealthChanged;
        healthComponent.onHealthEmpty += StartDeathSequence;
        healthComponent.BroadcastHealthValueImmediately();

        AbilityComponent.onStaminaChange += StaminaChanged;
        AbilityComponent.BroadcastStaminaChangedImmediately();
    }

    private void StaminaChanged(float newamount, float maxamount)
    {
        StaminaBar.UpdateValue(newamount, 0, maxamount);
    }

    private void StartDeathSequence()
    {
        _animator.SetLayerWeight(2,1 );
        _animator.SetTrigger("Death");
        uiManager.SetGameplayControlEnabled(false);
    }

    private void HealthChanged(float health, float delta, float maxhealth)
    {
        HealthBar.UpdateValue(health, delta, maxhealth);
    }

    private void StartSwitchWeapon()
    {
        _animator.SetTrigger("switchWeapon");
        //_inventoryComponent.NextWeapon();
        
    }

    public void AttackPoint()
    {
        _inventoryComponent.GetActiveWeapon().Attack();
    }
    public void SwitchWeapon()
    {
        _inventoryComponent.NextWeapon();
    }

    private void aimStickUpdated(Vector2 inputvalue)
    {
        aimInput = inputvalue;
        if (aimInput.magnitude > 0)
        {
            _animator.SetBool("attacking",true);
        }
        else
        {
            _animator.SetBool("attacking",false);
        }
    }

    private void MoveStickUpdated(Vector2 inputvalue)
    {
        moveInput = inputvalue;
    }

    Vector3 StickInputWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
       return rightDir * inputVal.x + upDir * inputVal.y;
    }

    private void Update()
    {
        PerformMoveAndAim();
        
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        // Vector3 rightDir = mainCam.transform.right;
        // Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
      
        Vector3 moveDir = StickInputWorldDir(moveInput);
        CharacterController.Move(moveDir * Time.deltaTime * moveSpeed);
         UpdateAim(moveDir);

         float forward = Vector3.Dot(moveDir,transform.forward);
         float right = Vector3.Dot(moveDir,transform.right);
         
         _animator.SetFloat("forwardSpeed",forward);
         _animator.SetFloat("rightSpeed",right);
         CharacterController.Move(Vector3.down * Time.deltaTime * 10f);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir =  currentMoveDir;

        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputWorldDir(aimInput);
        }

        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
                cameraController.AddYawInput(moveInput.x);
            
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = MovementComponent.RotateTowards(aimDir);
        // if (aimDir.magnitude != 0)
        // {
        //     Quaternion prevRotation = transform.rotation;
        //     float turnLerpAlpha = TurnSpeed * Time.deltaTime;
        //     transform.rotation =Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up),turnLerpAlpha);
        //     
        //     Quaternion currentRotation = transform.rotation;
        //     float Dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
        //     float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * Dir;
        //     currentTurnSpeed = rotationDelta / Time.deltaTime;
        //     
        //    
        // }

        
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        _animator.SetFloat("turnSpeed",animatorTurnSpeed);
    }
}

