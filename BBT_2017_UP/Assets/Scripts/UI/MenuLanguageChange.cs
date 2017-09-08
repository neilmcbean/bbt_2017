using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuLanguageChange : MonoBehaviour {

    public GameObject PageManagerRef;
    public Dropdown dropdownMenu;

    // Use this for initialization
    void Start() {
        //TODO:Create a system which will allow players to change languages on the fly
        dropdownMenu = GetComponent<Dropdown>();
        for (int languageCount = 0; languageCount < DataManager.languageManager.Length; languageCount++)
        {
            dropdownMenu.options.Add(new Dropdown.OptionData() { text = DataManager.languageManager[languageCount] });
            //dropdownMenu.options[languageCount].text = "test";
            // dropdownMenu.value = languageCount;
            //dropdownMenu.value = languageCount;
            //dropdownMenu.options.Add(new Dropdown.OptionData(dropdownMenu.options[languageCount].text));
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public void LanguageUpdate()
    {
        PageManagerRef.GetComponent<PageManager>().ChangeLanguage(GetComponentInChildren<Text>().text);
        //Debug.Log(GetComponentInChildren<Text>().text);
    }
}
