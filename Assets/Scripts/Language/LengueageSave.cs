using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LengueageSave : MonoBehaviour
{

    public void SaveLenguege()
    {
        LanguagueManager._instanced.SaveLenguage();
    }

    public void LoadLenguege()
    {
        LanguagueManager._instanced.LoadLenguage();
    }





}
