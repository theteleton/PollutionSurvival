using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class BackgroundPicker : MonoBehaviour
{
    public Sprite red, green, yellow;
    private Camera mainCamera;
    private List<int> vals;

    GameObject startObject ;

 
    void Start()
    {
        startObject = GameObject.Find("Start");
        StartCoroutine(UpdatePanelImageBasedOnAPI());
        startObject.SetActive(false);

    }

    IEnumerator UpdatePanelImageBasedOnAPI()
    {
        // Wait until APIFetcher is ready
        while (APIFetcher.Instance == null || APIFetcher.Instance.values.Count == 0)
        {
            Debug.Log("Waiting for API data...");
            yield return new WaitForSeconds(1f);
        }

        List<int> apiValues = APIFetcher.Instance.GetValues();
        float average = 0.0f;

        for (int i = 0; i < apiValues.Count; i++)
        {
            average += apiValues[i];
        }

        GameObject targetObject = GameObject.Find("Panel");
        GameObject textObject = GameObject.Find("Text");
        GameObject difficultyObject = GameObject.Find("DifficultyText");
        GameObject loadingObject= GameObject.Find("Loading");
        average /= apiValues.Count;
        
        Debug.Log(average);
        startObject.SetActive(true);
        targetObject.GetComponent<Image>().color = Color.white;

        //Debug.Log(average);
        if (average < 50.0f)
        {
            loadingObject.GetComponent<TextMeshProUGUI>().text = "";
            targetObject.GetComponent<Image>().sprite = green;
            textObject.GetComponent<TextMeshProUGUI>().text = "Low AIR POLLUTION WARNING!";
            difficultyObject.GetComponent<TextMeshProUGUI>().text = "Difficulty: EASY!"; 

        }
        else if (average < 100.0f)
        {
            targetObject.GetComponent<Image>().sprite = yellow;
            textObject.GetComponent<TextMeshProUGUI>().text = "Medium AIR POLLUTION WARNING!";
            difficultyObject.GetComponent<TextMeshProUGUI>().text = "Difficulty: MEDIUM!";
            loadingObject.GetComponent<TextMeshProUGUI>().text = "";


        }
        else
        {
            targetObject.GetComponent<Image>().sprite = red;
            textObject.GetComponent<TextMeshProUGUI>().text = "High AIR POLLUTION WARNING!";
            difficultyObject.GetComponent<TextMeshProUGUI>().text = "Difficulty: EXTREME";
            loadingObject.GetComponent<TextMeshProUGUI>().text = "";

        }


    }

    /*
    int GetRandomValue(List<int> list)
    {
        // Unity's Random.Range returns an inclusive minimum and exclusive maximum,
        // so we use list.Count to get a valid index
        return UnityEngine.Random.Range(0, list.Count); // randomIndex is from 0 to list.Count - 1
    }
    void Start()
    {
        if (APIFetcher.Instance != null)
        {
            yield vals = APIFetcher.Instance.values;
            
        }
      
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
        

    }
    */
}
