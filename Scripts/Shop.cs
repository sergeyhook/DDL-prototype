using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shop : MonoBehaviour
{
    public TMP_Text CostText;
    public GameObject[] Items;
    public float[] CostOfItems;
    public float AvailableMoney;
    private GameObject Item;
    private string PreviousTag;
    public int MaxItems;
    public float Cost;
    public int r;
    void Start()
    {
        r= Random.Range(0, Items.GetLength(0));
        Item = Instantiate(Items[r], this.transform.position + new Vector3(0, 1f, 0), transform.rotation);
        Item.GetComponent<Rigidbody>().isKinematic = true;
        PreviousTag = Item.tag;
        Item.tag = "empty";
        Cost = CostOfItems[r];
        CostText.text = Cost.ToString();
    }
    public void SellItem()
    {
        AvailableMoney -= Cost;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.tag = PreviousTag;
        MaxItems -= 1;
        if (MaxItems >= 1)
        {
            Item = Instantiate(Items[r], this.transform.position + new Vector3(0, 1f, 0), transform.rotation);
            Item.GetComponent<Rigidbody>().isKinematic = true;
            PreviousTag = Item.tag;
            Item.tag = "empty";
        }
    }
    void Update()
    {
        if (MaxItems <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
