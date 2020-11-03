using Cinemachine;
using UnityEngine;

/// <summary>
/// <para>This class listens to the input and it deposits it on the <c>Character</c> component, ready to be used by the <c>StateMachine</c></para>
/// </summary>
public class Protagonist : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader = default;

	private Character _charScript;
	private Vector2 _previousMovementInput;

	private void Awake()
	{
		_charScript = GetComponent<Character>();
	}

	//Adds listeners for events being triggered in the InputReader script
	private void OnEnable()
	{
		_inputReader.moveEvent += OnMove;
		//...
	}

	//Removes all listeners to the events coming from the InputReader script
	private void OnDisable()
	{
		_inputReader.moveEvent -= OnMove;
		//...
	}

	private void Update()
	{
		RecalculateMovement();
	}

	private void RecalculateMovement()
	{
		_charScript.Move(Vector2.ClampMagnitude(_previousMovementInput, 1f));
	}

	//---- EVENT LISTENERS ----

	private void OnMove(Vector2 movement)
	{
		_previousMovementInput = movement;
	}
}
