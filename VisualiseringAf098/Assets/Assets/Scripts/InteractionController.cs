using System.Collections;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Canvas interactionCanvas;
    public float fadeSpeed = 2f;
    public Transform tinyNode;
    public GameObject interactText;

    private bool isInRange = false;
    private bool isCanvasVisible = false;

    void Start()
    {
        CanvasGroup canvasGroup = interactionCanvas.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on the Canvas. Please add a CanvasGroup component to the Canvas GameObject.");
            enabled = false; // Disable the script if CanvasGroup is missing.
            return;
        }

        HideCanvas();
    }

    void Update()
    {
        CheckInteraction();

        // Scaling logic based on proximity
        float scaleMultiplier = Mathf.Clamp(1f - Vector3.Distance(transform.position, tinyNode.position) / interactionDistance, 0.5f, 1f);
        tinyNode.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);

        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleCanvas();
            }
        }
    }

    void CheckInteraction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the "Player" tag

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is in range for interaction
            isInRange = distance <= interactionDistance;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    void ToggleCanvas()
    {
        if (isCanvasVisible)
        {
            // If the canvas is visible, hide it
            HideCanvas();
        }
        else
        {
            // If the canvas is not visible, show it
            ShowCanvas();
        }
    }

    void ShowCanvas()
    {
        isCanvasVisible = true;
        interactionCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeCanvasGroup(interactionCanvas.GetComponent<CanvasGroup>(), 0f, 1f));
    }

    void HideCanvas()
    {
        isCanvasVisible = false;
        StartCoroutine(FadeCanvasGroup(interactionCanvas.GetComponent<CanvasGroup>(), 1f, 0f, () =>
        {
            interactionCanvas.gameObject.SetActive(false);
        }));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        float alpha = startAlpha;

        while (elapsedTime < fadeSpeed)
        {
            alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (onComplete != null)
        {
            onComplete.Invoke();
        }
    }
}
