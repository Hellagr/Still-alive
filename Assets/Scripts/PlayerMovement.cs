using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    [Header("Jump Settings")]
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float strafePowerInAir = 2f;

    [Header("Movement settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 100f;
    [SerializeField] private float dashTime = 0.07f;
    [SerializeField] private int avaliableAmountOfDashes = 2;
    public int currentAmountOfDashes { get; private set; }

    private PlayerShoot playerShoot;
    private PlayerModifications playerModifications;
    private CharacterController characterController;
    private InputSystem_Actions action;
    private Vector3 movementWithRotation;
    private Vector2 moveInput;
    private Vector2 moveInputInAir;
    private Vector2 dashDirection;
    private float currentVelocity;
    private float sideSpeedInJumping = 0.2f;
    private int numberOfJumps = 2;
    private int currentNumberOfJumps;
    private bool isJumping = false;
    private bool isDashing = false;
    public Action dashing;

    //Debug stats
    [SerializeField] DebugStats debugStats;

    public float GetSpeed()
    {
        return speed;
    }

    //Modificators
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    //Card buffs
    public void AddSpeed(float addedSpeed)
    {
        speed += addedSpeed;
    }

    public float GetDashTime()
    {
        return dashTime;
    }

    void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        playerModifications = GetComponent<PlayerModifications>();
        characterController = GetComponent<CharacterController>();
        action = new InputSystem_Actions();
        currentNumberOfJumps = numberOfJumps;
        currentVelocity = gravity;
        currentAmountOfDashes = avaliableAmountOfDashes;
    }

    void OnEnable()
    {
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += Stay;
        action.Player.Jump.performed += Jump;
        action.Player.Dash.performed += Dash;
        action.Player.Attack.performed += Shoot;
        action.Enable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else
        {
            moveInputInAir = context.ReadValue<Vector2>() * sideSpeedInJumping;
        }
    }

    private void Stay(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            moveInput = Vector2.zero;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (currentNumberOfJumps > 0)
        {
            isJumping = true;
            currentNumberOfJumps--;
            currentVelocity = jumpVelocity;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (currentAmountOfDashes > 0)
        {
            dashing?.Invoke();
            currentAmountOfDashes--;
            debugStats.DebugStatsUpdate();
            dashDirection = action.Player.Move.ReadValue<Vector2>() != Vector2.zero ? action.Player.Move.ReadValue<Vector2>() : moveInput;
            Vector3 dashVector = new Vector3(dashDirection.x, 0, dashDirection.y).normalized;

            if (currentAmountOfDashes < avaliableAmountOfDashes)
            {
                Invoke("RegenerateOfDash", 1f);
            }

            StartCoroutine(DashAction(dashVector));
        }
    }

    void RegenerateOfDash()
    {
        currentAmountOfDashes++;
        debugStats.DebugStatsUpdate();
    }

    IEnumerator DashAction(Vector3 dashVector)
    {
        var currentTime = 0f;
        SetMovementVector(dashVector);
        var preGravity = currentVelocity;
        isDashing = true;
        currentVelocity = 0f;
        while (currentTime < dashTime)
        {
            currentTime += Time.deltaTime;
            characterController.Move((transform.rotation * dashVector) * dashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        };
        isDashing = false;
        currentVelocity = preGravity;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        playerShoot.Shoot();
    }

    void OnDisable()
    {
        action.Player.Move.performed -= Move;
        action.Player.Move.canceled -= Move;
        action.Player.Dash.performed -= Dash;
        action.Disable();
    }

    void Update()
    {
        MoveCharacter();

        if (isJumping && !isDashing)
        {
            JumpAction();
        }
    }

    private void MoveCharacter()
    {
        var gravityVelocity = Vector3.up * currentVelocity;
        var movementVector = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        transform.rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);

        if (!isJumping)
        {
            SetMovementVector(movementVector);
        }

        characterController.Move((gravityVelocity + (movementWithRotation * speed)) * Time.deltaTime);

        if (characterController.isGrounded)
        {
            InterruptJump();
        }
    }

    private void InterruptJump()
    {
        currentNumberOfJumps = numberOfJumps;
        isJumping = false;
        currentVelocity = gravity;
        moveInput = action.Player.Move.ReadValue<Vector2>();
        moveInputInAir = Vector2.zero;
    }

    private void SetMovementVector(Vector3 movementVector)
    {
        movementWithRotation = transform.rotation * movementVector;
    }

    private void JumpAction()
    {
        var movementVectorInAir = new Vector3(moveInputInAir.x, 0, moveInputInAir.y).normalized;
        Strafe(movementVectorInAir);

        if (currentVelocity > gravity)
        {
            currentVelocity += gravity * Time.deltaTime;
        }
    }

    private void Strafe(Vector3 movementVectorInAir)
    {
        moveInputInAir = action.Player.Move.ReadValue<Vector2>();
        var movementWithRotationInAir = transform.rotation * movementVectorInAir;
        characterController.Move(movementWithRotationInAir * strafePowerInAir * Time.deltaTime);
    }
}