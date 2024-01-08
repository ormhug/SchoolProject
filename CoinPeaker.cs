using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPeaker : MonoBehaviour
{
    private float coins = 0;
    public TMP_Text coinsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            coins++;
            coinsText.text = coins.ToString();
            Destroy(collision.gameObject);
        }
    }
}
