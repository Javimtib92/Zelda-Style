using UnityEngine;

/// <summary>
/// This class detecs when the level(Location) ends
/// </summary>

public class LevelEnd : MonoBehaviour
{
	public LoadEvent onLevelEnd;
	public GameSceneSO[] locationsToLoad;
	public bool showLoadScreen;
	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("Player"))
		{
			onLevelEnd.Raise(locationsToLoad, showLoadScreen);
		}
	}
}
