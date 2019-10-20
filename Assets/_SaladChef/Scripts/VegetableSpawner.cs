using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{

    [Tooltip("Offset at which a Vegetable will spawn")]
    public Vector3 offset = Vector3.zero;

    [SerializeField]
    [Tooltip("List of Vegetables that are allowed to spawn at this position")]
    private List<VegetableName> allowedVegs = new List<VegetableName>();

    [SerializeField]
    [Tooltip("Interval at which this script will attempt to spawn one of the allowed Vegetables")]
    private float spawnInterval = 2f;

    // Reference to Inventory Manager
    // Check inventory befor deciding to spawn some vegetable
    private InventoryManager inventoryManager;

    // Reference to Spawn System
    // To get the Vegetable Prefab to Spawn
    private SpawnSystem spawnSystem;

    // Specifies if this script can spawn a vegetable now
    private bool canSpawn = true;

    // The Vegetable that was spawned previously
    // Null if no Vegetable is spawned yet
    private Vegetable spawnedVegetable = null;

    // Check if this is already subbed to a delegate form a Player
    // Should not be subbed to both players at the same time
    private bool isListening = false;

    private PlayerInfo focusPlayer = null;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<SpawnSystem>())
        {
            spawnSystem = GetComponentInParent<SpawnSystem>();
        }
        else
        {
            Debug.LogError("Spawn System not referenced");
        }
        if (GetComponentInParent<InventoryManager>())
        {
            inventoryManager = GetComponentInParent<InventoryManager>();
        }
        else
        {
            Debug.LogError("Inventory Manager not referenced");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Overlapped GameObject is Player
        if (other.GetComponent<PlayerInfo>())
        {
            // Overlapped by Player
            focusPlayer = other.GetComponent<PlayerInfo>();
            // Subscribe to Interact Delegate
            if (!isListening)
            {
                other.GetComponent<PlayerMovement>().OnInteract += OnInteract;
                isListening = true;
            }

            //Debug.Log("Overlapped with: " + other.GetComponent<PlayerInfo>().playerNum);
        }
        //Debug.Log("Overlap Detected: " + other.name);
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if there is already an Overlapping Player
        if (focusPlayer)
        {
            // Check if there are any other Overlapping Players (Other than the existing one)
            if (other.GetInstanceID() != focusPlayer.GetInstanceID())
            {
                // More than one Player was overlapping
                // Check if the existing player exited or unsubbed from the delegate
                if (!isListening)
                {
                    // Existing player stopped overlapping
                    // Its safe to Unsub to the delegate here
                    focusPlayer.GetComponent<PlayerMovement>().OnInteract -= OnInteract;
                    // Sub to interaction from the other player and change the FocusPlayer
                    focusPlayer = other.GetComponent<PlayerInfo>();
                    focusPlayer.GetComponent<PlayerMovement>().OnInteract += OnInteract;
                    // Another player just subbed to the Interaction delegate
                    isListening = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the Overlapped GameObject is Player
        if (other.GetComponent<PlayerInfo>())
        {
            // Overlapped by Player
            // Unsubscribe to Interact Delegate
            if (isListening)
            {
                // Overlapping Player exited
                other.GetComponent<PlayerMovement>().OnInteract -= OnInteract;
                // Existing delegate was just unsubbed
                isListening = false;
            }

        }
    }

    private void OnInteract(bool pressed, PlayerInfo player)
    {
        // We are only interested in Key Pressed Event and not Released
        if (pressed)
        {
            Debug.Log(player.playerNum + " Interacted");
        }
    }

    public bool TrySpawnVegetable()
    {
        if (allowedVegs.Count > 0)
        {
            if (canSpawn && spawnedVegetable == null)
            {
                if (SpawnVegetable(GetVegToSpawn()))
                {
                    canSpawn = false;
                    StopAllCoroutines();
                    StartCoroutine(CoolDown());
                    return true;
                }
            }
            else
            {
                Debug.Log("Not able to spawn any Vegetable at the moment");
            }
        }
        return false;
    }

    private IEnumerator CoolDown()
    {
        Debug.Log("Cooldown period for " + spawnedVegetable.vegName + " started");
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }

    private VegetableName GetVegToSpawn()
    {
        VegetableName vegName = allowedVegs[0];
        int count = inventoryManager.GetVegetableCount(allowedVegs[0]);

        for (int i = 1; i < allowedVegs.Count; i++)
        {
            if (inventoryManager.GetVegetableCount(allowedVegs[i]) < count)
            {
                count = inventoryManager.GetVegetableCount(allowedVegs[i]);
                vegName = allowedVegs[i];
            }
        }
        Debug.Log("Selected " + vegName + " to Spawn");
        return vegName;
    }

    private bool SpawnVegetable(VegetableName vegName)
    {
        switch (vegName)
        {
            case VegetableName.Apple:
                if (spawnSystem.Apple)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Apple, transform.position + offset, 
                        spawnSystem.Apple.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;

            case VegetableName.Broccoli:
                if (spawnSystem.Broccoli)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Broccoli, transform.position + offset, 
                        spawnSystem.Broccoli.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;

            case VegetableName.Carrot:
                if (spawnSystem.Carrot)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Carrot, transform.position + offset,
                        spawnSystem.Carrot.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;

            case VegetableName.Lettuce:
                if (spawnSystem.Lettuce)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Lettuce, transform.position + offset, 
                        spawnSystem.Lettuce.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;

            case VegetableName.Olive:
                if (spawnSystem.Olive)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Olive, transform.position + offset,
                        spawnSystem.Olive.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;

            case VegetableName.Peas:
                if (spawnSystem.Peas)
                {
                    spawnedVegetable = Instantiate
                        (spawnSystem.Peas, transform.position + offset,
                        spawnSystem.Peas.transform.rotation).GetComponent<Vegetable>();
                    spawnedVegetable.InventoryManager = inventoryManager;
                    spawnedVegetable.InitVegetable();
                    return true;
                }
                break;
        }
        return false;
    }
}
