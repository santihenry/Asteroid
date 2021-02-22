using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.IO;

public enum Language
{
    eng,
    spa
}


public class LanguagueManager : MonoBehaviour
{
    public Language selectedLanguage;
    public Dictionary<Language, Dictionary<string, string>> LanguageManager;
    string externalUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vS6H37q-rOVn6OKWtVcaZ6Lx4iM3-6q61mRI-x_mdqDSKkeYHwz_Ib0iSTY5mTiEnrEXF6oiKUT79if/pub?output=csv";
    public event Action OnUpdate = delegate { };
    public static LanguagueManager _instanced;
    bool canInitiate = false;


    void Awake()
    {
        DontDestroyOnLoad(this);
        if (_instanced == null) 
            _instanced = this;
        else
            Destroy(gameObject);       
        StartCoroutine(DownloadCSV(externalUrl));
    }
    public string GetTranslate(string _id)
    {
        if (!LanguageManager[selectedLanguage].ContainsKey(_id))
            return "Error 404: Not Found";
        else
            return LanguageManager[selectedLanguage][_id];
    }
    public IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        LanguageManager = LanguageU.loadCodexFromString("www", www.downloadHandler.text);
        canInitiate = true;
        OnUpdate();
    }

    private void Update()
    {
        if (canInitiate)
            OnUpdate();
    }
    public void ChangeLanguage()
    {
        if (selectedLanguage == Language.eng)
            selectedLanguage = Language.spa;
        else
            selectedLanguage = Language.eng;

    }


    public void SaveLenguage()
    {
        SaveManager.SaveLenguage(this);
    }


    public void LoadLenguage()
    {
        LenguageData lenguegeData = SaveManager.LoadLenguage();
        selectedLanguage = lenguegeData.lenguage;
    }


}
