using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    public string productName;
    public int price;
    public float duration;
    public Text itemText;

    // Start is called before the first frame update
    void Start()
    {
        itemText.text = productName + " - " + price + "(" + duration + ")";
    }
}
