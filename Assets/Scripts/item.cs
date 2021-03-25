using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
class template for store items
*/
public class item : MonoBehaviour
{
    public string productName;
    public int price;
    public float duration;
    public TMPro.TMP_Text itemText;
    public Button button;

    public GameObject gm;

    // add details to the buttons, and add the function on click 
    void Start()
    {
        itemText.text = productName + " - " + price + " coins" + "(" + duration + " secs)";
        button.onClick.AddListener(delegate { gm.GetComponent<Economy>().Buy(productName, duration, price) ; });
    }
}
