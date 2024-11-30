using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq; // Requires Newtonsoft.Json package for JSON parsing

public class APIFetcher : MonoBehaviour
{
    public static APIFetcher Instance; // Singleton instance

    private string apiUrl = "https://skopje.pulse.eco/rest/current"; // Replace with your API URL

    public List<int> values = new List<int>(); // List to store 'value' field
    public List<string> positions = new List<string>(); // List to store 'position' field

    private void Awake()
    {
        // Singleton pattern setup
        if (Instance == null)
        {
            Debug.Log("Instance found!");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FetchAPIData());
    }

    IEnumerator FetchAPIData()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                ProcessAPIResponse(jsonResponse);
            }
            else
            {
                Debug.LogError("Error fetching API data: " + request.error);
            }

            // Wait for 20 seconds before making the next request
            yield return new WaitForSeconds(1100f);
        }
    }
    public List<int> GetValues()
    {
        return values;
    }

    private void ProcessAPIResponse(string jsonResponse)
    {
        try
        {
            // Clear existing data
            values.Clear();
            positions.Clear();

            // Parse the JSON array
            JArray jsonArray = JArray.Parse(jsonResponse);
			
            foreach (var item in jsonArray)
            {
                // Extract 'value' as int and 'position' as string
                int value = int.Parse(item["value"].ToString());
                string position = item["position"].ToString();
				string type = item["type"].ToString(); 
                // Add to respective lists
				if (type == "pm10") 
				{
                	values.Add(value);
                	positions.Add(position);
				}
            }

            Debug.Log("Values: " + string.Join(", ", values));
            Debug.Log("Positions: " + string.Join(", ", positions));
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error processing API response: " + e.Message);
        }

        
    }
}
