using UnityEngine;
using System.Collections;

public class StickyBackground : MonoBehaviour {

    public GameObject subject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= (transform.position - subject.transform.position) * 0.2f * Time.deltaTime;
	}
}
