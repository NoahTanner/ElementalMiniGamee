using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string correctElement;
    
    public enum CheckpointType
    {
        Fire,
        Water,
        Air,
        Earth
    }

    public CheckpointType type;
    public UIManager uiManager;  // Reference to the UIManager script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elemental"))
        {
            uiManager.SetCurrentCheckpointType(type);  // Inform UIManager of the current checkpoint type
        }
    }
}
