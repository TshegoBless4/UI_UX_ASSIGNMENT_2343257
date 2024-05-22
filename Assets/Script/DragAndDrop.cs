using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    public Text moneyText;
    public float StartingMoney { get; private set; } = 100f;
    private float money;
    

    public GameObject firstObjectToToggle; // Reference to the first object you want to toggle
    public GameObject SecondObjectToToggle; // Reference to the second object you want to toggle

    private void Start()
    {
        money = StartingMoney;
        UpdateMoneyText();

        StartCoroutine(ToggleObjects());
    }

    private IEnumerator ToggleObjects()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        // Activate the first object after waiting
        firstObjectToToggle.SetActive(true);

        yield return new WaitForSeconds(3f); // Wait for additional 3 seconds

        // Activate the second object after waiting
        SecondObjectToToggle.SetActive(true);
    }

    // Existing methods for buying, selling, refunding, etc.

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + money.ToString("F2");
        }
    }

    // Methods for buying, selling, refunding items
    public void RefundItem(float price)
    {
        money += price;
        Debug.Log($"Item refunded for {price}! $ {money}");
        UpdateMoneyText(); // Update UI text after refunding item
    }

    public void BuyItem(float price)
    {
        if (money >= price)
        {
            money -= price;
            Debug.Log($"Item bought for {price}! Money left: {money}");
            UpdateMoneyText(); // Update UI text after buying item
        }
        else
        {
            Debug.Log("Not enough money to buy the item!");
        }
    }

    public void SellItem(float price)
    {
        money += price;
        Debug.Log($"Item sold for {price}! $ {money}");
        UpdateMoneyText(); // Update UI text after selling item
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
