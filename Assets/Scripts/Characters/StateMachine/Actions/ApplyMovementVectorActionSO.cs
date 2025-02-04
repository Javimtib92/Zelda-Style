using UnityCore.Controller;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Apply Movement Vector")]
public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> { }

public class ApplyMovementVectorAction : StateAction
{
	//Component references
	private Character _characterScript;
	private Controller2D _characterController;

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
		_characterController = stateMachine.GetComponent<Controller2D>();
	}

	public override void OnUpdate()
	{
		_characterController.Move(_characterScript.movementVector * Time.deltaTime);
	}
}
