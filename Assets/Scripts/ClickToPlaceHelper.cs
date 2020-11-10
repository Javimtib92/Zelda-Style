using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("UOP1/Tools/Click to Place")]
public class ClickToPlaceHelper : MonoBehaviour
{
	[Tooltip("Vertical offset above the clicked point. Useful to avoid spawn points to be directly ON the geometry which might cause issues.")]
	[SerializeField] float _verticalOffset = 0.1f;

	[Tooltip("Collision mask. Useful to avoid spawn points on top of obstacles")]
	[SerializeField] LayerMask collisionMask = default;

	private Vector2 _spawnPosition;
	private bool _displaySpawnPosition = false;

	private delegate void ButtonAction();
	private ButtonAction myButtonAction;

	private Color gizmosColor;

	private void OnDrawGizmos()
	{
		if (_displaySpawnPosition)
		{
			Gizmos.color = gizmosColor;
			Gizmos.DrawWireSphere(_spawnPosition, 0.3f);
		}
	}

	void OnMouseClick(SceneView scene)
	{
		Event currentGUIEvent = Event.current;

		Vector2 mousePos = currentGUIEvent.mousePosition;

		_spawnPosition = (Vector2)HandleUtility.GUIPointToWorldRay(mousePos).origin + Vector2.up * _verticalOffset;

		RaycastHit2D raycastHit = Physics2D.Raycast(_spawnPosition, Vector2.zero, 0f, collisionMask);

		if (raycastHit)
		{
			gizmosColor = Color.red;
		}
		else if (gizmosColor != Color.green)
		{
			gizmosColor = Color.green;
		}

		if (currentGUIEvent.type == EventType.MouseMove)
		{
			HandleUtility.Repaint();
		}
		if (currentGUIEvent.type == EventType.MouseDown
			&& currentGUIEvent.button == 0 && !raycastHit) // Wait for Left mouse button down
		{
			myButtonAction();
			SceneView.duringSceneGui -= OnMouseClick;
			_displaySpawnPosition = false;

			currentGUIEvent.Use(); // This consumes the event, so that other controls/buttons won't be able to use it
		}
	}

	public void SetSpawnLocationAtCursor()
	{
		Debug.Log("Use the LMB to position this object");
		myButtonAction = SetTransform;
		_displaySpawnPosition = true;
		SceneView.duringSceneGui += OnMouseClick;
	}

	/// <summary>
	/// The delegate called when the mouse is clicked in the viewport
	/// </summary>
	private void SetTransform()
	{
		transform.position = _spawnPosition;
		Debug.Log("Object moved to " + _spawnPosition);
	}
}
