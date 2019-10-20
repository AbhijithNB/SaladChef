using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Vegetable : Vegetables
{

    [SerializeField]
    [Tooltip("Lable UI Component of the Vegetable")]
    private Image imgLable = null;

    private TMP_Text txtVegName = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!imgLable)
        {
            if (GetComponentInChildren<Image>())
            {
                imgLable = GetComponentInChildren<Image>();
                Debug.LogWarning("Lable Image not referenced. Assigning first Image component found in children");
            }
            else
            {
                Debug.LogError("Lable Image not Referenced. Did not find Image component in children.");
            }
        }

        if (imgLable)
        {
            if (imgLable.GetComponentInChildren<TMP_Text>())
            {
                txtVegName = imgLable.GetComponentInChildren<TMP_Text>();
                txtVegName.text = vegName.ToString();
            }
            else
            {
                Debug.LogError("No TMP Text component found under Lable Image");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitVegetable()
    {
        RegisterVegetable();
    }

    private void RegisterVegetable()
    {
        if (InventoryManager)
        {
            InventoryManager.AddVegetable(vegName);
            Debug.Log("Vegetable Registered in the Inventory");
        }
        else
        {
            Debug.LogError("Inventory Manager is not referenced");
        }
    }

    private void UnregisterVegetable()
    {
        if (InventoryManager)
        {
            InventoryManager.RemoveVegetable(vegName);
        }
        else
        {
            Debug.LogError("Inventory Manager is not referenced");
        }
    }
}
