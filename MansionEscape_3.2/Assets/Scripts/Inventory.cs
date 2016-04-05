using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	List<Item> inventory;
    public int capacity;

    //GUI Stuff
    private float blockSize = 35;
    public RectTransform display;
    public Image openSlot;
    public Image filledSlot;

    // Use this for initialization
    void Start () {
		inventory = new List<Item> ();
        drawSlots();
	}
	
	public bool addItem(Item item)
	{
        bool added = false;
        if(item.GetComponent<Backpack>() != null)
        {
            capacity += 8;
        }

        if(item.stackable)
        {
            Item stackable = inventory.Where(i => i.type == item.type).First();
            if (stackable != null)
            {
                stackable.addStack();
                added = true;
            }
        }

        if(getSize() < capacity && !added)
        {
            inventory.Add(item);
            item = null;
            added = true;
        }

        drawSlots();

        return added;
	}

	//will add error handling later
	//index out of range exception possibility
	public void removeItem(int selector)
	{
		inventory.RemoveAt(selector);
	}

    public void setCapacity(int capacity)
    {
        this.capacity = capacity;
        drawSlots();
    }

    private void drawSlots()
    {
        var drawnSlots = new List<GameObject>();
        foreach (Transform child in display) drawnSlots.Add(child.gameObject);
        drawnSlots.ForEach(slot => Destroy(slot));

        inventory.Sort((a, b) => a.size - b.size);
        int i = 0;
        foreach (Item item in inventory)
        {
            Image slot = Instantiate(filledSlot);
            slot.transform.SetParent(display, false);
            slot.transform.position = new Vector3(display.position.x + (i / 2) * blockSize, display.position.y - (i % 2) * blockSize);

            Image icon = Instantiate(filledSlot);
            icon.sprite = item.GetComponent<SpriteRenderer>().sprite;
            icon.transform.SetParent(slot.transform);
            icon.transform.position = slot.transform.position;

            i += item.size;
        }

        for (; i < capacity; i++)
        {
            Image slot = Instantiate(openSlot);
            slot.transform.SetParent(display, false);
            slot.transform.position = new Vector3(display.position.x + (i / 2) * blockSize, display.position.y - (i % 2) * blockSize);
        }
    }

    public int getSize()
    {
        return inventory.Aggregate(0, (current, item) => current + item.size);
    }
}
