using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private string levelOneStart = "*There's a voice from the darkness...* You have to get past... You have to slay them... Slay them all...";
    private char[] textSymbols;
    private float symbolCounter = 0;
    private int symbolNr = 0;
    public Text storyText;
    public List<GameObject> healthArray;
    public GameObject heartSprite;
    public GameObject player;
    public GameObject canvas;
    public bool playText;
    public bool sceneHasText;
    private GameObject heart;

    void Awake()
    {

        healthArray = new List<GameObject>();

    }

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        // The script won't load text if the scene doesn't contain it -Seb
        if (sceneHasText)
            storyText = GameObject.FindGameObjectWithTag("StoryText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (playText)
            TellStory(1);

        CheckSpawnHearts();
        //Call CheckSpawnHeart if maxheart changes: Kajsa
    }

    public void CheckSpawnHearts()
    {
        /*foreach (GameObject heart in healthArray)
        {
            Destroy(heart);
        }*/

        // This code spawns the hearts on the screen -Seb
        if (healthArray.Count < Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2))
        {
            for (int i = 0; i < Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2); i++)
            {
                if (healthArray.Count != i)
                {
                    Destroy(healthArray[i]);
                    healthArray.RemoveAt(i);
                }

                heart = Instantiate(heartSprite, GameObject.FindGameObjectWithTag("Heart").GetComponent<Transform>().position, transform.rotation) as GameObject;
                heart.transform.SetParent(canvas.transform);
                healthArray.Add(heart);

                //Decides how many gets drawn based on health: Kajsa

                heart.transform.position = new Vector2(heart.transform.position.x + (64 * i), heart.transform.position.y);
                //Moves heart to the side so every heart is visibile: Kajsa
            }
        }
        else if (healthArray.Count == Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2))
        {
            // If the health of the player is odd this will scale down the heart to half size -Seb
            if (player.GetComponent<PlayerController>().health % 2 == 1)
            {
                healthArray[Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2) - 1].transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }

            else if (player.GetComponent<PlayerController>().health % 2 == 0)
            {
                healthArray[Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2) - 1].transform.localScale = new Vector3(1f, 1f, 1);
            }
        }
        // This delites the hearts if thay should'nt be on the screen -Seb
        else
        {
            Destroy(healthArray[Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2)]);
            healthArray.RemoveAt(Mathf.CeilToInt((float)player.GetComponent<PlayerController>().health / 2));
        }

    }

    // Exits the program -Lucas
    public void ExitApplication()
    {
        Application.Quit();
        audioSource.PlayOneShot(clickSound);
    }

    // Loads a new Scene -Lucas
    public void LoadScene()
    {
        Application.LoadLevel("Level 1");
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound,0.3f);
    }

    // Tells a specified part of the story by text on the screen -Seb
    public void TellStory(int messageNr)
    {
        if (messageNr == 1)
        {
            textSymbols = levelOneStart.ToCharArray();
        }

        symbolCounter += Time.deltaTime;

        // Writes a text a symbol at a time -Seb
        if (symbolCounter >= 0.13f && symbolNr < textSymbols.Length)
        {
            storyText.text += textSymbols[symbolNr];
            symbolNr++;
            symbolCounter = 0;
        }
    }
}
