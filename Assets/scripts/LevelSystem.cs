using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public Button[] buttons;

    void Start()
    {
        int maxLevel = PlayerPrefs.GetInt("maxLevel", 2); // 2 here is default value (will only enable btn 1)

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 2 > maxLevel)
                buttons[i].interactable = false;
        }
    }

}
