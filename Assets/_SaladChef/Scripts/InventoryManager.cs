using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    // To keep track of number of Vegetables availabe in the inventory
    private Dictionary<VegetableName, int> vegetableCount = new Dictionary<VegetableName, int>();


    // Start is called before the first frame update
    void Start()
    {
        //ClearInventory();
        // Initialize Inventory
        InitInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitInventory()
    {
        // Initialize the Inventory with 0 entries for all vegetables
        vegetableCount.Add(VegetableName.Apple, 0);
        vegetableCount.Add(VegetableName.Broccoli, 0);
        vegetableCount.Add(VegetableName.Carrot, 0);
        vegetableCount.Add(VegetableName.Lettuce, 0);
        vegetableCount.Add(VegetableName.Olive, 0);
        vegetableCount.Add(VegetableName.Peas, 0);
    }

    private void ClearInventory()
    {
        // Clear all entries of all Vegetables
        vegetableCount.Clear();
    }


    /// <summary>
    /// Adds an entry of the Vegetable to the inventory
    /// </summary>
    /// <param name="vegName"></param>
    public void AddVegetable(VegetableName vegName)
    {
        // Add one more entry of a particular vegetable
        vegetableCount[vegName]++;
    }


    /// <summary>
    /// Checks and removes a Vegetable from the inventory
    /// </summary>
    /// <param name="vegName"></param>
    public void RemoveVegetable(VegetableName vegName)
    {
        // Check if any entries of this Vegetable exists
        if (vegetableCount[vegName] > 0)
        {
            // Found more than 0 entries
            // Reduce one from the inventory
            vegetableCount[vegName]--;
        }
        else
        {
            // No record of the Vegetable was found in the inventory
            Debug.LogError("Error: No records of " + vegName + " in the Inventory to remove");
        }
    }


    /// <summary>
    /// Returns the count of the Vegetable in the inventory
    /// </summary>
    /// <param name="vegName"></param>
    /// <returns></returns>
    public int GetVegetableCount(VegetableName vegName)
    {
        // Return the number of entries of a particular Vegetable
        return vegetableCount[vegName];
    }

    public void PrintInventory()
    {
        Debug.Log("Apple: " + vegetableCount[VegetableName.Apple] +
            " Broccoli: " + vegetableCount[VegetableName.Broccoli] +
            " Carrot: " + vegetableCount[VegetableName.Carrot] +
            " Lettuce: " + vegetableCount[VegetableName.Lettuce] +
            " Olive: " + vegetableCount[VegetableName.Olive] +
            " Peas: " + vegetableCount[VegetableName.Peas]);
    }
}
