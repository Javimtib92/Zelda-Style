using UnityEngine;

/// <summary>
/// <para>This class is a data holder that the <c>StateMachine</c> class uses to deposit data that needs to be shared between states.
/// Ideally, both the player character and NPCs can use this component to drive locomotion.</para>
/// </summary>
public class Character : MonoBehaviour
{
	//These fields are manipulated by the StateMachine actions
	[HideInInspector] public Vector2 movementInput; //Initial input coming from the Protagonist script
	[HideInInspector] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
	[HideInInspector] public Vector2 faceDirection; //Face direction determined by the last movementVector

	//---- COMMANDS ISSUED BY OTHER SCRIPTS ----
	public void Move(Vector2 movement)
	{
		movementInput = movement;
	}
}
