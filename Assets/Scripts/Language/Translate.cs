using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translate : MonoBehaviour
{
    public string ID;
    public LanguagueManager manager;
    public TMP_Text myView;
    string myText = "";

    private void Start()
    {

        manager = FindObjectOfType<LanguagueManager>();
        if (manager == null)
        {
            return;
        }
        myText = myView.text;
        manager.OnUpdate += ChangeLang;
    }
    private void Update()
    {
        if (manager)
            FindObjectOfType<LanguagueManager>();

        

    }

    void ChangeLang()
    {
        if(myView!=null && manager != null) 
            myView.text = manager.GetTranslate(ID);
        
    }
}
