using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LenguageData 
{
    public Language lenguage;


    public LenguageData(LanguagueManager leng)
    {
        lenguage = leng.selectedLanguage;
    }

}
