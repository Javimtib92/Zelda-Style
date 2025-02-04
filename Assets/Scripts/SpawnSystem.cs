﻿using Cinemachine;
using System;
using System.Linq;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private int _defaultSpawnIndex = 0;

	[Header("Project References")]
	[SerializeField] private Protagonist _playerPrefab = null;

	[Header("Scene References")]
	[SerializeField] private Transform[] _spawnLocations;

	[SerializeField] private CinemachineVirtualCamera _cameraManager = default;

	void Start()
	{
		Debug.Log("awakening");
		try
		{
			Spawn(_defaultSpawnIndex);
		}
		catch (Exception e)
		{
			Debug.LogError($"[SpawnSystem] Failed to spawn player. {e.Message}");
		}
	}

	void Reset()
	{
		AutoFill();
	}

	[ContextMenu("Attempt Auto Fill")]
	private void AutoFill()
	{
		if (_spawnLocations == null || _spawnLocations.Length == 0)
			_spawnLocations = transform.GetComponentsInChildren<Transform>(true)
								.Where(t => t != this.transform)
								.ToArray();
	}

	private void Spawn(int spawnIndex)
	{
		Transform spawnLocation = GetSpawnLocation(spawnIndex, _spawnLocations);
		Protagonist playerInstance = InstantiatePlayer(_playerPrefab, spawnLocation);

		Debug.Log(spawnLocation);
		Debug.Log(playerInstance);
		_cameraManager.Follow = playerInstance.transform;
	}

	private Transform GetSpawnLocation(int index, Transform[] spawnLocations)
	{
		if (spawnLocations == null || spawnLocations.Length == 0)
			throw new Exception("No spawn locations set.");

		index = Mathf.Clamp(index, 0, spawnLocations.Length - 1);
		return spawnLocations[index];
	}

	private Protagonist InstantiatePlayer(Protagonist playerPrefab, Transform spawnLocation)
	{
		if (playerPrefab == null)
			throw new Exception("Player Prefab can't be null.");

		Protagonist playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

		return playerInstance;
	}
}
