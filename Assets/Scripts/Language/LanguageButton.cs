using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{

    public Sprite[] languages= new Sprite[2];
    Image image;
    Button button;
    LanguagueManager manager;

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        manager = FindObjectOfType<LanguagueManager>();
        button.onClick.AddListener(manager.ChangeLanguage);
    }


    void Update()
    {
        FindObjectOfType<LanguageButton>();
        if (manager.selectedLanguage == Language.spa)
            image.sprite = languages[1];
        else
            image.sprite = languages[0];



    }
}
