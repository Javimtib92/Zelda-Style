using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Character _charScript;
	[SerializeField] private List<Transform> wayPointList;

	private void Awake()
	{
		_charScript = GetComponent<Character>();

		_charScript.wayPointList = wayPointList;
	}
}
