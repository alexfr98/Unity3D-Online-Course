using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private Text _actionText;

    [SerializeField]
    private GameObject _inventoryCoin;


    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void actionText(string text)
    {
        _actionText.text = text;
    }
    public void actionDone()
    {
        _actionText.text = "";
    }


    public void CollectedCoin()
    {
        _inventoryCoin.SetActive(true);
    }

    public void giveCoin()
    {
        _inventoryCoin.SetActive(false);
    }


}
