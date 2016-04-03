using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{

    public int size = 1;
    public bool stackable = false;
    private int count = 1;
    public string type = "";

    public static GameObject colliding = null;
    public static bool guiEnable = true;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			colliding = gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			colliding = null;
		}
	}

	void OnGUI()
	{
		GUI.contentColor = Color.white;
		if (guiEnable == true) 
		{
			GUI.Label (new Rect (this.transform.position.x, this.transform.position.y + 1, 25, 10), "Press E to pick up ???");
		}
	}

    public int getCount()
    {
        return count;
    }

    public void addStack()
    {
        count++;
    }

    public void removeStack()
    {
        if(count > 0)
        {
            count--;
        }
    }
}
