﻿using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    public float verticalGap = 3f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
		int randomValue = GetRandomValue(APIFetcher.Instance.values);
		GameManager.Instance.allSpawns.Add(APIFetcher.Instance.positions[randomValue]); 
		GameManager.Instance.allValues.Add(APIFetcher.Instance.values[randomValue]);
		Debug.Log("Values: " + string.Join(", ", GameManager.Instance.allSpawns));	
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = verticalGap;
    }

	int GetRandomValue(List<int> list)
    {
        // Unity's Random.Range returns an inclusive minimum and exclusive maximum,
        // so we use list.Count to get a valid index
        return UnityEngine.Random.Range(0, list.Count); // randomIndex is from 0 to list.Count - 1
    }

}
