using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
	[SerializeField] private Text placeText;
	public List<string> allSpawns; 
	public List<int> allValues; 

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
		allSpawns.Clear();
		allValues.Clear();
        Pause();
    }

    public void IncreaseScore()
    {
		if(allSpawns.Count > 0) 
		{
			placeText.text = allSpawns[score].ToString() + " " + allValues[score].ToString();	
		}
        score++;
        scoreText.text = score.ToString();
    }

}
