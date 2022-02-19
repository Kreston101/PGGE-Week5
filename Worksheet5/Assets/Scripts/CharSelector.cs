using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelector : MonoBehaviour
{
    //int varible to hold the number
    public int chosenCharNum;
    //the buttons
    [SerializeField]
    Button ButtonSelectChar1;
    [SerializeField]
    Button ButtonSelectChar2;
    [SerializeField]
    Button ButtonSelectRand;

    //add the listeners to the button
    void Start()
    {
        ButtonSelectChar1.onClick.AddListener(ChooseCharOne);
        ButtonSelectChar2.onClick.AddListener(ChooseCharTwo);
        ButtonSelectRand.onClick.AddListener(ChooseRandChar);
    }

    //when a button is selected, set chosenCharNum to
    //a designated number, then send the number to the GameApp via
    //a function. The GameApp is used as it persists through the scenes
    public void ChooseCharOne()
    {
        chosenCharNum = 0;
        FindObjectOfType<GameApp>().passInt(chosenCharNum);
    }
    public void ChooseCharTwo()
    {
        chosenCharNum = 1;
        FindObjectOfType<GameApp>().passInt(chosenCharNum);
    }
    public void ChooseRandChar()
    {
        chosenCharNum = Random.Range(0, 2);
        FindObjectOfType<GameApp>().passInt(chosenCharNum);
    }
}
