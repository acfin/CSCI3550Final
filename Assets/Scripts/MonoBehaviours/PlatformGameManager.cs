using UnityEngine;
using Cinemachine;

public class PlatformGameManager : MonoBehaviour
{
    // Reference to the spawn point designed for the player
    // Needed so the player can be re-spawned when they die
    public SpawnPoint playerSpawnPoint;

    // Reference to the virtual camera in the scene
    public GameObject vCam;

    // A variable used to access the singleton object
    public static PlatformGameManager sharedInstance = null;

    // Ensure only a single instance of the RPGGameManager exists
    // It's possible to get multiple instances if multiple copies of the RPGGameManager exists in the Hierarchy
    // or if multiple copes are programmatically instantiated
    public void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    // Create a new player object if one does not already exist
    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            // Spawn player
            GameObject player = playerSpawnPoint.SpawnObject();

            // Find the virtual camera object in the current scene
            //GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");

            // Get a reference to the virtual camera component of the virtual camera
            CinemachineVirtualCamera virtualCamera = vCam.GetComponent<CinemachineVirtualCamera>();

            // Set the virtual camera to follow the player that was just spawned
            virtualCamera.Follow = player.transform;
        }
    }
}
