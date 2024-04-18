using UnityEngine;
using UnityEngine.EventSystems;

public class CollisionPopUp : MonoBehaviour
{
    public GameObject objectToActivate; // GameObject to activate (Elemental options)
    public GameObject fireElement;
    public GameObject waterElement;
    public GameObject airElement;
    public GameObject earthElement;
    private GameObject currentCheckpoint; // To keep track of the current checkpoint being touched

    void Start()
    {
        // Register event handlers for element GameObjects
        RegisterElement(fireElement, "Fire");
        RegisterElement(waterElement, "Water");
        RegisterElement(airElement, "Air");
        RegisterElement(earthElement, "Earth");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.gameObject; // Store the checkpoint being touched
            objectToActivate.SetActive(true); // Enable the element options
            Debug.Log("Elemental has collided with a Checkpoint. Element options displayed.");
        }
    }

    private void RegisterElement(GameObject element, string elementName)
    {
        EventTrigger trigger = element.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { PlayerMadeChoice(elementName); });
        trigger.triggers.Add(entry);
    }

    // Call this method when an element is chosen
    public void PlayerMadeChoice(string chosenElement)
    {
        if (currentCheckpoint != null)
        {
            string correctElement = currentCheckpoint.GetComponent<Checkpoint>().correctElement;
            if (chosenElement == correctElement)
            {
                Debug.Log("Correct element selected. Checkpoint will be destroyed.");
                Destroy(currentCheckpoint); // Destroy the checkpoint
                objectToActivate.SetActive(false); // Disable the element options
            }
            else
            {
                Debug.Log("Incorrect element selected. Try again.");
                // Optionally, provide feedback for incorrect choice
            }
        }
    }
}

