using System.Collections.Generic;
using UnityCore.Controller;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ChaseAction", menuName = "State Machines/Actions/Chase")]
public class ChaseActionSO : StateActionSO
{
	protected override StateAction CreateAction() => new ChaseAction();
}

public class ChaseAction : StateAction
{
	//Component references
	private Character _characterScript;
	private Controller2D _characterController;
	private GameObject _player;

	public ChaseAction(){}

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
		_characterController = stateMachine.GetComponent<Controller2D>();
	}

	public override void OnUpdate()
	{
		if (!_player)
		{
			_player = GameObject.FindGameObjectWithTag("Player");
		}
		
		Vector2 direction = _player.transform.position - _characterScript.transform.position;

		direction = direction.normalized;
		direction *= 5;

		_characterController.Move(direction * Time.deltaTime);
	}
}
