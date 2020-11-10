using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsInAttackRange", menuName = "State Machines/Conditions/Is in Attack Range")]
public class IsInAttackRangeConditionSO : StateConditionSO
{
	[SerializeField] private float _range = 0.1f;

	protected override Condition CreateCondition() => new IsInAttackRangeCondition(_range);
}

public class IsInAttackRangeCondition : Condition
{
	private float _range;
	private GameObject _player;
	private Character _characterScript;

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
	}

	public IsInAttackRangeCondition(float range)
	{
		_range = range;
	}

	public override bool Statement()
	{
		_player = GameObject.FindGameObjectWithTag("Player");

		float distance = Vector2.Distance(_characterScript.transform.position, _player.transform.position);
		return distance < _range;
	}

	public override void OnStateExit()
	{
		_characterScript.movementVector = Vector2.zero;
	}
}
