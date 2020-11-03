﻿using UnityEngine;

// Created with collaboration from:
// https://forum.unity.com/threads/inventory-system.980646/
public class InventoryController : MonoBehaviour
{
	[SerializeField]
	private Inventory _inventory = default;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<ItemInstance>(out ItemInstance itemInstance))
		{
			_inventory.Add(itemInstance.Item);
			Destroy(itemInstance.gameObject);
		}
	}
}
