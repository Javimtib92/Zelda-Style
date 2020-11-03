using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "MoveAction", menuName = "State Machines/Actions/Move")]
public class MoveActionSO : StateActionSO
{
	[Tooltip("Horizontal XZ plane speed multiplier")] [SerializeField] private float _speed = 8f;

	protected override StateAction CreateAction() => new WalkAction(_speed);
}

public class WalkAction : StateAction
{
	//Component references
	private Character _characterScript;

	private float _speed;

	public WalkAction(float speed)
	{
		_speed = speed;
	}

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
	}

	public override void OnUpdate()
	{
		_characterScript.movementVector.x = _characterScript.movementInput.x * _speed;
		_characterScript.movementVector.y = _characterScript.movementInput.y * _speed;
	}
}
