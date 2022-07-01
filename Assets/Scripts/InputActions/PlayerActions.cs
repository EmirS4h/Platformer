using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class PlayerActions : ScriptableObject, PlayerInputActions.IPlayerActions
{
    PlayerInputActions playerInputActions;

    public event UnityAction interactEvent = delegate { };
    public event UnityAction tpBackEvent = delegate { };
    public event UnityAction dropDownEvent = delegate { };
    public event UnityAction placeTpStoneEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction dashEvent = delegate { };
    public event UnityAction jumpEventCancelled = delegate { };
    public event UnityAction optionsBtn = delegate { };
    public event UnityAction nextBtn = delegate { };
    public event UnityAction prevBtn = delegate { };
    public event UnityAction<Vector2> movementEvent = delegate { };

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.SetCallbacks(this);
            playerInputActions.Player.Enable();
        }
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }
    public void DisableMovement()
    {
        playerInputActions.Player.Disable();
    }
    public void EnableMovement()
    {
        playerInputActions.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            jumpEventCancelled.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            dashEvent.Invoke();
        }
    }

    public void OnOptionsMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            optionsBtn.Invoke();
        }
    }

    public void OnTpToStone(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            tpBackEvent.Invoke();
        }
    }

    public void OnDropDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            dropDownEvent.Invoke();
        }
    }

    public void OnPlaceTpStone(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            placeTpStoneEvent.Invoke();
        }
    }
}
