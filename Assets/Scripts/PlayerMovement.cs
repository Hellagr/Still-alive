using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPlayerModifiers
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintModifier = 2f;
    // [SerializeField] private float sprintAcceleration = 0.2f;
    private PlayerShoot playerShoot;
    private PlayerModifications playerModifications;
    private CharacterController characterController;
    private InputSystem_Actions action;
    private Vector3 movementWithRotation;
    private Quaternion rotation;
    private Vector2 moveInput;
    private Vector2 moveInputInAir;
    private float currentVelocity = -9.81f;
    private float jumpVelocity = 10f;
    private float sideSpeedInJumping = 0.2f;
    //float standartFOV = 100f;
    //float sprintFOV = 120f;
    private int numberOfJumps = 2;
    private int currentNumberOfJumps;
    private bool isJumping = false;

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        //StartCoroutine(ChangeSpeed(newSpeed, sprintAcceleration));
        speed = newSpeed;
    }

    //IEnumerator ChangeSpeed(float newSpeed, float duration)
    //{
    //    var t = 0f;
    //    float initialSpeed = speed;

    //    while (t < 1f)
    //    {
    //        t += Time.deltaTime;
    //        speed = Mathf.Lerp(initialSpeed, newSpeed, t);
    //        yield return null;
    //    }
    //    speed = newSpeed;
    //}

    void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        playerModifications = GetComponent<PlayerModifications>();
        characterController = GetComponent<CharacterController>();
        action = new InputSystem_Actions();
        currentNumberOfJumps = numberOfJumps;
    }

    void OnEnable()
    {
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += Stay;
        action.Player.Jump.performed += Jump;
        action.Player.Sprint.performed += Sprint;
        action.Player.Sprint.canceled += BackToNormalSpeed;
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

    private void Sprint(InputAction.CallbackContext context)
    {
        playerModifications.ApplyMovementModifier(this);
    }

    private void BackToNormalSpeed(InputAction.CallbackContext context)
    {
        playerModifications.RemoveMovementModifier(this);
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        playerShoot.Shoot();
    }

    void OnDisable()
    {
        action.Player.Move.performed -= Move;
        action.Player.Move.canceled -= Move;
        action.Player.Sprint.performed -= Sprint;
        action.Player.Sprint.canceled -= BackToNormalSpeed;
        action.Disable();
    }

    void Update()
    {
        MoveCharacter();

        if (isJumping)
        {
            JumpAction();
        }
    }

    private void MoveCharacter()
    {
        var gravityVelocity = Vector3.up * currentVelocity;
        var movementVector = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        var movementVectorInAir = new Vector3(moveInputInAir.x, 0, moveInputInAir.y).normalized;
        rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y, 0);

        if (!isJumping)
        {
            movementWithRotation = rotation * movementVector;
        }

        characterController.Move((gravityVelocity + (movementWithRotation * speed)) * Time.deltaTime);

        if (isJumping)
        {
            moveInputInAir = action.Player.Move.ReadValue<Vector2>();
            var movementWithRotationInAir = rotation * movementVectorInAir;
            characterController.Move(movementWithRotationInAir * Time.deltaTime);
        }
    }

    private void JumpAction()
    {
        if (currentVelocity > gravity)
        {
            currentVelocity += gravity * Time.deltaTime;
        }
        else
        {
            currentNumberOfJumps = numberOfJumps;
            isJumping = false;
            currentVelocity = gravity;
            moveInput = action.Player.Move.ReadValue<Vector2>();
            moveInputInAir = Vector2.zero;
        }
    }

    public float GetMovementModifier()
    {
        return sprintModifier;
    }
}