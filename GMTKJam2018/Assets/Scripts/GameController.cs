using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour {
    public int health;
    public int money;
    public GameObject slider;
    public GameObject dragging;
    public GameObject baseModule, machineGunModule;
    public GameObject moneyText;
    // Use this for initialization
    void Start () {
        slider.GetComponent<Slider>().value = health;
        slider.GetComponent<Slider>().maxValue = health;
        moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage()
    {
        health--;
        slider.GetComponent<Slider>().value = health;

        if(health <= 0)
        {
            slider.transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log("Dead");
        }
    }
    public bool TakeMoney(int cost)
    {
        if(cost > money)
        { return false; }
        else
        {
            money -= cost;
            moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
            return true;
        }
    }
    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
    }

    public void SelectMachineGun()
    {
        dragging.GetComponent<Dragging>().DragThis(machineGunModule);
    }
    public void SelectModule()
    {
        dragging.GetComponent<Dragging>().DragThis(baseModule);
    }
}
