using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    public string productName;
    public int price;
    public float duration;
    public TMPro.TMP_Text itemText;
    public Button button;

    public GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        itemText.text = productName + " - " + price + " coins" + "(" + duration + " secs)";
        button.onClick.AddListener(delegate { gm.GetComponent<Economy>().Buy(productName, duration, price) ; });
    }
}
