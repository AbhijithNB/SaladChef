using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VegetableName
{
    Apple,
    Broccoli,
    Carrot,
    Lettuce,
    Olive,
    Peas,
}

public class Vegetables : MonoBehaviour
{

    private InventoryManager inventoryManager;
    public InventoryManager InventoryManager
    {
        get => inventoryManager;
        set => inventoryManager = value;
    }

    public VegetableName vegName = VegetableName.Apple;

    public int points = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (points <= 0)
        {
            points = 1;
            Debug.LogWarning("Points for a Vegetable cannot be Less than or Equal to Zero");
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
