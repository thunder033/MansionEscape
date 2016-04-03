using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<string> inventory;
    public int size;
    public RectTransform display;
    public Image openSlot;

    private float blockSize = 35;

	// Use this for initialization
	void Start () {
		inventory = new List<string> ();
        drawSlots();
	}
	
	public void addItem(string item)
	{
        if(inventory.Count < size)
        {
            inventory.Add(item);
            item = null;
        }
	}

	//will add error handling later
	//index out of range exception possibility
	public void removeItem(int selector)
	{
		inventory.RemoveAt(selector);
	}

    public void setSize(int size)
    {
        this.size = size;
        drawSlots();
    }

    private void drawSlots()
    {
        for (int i = inventory.Count; i < size; i++)
        {
            Image slot = Instantiate(openSlot);
            slot.transform.SetParent(display, false);
            slot.transform.position = new Vector3(display.position.x + (i / 2) * blockSize, display.position.y - (i % 2) * blockSize);
        }
    }
}
