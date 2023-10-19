using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Dropdown;

[CreateAssetMenu(fileName = "Flags")]
public class CountriesData: ScriptableObject
{
    [SerializeField] private List<Country> _countries;

    public IReadOnlyList<Country> Value => _countries;
     
    //public bool TryGetFlag(int index , out Sprite flag)
    //{
    //    flag = null;

    //    foreach (var country in _countries)
    //    {
    //        if (country.KeyLanguage == (KeyLanguage)index)
    //        {
    //            flag= country.Flag;
    //            return country.KeyLanguage == (KeyLanguage)index;
    //        }
    //    }

    //    return false;
    //}

    //public bool TryGetFlag(string lang , out Sprite flag)
    //{
    //    flag = null;

    //    foreach (var country in _countries)
    //    {
    //        if (country.KeyLanguage.ToString() == lang)
    //        {
    //            flag = country.Flag;
    //            return country.KeyLanguage.ToString() == lang;
    //        }
    //    }

    //    return false;
    //}

    //public bool TryGetLanguage(string lang, out string langauage)
    //{
    //    langauage = null;

    //    foreach (var country in _countries)
    //    {        
    //        if (country.KeyLanguage.ToString() == lang)
    //        {
    //            langauage = country.Language;
    //            return country.KeyLanguage.ToString() == lang;
    //        }
    //    }

    //    return false;
    //}

    //public bool TryGetLanguage(int index, out string langauage)
    //{
    //    langauage = null;

    //    foreach (var country in _countries)
    //    {
    //        if (country.KeyLanguage == (KeyLanguage)index)
    //        {
    //            langauage = country.Language;
    //            return country.KeyLanguage == (KeyLanguage)index;
    //        }
    //    }

    //    return false;
    //}

}


[Serializable]
public class Country
{
    [SerializeField] private KeyLanguage _keyLanguage;
    [SerializeField] private Sprite _flag;
    [SerializeField] private string _language;
    public OptionData OptionData => new OptionData(_language, _flag);
    public KeyLanguage KeyLanguage => _keyLanguage;
}
