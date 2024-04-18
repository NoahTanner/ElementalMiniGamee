using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject fireElement;
    public GameObject waterElement;
    public GameObject airElement;
    public GameObject earthElement;

    private Checkpoint.CheckpointType currentCheckpointType;

    void Start()
    {
        // Register click event handlers
        AddClickHandler(fireElement, Checkpoint.CheckpointType.Fire);
        AddClickHandler(waterElement, Checkpoint.CheckpointType.Water);
        AddClickHandler(airElement, Checkpoint.CheckpointType.Air);
        AddClickHandler(earthElement, Checkpoint.CheckpointType.Earth);
    }

    private void AddClickHandler(GameObject element, Checkpoint.CheckpointType type)
    {
        EventTrigger trigger = element.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnElementSelected(type); });
        trigger.triggers.Add(entry);
    }

    public void SetCurrentCheckpointType(Checkpoint.CheckpointType type)
    {
        currentCheckpointType = type;
    }

    private void OnElementSelected(Checkpoint.CheckpointType selectedType)
    {
        if (selectedType == currentCheckpointType)
        {
            Debug.Log("Correct element selected!");
        }
        else
        {
            Debug.Log("Incorrect element selected!");
        }
    }
}
