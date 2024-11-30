using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

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
    [SerializeField] private GameObject quitButton;
    private int highscore;
    public List<string> allSpawns;
    public List<int> allValues;
        Dictionary<string, string> locations = new Dictionary<string, string>
        {
            { "42.003309,21.408075", "TINEX (Dramski)" },
            { "42.022,21.356", "Dame Gruev" },
            { "42.00538209603325,21.385693210261966", "Palma Karposh" },
            { "42.016,21.454", "Zabna ordinacija Iliev Ivan" },
            { "41.99421302266008,21.402838605306403", "Kam Market Kozle" },
            { "41.94143946567824,21.520919411765377", "Balon Drachevo" },
            { "42.01288767219,21.40875654761", "Bolnica Sistina" },
            { "41.98390993402405,21.451233146520536", "Rade Konchar" },
            { "41.97043288447,21.47968416117", "PiccadillyS* - language school" },
            { "41.993498532663594,21.44513139126384", "Makedonska poshta (Poshta 2)" },
            { "42.00823982748,21.33677707985", "PZU D-r Despotovska" },
            { "41.99554114553998,21.5870189666748", "Crkva Sv. Troica" },
            { "42.00260380175356,21.461136149901503", "Le Petit Caffe" },
            { "41.960415557926744,21.43481969833374", "Mijoski Teferic" },
            { "41.99144106866676,21.42275119088699", "Rendezvous" },
            { "41.979767336029425,21.433950662612915", "KAM Market (Crniche)" },
            { "41.99918898022106,21.416060439743106", "Furna Silbo Debar Maalo" },
            { "42.00788313967865,21.39364242553711", "Skate Park (Skejt Park)" },
            { "41.995828195848325,21.484215259552002", "Elektrometal" },
            { "42.072,21.504", "Bulachani" },
            { "42.00217598748,21.49814933538", "Gostilnica Jato 90" },
            { "41.978,21.476", "Sportska gostilnica Pace" },
            { "42,21.464", "Merkur (Merkur)" },
            { "42.003768,21.394638", "Skopje City Mall" },
            { "41.93757034466266,21.617401059813652", "Bocca Di Lupo" },
            { "41.99179209680957,21.620027061833486", "Konak Jazbine" },
            { "42.029850425889705,21.34326786835947", "Stopanski Dvor" },
            { "42.00456526869432,21.370220684203826", "Naselba Hrom" },
            { "42.001981658304395,21.388757372706504", "Zhan Mari" },
            { "41.999524662697816,21.496051878253393", "Pekara Den/Dezo" },
            { "41.98360299216671,21.436740013339076", "Cheshma" },
            { "41.981056714489284,21.425465361933103", "Turana" },
            { "42.05,21.516", "OU Naum Ohridski" },
            { "41.99489643006,21.42603605054", "Pastel (Pastel)" },
            { "42.0214996083633,21.446203234042805", "Albion FC" },
            { "41.99249998,21.4236110", "Rendezvous" },
            { "42.01500829536938,21.431869283922524", "Detska gradinka 'Snezhana'" },
            { "42.01313055226713,21.45875573158264", "Dim Market" },
            { "42.027618975073544,21.38741970062256", "Dusko's Pool" },
            { "42.11102052940963,21.44219077861561", "Janmar" },
            { "41.97025864138,21.45175285317", "Cementarnica Stadium" },
            { "42.03178109607825,21.37988980083543", "Monika's Pool" },
            { "41.96032078166045,21.433852549298248", "Mijoski Teferic" },
            { "42.01381277078925,21.383400684279913", "Propoint" },
            { "42.00107311457205,21.42767496688523", "Francophonie Park" },
            { "42.04,21.322", "Bianko campground" },
            { "41.97333621977,21.47142618476", "Sport Life" },
            { "41.934689175939155,21.527361273765564", "Mostot Na Bosfor" },
            { "41.9678799040531,21.597759343232298", "Likove" },
            { "41.98313146067192,21.43476663670465", "Shampionche" },
            { "42.004804509388094,21.35350669037686", "Kaj Ivche" },
            { "41.98561850918707,21.585789277100762", "Zlate Royal Food" },
            { "42.072,21.394", "Crnokrak" },
            { "50.854,2.86", "Speelplein" },
            { "42.001388942078236,21.410187456009414", "Dramski teatar" },
            { "41.98284258321098,21.472823450282345", "Bejking Bred Aerodrom" },
            { "41.97515527046036,21.456478536128998", "Veki" },
            { "41.99924877905,21.40307843685", "Avicena Laboratorija" },
            { "41.9992,21.4408", "Zona Caffe" },
            { "41.98684580602853,21.46617687511082", "Furna Silbo Aerodrom" },
            { "41.98874591491684,21.451932847350726", "Semos Edukativen Centar" },
            { "42.0036,21.4636", "Burekchilnica Tohe" },
            { "42.06232365614429,21.448080907397287", "Parkche Radishani" },
            { "42.00666664,21.38694446", "Pikolinu gradinka" },
            { "41.94484875825,21.40201767294", "La Mia Casa Sonje" },
            { "41.9875,21.6525", "OOU Brakja Miladinovci" },
            { "41.9783,21.47", "Dzhevahir" },
            { "42.033929104624065,21.4095544567309", "Gostilnica 'Merak'" },
            { "41.987585130440756,21.468077392527427", "Kapitol" },
            { "41.987661208718606,21.414543354423493", "Ambasada na Srbija" },
            { "42.055622821657266,21.305010875644516", "Cheshma Kuchkovo" },
            { "42.004552505090906,21.416377346204218", "Kafe Galeri" },
           
        };

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
        GameObject gm = GameObject.Find("GameOver");
        gm.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }


    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        quitButton.SetActive(false);

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
        quitButton.SetActive(false);

        placeText.text = "Skopje";

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
        placeText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
		allSpawns.Clear();
		allValues.Clear();
        quitButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        
		if(allSpawns.Count > 0) 
		{
            Debug.Log(locations[allSpawns[score].ToString()]);
            try
            {
                // Check if the dictionary contains the key
                if (locations.ContainsKey(allSpawns[score].ToString()))
                {
                    // If the key exists, set the text to the corresponding value
                    placeText.text = locations[allSpawns[score].ToString()] +":    " + allValues[score].ToString();
                    
                }
                else
                {
                    // Optionally, handle the case where the key doesn't exist
                    placeText.text = "SKOPJE";  // Default text
                }
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the KeyNotFoundException specifically if thrown
                Debug.Log("WRONG: " + allSpawns[score].ToString());
                placeText.text = "SKOPJE";  // Fallback value in case of error
            }
            catch (Exception ex)
            {
                // Catch any other exceptions
                Debug.LogError($"An unexpected error occurred: {ex.Message}");
                placeText.text = "SKOPJE";  // Fallback value in case of other errors
            }
        }
        score++;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        scoreText.text = score.ToString();
    }

}
