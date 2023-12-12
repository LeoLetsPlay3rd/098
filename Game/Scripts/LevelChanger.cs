using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeSpeed = 1.5f;

    private bool isFading = false;

    private void Start()
    {
        // Start the fade-in effect on the first scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(FadeEffect(Color.clear, new Color(0.1176f, 0.1176f, 0.1176f, 1f)));
        }
    }

    private void Update()
    {
        // Check for input to trigger scene transition
        if (Input.GetKeyDown(KeyCode.F) && !isFading && SceneTransitionController.Instance.CanTransition)
        {
            // Start the fade-in effect when F is pressed
            StartCoroutine(FadeEffect(new Color(0.1176f, 0.1176f, 0.1176f, 1f), Color.clear));
        }
    }

    private System.Collections.IEnumerator FadeEffect(Color startColor, Color endColor)
    {
        isFading = true;

        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            // Interpolate between colors for a smoother transition
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / fadeSpeed);

            // Apply the color to the material
            fadeMaterial.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set
        fadeMaterial.color = endColor;

        if (endColor == Color.clear)
        {
            // Load the next scene in the build index when fade-out is complete
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);

            // Disable this script in the next scene
            SceneTransitionController.Instance.CanTransition = false;
        }

        isFading = false;
    }
}
