using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SeesPlayer", menuName = "State Machines/Conditions/Sees Player")]
public class SeesPlayerConditionSO : StateConditionSO
{
	[SerializeField] private float _distance = 2f;

	protected override Condition CreateCondition() => new SeesPlayerCondition(_distance);
}

public class SeesPlayerCondition : Condition
{
	private Character _characterScript;
	private GameObject _player;
	private float _distance;

	public override void Awake(StateMachine stateMachine)
	{
		_characterScript = stateMachine.GetComponent<Character>();
	}

	public SeesPlayerCondition(float distance)
	{
		_distance = distance;
	}

	public override bool Statement()
	{

		_player = GameObject.FindGameObjectWithTag("Player");

		float distance = Vector2.Distance(_characterScript.transform.position, _player.transform.position);
		return distance < _distance;
	}

	public override void OnStateExit()
	{
		_characterScript.movementVector = Vector2.zero;
	}
}
