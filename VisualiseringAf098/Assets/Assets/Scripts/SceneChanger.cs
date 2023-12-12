using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string citationSceneName = "Citation";
    public Button startButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the buttons
        startButton.onClick.AddListener(StartButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add more logic here if needed
    }

    // Function to handle start button click
    void StartButtonClicked()
    {
        SceneManager.LoadScene(citationSceneName);
    }

    // Function to handle quit button click
    void QuitButtonClicked()
    {
        // You can add more cleanup or save functionality here if needed
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
