using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyFaceDirection", menuName = "State Machines/Actions/Apply Face Direction Vector")]
public class ApplyFaceDirectionActionSO : StateActionSO<ApplyFaceDirectionAction> { }

public class ApplyFaceDirectionAction : StateAction
{
	//Component references
	private Character _characterScript;

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
	}

	public override void OnUpdate()
	{
		_characterScript.faceDirection = _characterScript.movementVector;
	}
}
