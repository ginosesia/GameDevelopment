using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropDown : MonoBehaviour
{
    private Dropdown dropdown;
    private string playerPref = "Color";

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            HandleChanges(dropdown.value);
            PlayerPrefs.Save();
        }));

    }

    private void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(playerPref, 0);
    }


    public void HandleChanges(int val)
    {

        switch (val)
        {
            case 0:
                PlayerPrefs.SetInt(playerPref, 0);
                break;
            case 1:
                PlayerPrefs.SetInt(playerPref, 1);
                break;
            case 2:
                PlayerPrefs.SetInt(playerPref, 2);
                break;
            case 3:
                PlayerPrefs.SetInt(playerPref, 3);
                break;
            case 4:
                PlayerPrefs.SetInt(playerPref, 4);
                break;
            case 5:
                PlayerPrefs.SetInt(playerPref, 5);
                break;
        }

    }

}
