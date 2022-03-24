using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }
    public List<GameObject> preFabList = new List<GameObject>();
    private string activeScene = "Main";
    public Button playGameButton;
    public Button settingsButton;
    public Button aboutButton;
    public Button backButton;
    public Button quitButton;
    public Button mainMenuButton;
    public Button exitGameButton;
    public Button controlsButton;
    public Button questButton;
    private bool GameOver = false;

    public Image blackScreen; //BD
    public float fadeSpeed = 2f; //BD
    public bool fadeToBlack; //BD
    public bool fadeFromBlack; //BD

    private Dictionary<string, GameObject> canvasDictionary = new Dictionary<string, GameObject>();

    void Update() //BD
    {
        if (fadeToBlack) 
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(blackScreen.color.a == 1f) 
            {
                fadeToBlack = false; 
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void SetGameOver(bool isOver)
    {
        GameOver = isOver;
    }
    public void Awake()
    {
        // Singleton pattern to keep UI_Controller active and functional through scenes.
        // Similar to the design shown by Professor Price in his UIManager_Simple Script at line 23 in his code
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            activeScene = SceneManager.GetActiveScene().name;

            // Instantiate, transform and insert the canavses into a dictionary
            // Similar to the design shown by Professor Price in his UI_Manager_Simple script at line 35 in his code
            foreach (GameObject prefab in preFabList)
            {
                GameObject canvasToAdd = Instantiate(prefab);
                canvasToAdd.name = prefab.name;
                canvasToAdd.transform.SetParent(transform);
                canvasDictionary.Add(canvasToAdd.name.ToString(), canvasToAdd);
            }
            // Load the main scene canvas if the main scene is active
            // Provide functionality to all of the buttons
            // Similar to the design shown by Professor Price in his UI_Manager_Simple script at line 44 in his code
            if (activeScene == "Main")
            {
                // Set each canvas to inactive before setting the canvas for the main scene to active
                foreach (KeyValuePair<string, GameObject> entry in canvasDictionary)
                {
                    entry.Value.SetActive(false);
                }
                GameObject mainCanvas = canvasDictionary["Canvas_Main"];
                mainCanvas.SetActive(true);

                playGameButton = GameObject.Find("Button_Play_Game").GetComponent<Button>();
                playGameButton.onClick.AddListener(() => PlayGame());

                settingsButton = GameObject.Find("Button_Settings").GetComponent<Button>();
                settingsButton.onClick.AddListener(() => LoadSceneByNumber(2));

                aboutButton = GameObject.Find("Button_About").GetComponent<Button>();
                aboutButton.onClick.AddListener(() => LoadSceneByNumber(1));

                quitButton = GameObject.Find("Button_Quit").GetComponent<Button>();
                quitButton.onClick.AddListener(() => QuitGame(0));

                questButton = GameObject.Find("Button_Quest").GetComponent<Button>();
                questButton.onClick.AddListener(() => LoadSceneByNumber(3));

                controlsButton = GameObject.Find("Button_Controls").GetComponent<Button>();
                controlsButton.onClick.AddListener(() => LoadSceneByNumber(8));
                GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Main Theme");
            }
        }
        else
        {
            Destroy(gameObject); // Destroy other types of this gameObject
        }
    }

    // Load already instantiated canvases based on which scene is active. 
    // Called by the LoadSceneByNumber function
    // Similar to Onlevelwasloaded function in Professor Price's UIManager_Simple script at line 74 in his code
    void OnSceneLoaded(int sceneNumber)
    {
        if (canvasDictionary == null) return;
        // Scene 0 is the Main Scene
        if (sceneNumber == 0)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);


            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Main Theme");
        }
        // Scene 1 is the About Scene
        else if (sceneNumber == 1)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);



            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

          

            backButton = GameObject.Find("Button_Back").GetComponent<Button>();
            backButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }
        // Scene 2 is the Settings Scene
        else if (sceneNumber == 2)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);



            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

           

            backButton = GameObject.Find("Button_Back").GetComponent<Button>();
            backButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }
        // Scene 3 is the Quest Scene
        else if (sceneNumber == 3)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);



            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(true);

           

            backButton = GameObject.Find("Button_Back").GetComponent<Button>();
            backButton.onClick.AddListener(() => LoadSceneByNumber(0));


        }
        // Game Over scene
        else if (sceneNumber == 4)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);


            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Main Theme");

            mainMenuButton = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }
        // Credits scene
        else if (sceneNumber == 5)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);


            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Main Theme");

            mainMenuButton = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }
        // Level A1
        else if (sceneNumber == 6)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);


            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

           

            exitGameButton = GameObject.Find("Button_Exit_Game").GetComponent<Button>();
            exitGameButton.onClick.AddListener(() => LoadSceneByNumber(0));

            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Background Noise");
        }
        // Level A2
        else if (sceneNumber == 7)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(false);


            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

            GameObject.Find("AudioManager").GetComponent<AudioManager>().StopAll();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Background Noise");

            exitGameButton = GameObject.Find("Button_Exit_Game").GetComponent<Button>();
            exitGameButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }
        
        // Controls scene
        else if (sceneNumber == 8)
        {
            GameObject currentCanvas = canvasDictionary["Canvas_Main"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Credits"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_HUD"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_About"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Settings"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Game_Over"];
            currentCanvas.SetActive(false);

            currentCanvas = canvasDictionary["Canvas_Controls"];
            currentCanvas.SetActive(true);

            currentCanvas = canvasDictionary["Canvas_Quest"];
            currentCanvas.SetActive(false);

          
            backButton = GameObject.Find("Button_Back").GetComponent<Button>();
            backButton.onClick.AddListener(() => LoadSceneByNumber(0));
        }

      

    }
    // Changes scene when called by a button
    // Calls the OnSceneLoaded function to load the canvas
    public void LoadSceneByNumber(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        OnSceneLoaded(sceneNumber);
    }

    public void PlayGame()
    {
        Player_Manager playerManager = GameObject.Find("PlayerManager").GetComponent<Player_Manager>();
        Player_Manager.Player playerReference = playerManager.player;
        playerReference.SetCoins(0);
        playerReference.SetLives(4);
        playerReference.SetIsImmune(false);
        playerReference.gemList = new LinkedList<GameObject>();
        playerReference.RemoveAllPowerUps();
        int firstLevelSceneNum = 6;
        LoadSceneByNumber(firstLevelSceneNum);
        playerReference.SetSpawnPoint(new Vector3(300, 5, 700));
    }

    // Quits the game when called by the quit button
    public void QuitGame(int ignoreLevel)
    {
        Debug.Log("quit called");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // set the PlayMode to stop
#else
        Application.Quit();
#endif
    }
}