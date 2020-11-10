using System.Collections.Generic;
using UnityCore.Controller;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AttackAction", menuName = "State Machines/Actions/Attack")]
public class AttackActionSO : StateActionSO
{
	protected override StateAction CreateAction() => new AttackAction();
}

public class AttackAction : StateAction
{
	//Component references
	private Character _characterScript;
	private Controller2D _characterController;
	private GameObject _player;

	public AttackAction() { }

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
		_characterController = stateMachine.GetComponent<Controller2D>();
	}

	public override void OnUpdate()
	{
		Debug.Log("Attack");
	}
}
