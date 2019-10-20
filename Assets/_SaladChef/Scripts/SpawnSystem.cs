using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    [SerializeField]
    [Tooltip("List of all the Vegetable Spawn Points")]
    private List<VegetableSpawner> spawnPoints = new List<VegetableSpawner>();

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject apple;
    public GameObject Apple { get => apple; private set => apple = value; }

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject broccoli;
    public GameObject Broccoli { get => broccoli; private set => broccoli = value; }

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject carrot;
    public GameObject Carrot { get => carrot; private set => carrot = value; }

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject lettuce;
    public GameObject Lettuce { get => lettuce; private set => lettuce = value; }

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject olive;
    public GameObject Olive { get => olive; private set => olive = value; }

    [SerializeField]
    [Tooltip("Apple Prefab to Spawn")]
    private GameObject peas;
    public GameObject Peas { get => peas; private set => peas = value; }



    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Count <= 0)
        {
            Debug.LogError("Error: No Spawn Points Referenced!");
            return;
        }
        if (!apple.GetComponent<Vegetable>() &&
            !broccoli.GetComponent<Vegetable>() &&
            !carrot.GetComponent<Vegetable>() &&
            !lettuce.GetComponent<Vegetable>() &&
            !olive.GetComponent<Vegetable>() &&
            !peas.GetComponent<Vegetable>())
        {
            Debug.LogError("One of the referenced Vegetable Prefabs is incorrect");
            return;
        }

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(1f);

        foreach (VegetableSpawner vegSpawner in spawnPoints)
        {
            vegSpawner.TrySpawnVegetable();
        }

        GetComponent<InventoryManager>().PrintInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(StartSpawning());
    }
}
