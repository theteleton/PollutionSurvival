using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{
    public Pipes prefab, prefab_red, prefab_yellow;
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
		
        float randomValue2 =  (float)APIFetcher.Instance.values[randomValue];
        Debug.Log(randomValue2);
       Pipes pipes;
        if (randomValue2 < 16.0f)
        {
             pipes = Instantiate(prefab, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 3.5f;
        }
        else if (randomValue2 < 40f)
        {
             pipes = Instantiate(prefab_yellow, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 2.5f;
        }
        else if (randomValue2 < 65.0f)
        {
             pipes = Instantiate(prefab_red, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 2f;
        }
        else if(randomValue2 < 150.0f)
        {
             pipes = Instantiate(prefab_red, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 1.2f;
        }
        else if(randomValue2 < 250.0f)
        {
             pipes = Instantiate(prefab_red, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 1f;
        }
        else
        {
            pipes = Instantiate(prefab_red, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

            pipes.gap = 0f;
        }
       // pipes.gap = (float)((1 - randomValue2) * verticalGap) + 1f;
        
        Debug.Log("Values: " + pipes.gap + " " + ((float)(randomValue2 * verticalGap) + 1f));
    }

	int GetRandomValue(List<int> list)
    {
        return UnityEngine.Random.Range(0, list.Count); // randomIndex is from 0 to list.Count - 1
    }

}
