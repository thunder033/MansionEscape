using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponStatusGUI : MonoBehaviour {

    public characterController2D target;
   
    Weapon weapon = null;
    public Image weaponIcon;
    public Text damageText;

	// Use this for initialization
	void Start () {
        if(weaponIcon)
        {
            weaponIcon.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

	    if(target.weapon != weapon && target.weapon != null)
        {
            weaponIcon.gameObject.SetActive(true);
            weaponIcon.sprite = target.weapon.GetComponent<SpriteRenderer>().sprite;
            damageText.text = target.weapon.damage.ToString();
            weapon = target.weapon;
        }
        else if(target.weapon == null)
        {
            weaponIcon.gameObject.SetActive(false);
            weaponIcon.sprite = null;
            damageText.text = "0";
        }
	}
}
