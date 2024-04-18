using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ElementInteraction : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject fireElement;
    public GameObject waterElement;
    public GameObject airElement;
    public GameObject earthElement;
    private GameObject currentCheckpoint;
    private ElementalMovement elementalMovement; // Reference to the ElementalMovement script
    public Animator transition;
    public string sceneToLoad = "YourSceneName";
    public float transitionTime = 1f;

    public Text countdownText;  // Reference to the UI Text component for countdown
    // public TextMeshProUGUI countdownText;  // Use this line instead if using TextMeshPro

    private float countdownDuration = 3.0f;
    private bool isCountingDown = false;

    void Start()
    {
        elementalMovement = GetComponent<ElementalMovement>(); // Get the movement script
        RegisterElement(fireElement, "Fire");
        RegisterElement(waterElement, "Water");
        RegisterElement(airElement, "Air");
        RegisterElement(earthElement, "Earth");
        objectToActivate.SetActive(false);
        countdownText.gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.gameObject;
            objectToActivate.SetActive(true);
            Debug.Log("Checkpoint encountered. Element options displayed.");
            elementalMovement.StopMoving(); // Stop the elemental's movement
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        countdownDuration = 3.0f;  // Reset the countdown
        isCountingDown = true;
        countdownText.gameObject.SetActive(true);  // Show the countdown text
    }

    private void StopCountdown()
    {
        isCountingDown = false;
        countdownText.gameObject.SetActive(false);  // Hide the countdown text
        //elementalMovement.StartMoving();  // Resume movement
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            elementalMovement.StopMoving();
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void RegisterElement(GameObject element, string elementName)
    {
        EventTrigger trigger = element.GetComponent<EventTrigger>() ?? element.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { PlayerMadeChoice(elementName); });
        trigger.triggers.Add(entry);
    }

    public void PlayerMadeChoice(string chosenElement)
    {
        if (currentCheckpoint != null && currentCheckpoint.GetComponent<Checkpoint>().correctElement == chosenElement)
        {
            Debug.Log("Correct element selected. Checkpoint will be destroyed.");
            Destroy(currentCheckpoint);
            currentCheckpoint = null;
            StopCountdown();
            objectToActivate.SetActive(false);
            elementalMovement.StartMoving(); // Resume the elemental's movement
        }
        else
        {
            Debug.Log("Incorrect element selected. Try again.");
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            countdownDuration -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(countdownDuration).ToString();
            if (countdownDuration <= 0)
            {
                StopCountdown();
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
            }
        }
    }
}
