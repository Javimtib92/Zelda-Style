using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
	public event UnityAction attackEvent;
	public event UnityAction interactEvent;
	public event UnityAction extraActionEvent;
	public event UnityAction pauseEvent;
	public event UnityAction<Vector2> moveEvent;

	public GameInput gameInput;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.Gameplay.SetCallbacks(this);
		}
		gameInput.Gameplay.Enable();
	}

	private void OnDisable()
	{
		gameInput.Gameplay.Disable();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (attackEvent != null
			&& context.phase == InputActionPhase.Performed)
			attackEvent.Invoke();
	}

	public void OnExtraAction(InputAction.CallbackContext context)
	{
		if (extraActionEvent != null
			&& context.phase == InputActionPhase.Performed)
			extraActionEvent.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (interactEvent != null
			&& context.phase == InputActionPhase.Performed)
			interactEvent.Invoke();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (moveEvent != null)
		{
			moveEvent.Invoke(context.ReadValue<Vector2>());
		}
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (pauseEvent != null
			&& context.phase == InputActionPhase.Performed)
			pauseEvent.Invoke();
	}
}
