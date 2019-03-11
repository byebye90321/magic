using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectLock : MonoBehaviour {

    public Button character;
    public GameObject clock;
    public string staticValueName;
    private int staticValueInt;

    public void Start()
    {
        staticValueInt = PlayerPrefs.GetInt(staticValueName);

        if (staticValueInt == 1)
        {
            character.interactable = true;
            clock.SetActive(false);
        }
    }
}
