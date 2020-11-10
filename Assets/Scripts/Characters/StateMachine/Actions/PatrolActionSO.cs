using System.Collections.Generic;
using UnityCore.Controller;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PatrolAction", menuName = "State Machines/Actions/Patrol")]
public class PatrolActionSO : StateActionSO
{
	[Tooltip("XY plane speed multiplier")] [SerializeField] private float _speed = 8f;

	protected override StateAction CreateAction() => new PatrolAction(_speed);
}

public class PatrolAction : StateAction
{
	//Component references
	private Character _characterScript;
	private Controller2D _characterController;

	private float _speed;

	public PatrolAction(float speed)
	{
		_speed = speed;
	}

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
		_characterController = stateMachine.GetComponent<Controller2D>();
	}

	public override void OnUpdate()
	{
		Vector2 direction = _characterScript.wayPointList[_characterScript.nextWayPoint].position - _characterScript.transform.position;

		//check our distance to the current waypoint, Are we near enough?
		if (direction.magnitude < 2f)
		{
			

			if (_characterScript.nextWayPoint < _characterScript.wayPointList.Count - 1) //switch to the nex waypoint if exists
			{
				_characterScript.nextWayPoint++;
			}
			else //begin from new if we are already on the last waypoint
			{
				_characterScript.nextWayPoint = 0;
			}
		}
		direction = direction.normalized;
		direction *= _speed;

		_characterController.Move(direction * Time.deltaTime);
	}
}
