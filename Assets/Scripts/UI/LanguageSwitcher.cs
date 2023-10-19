using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private CountriesData _countryData;
    [SerializeField] private SaverData _saverData;

    private Dropdown _dropdown;
    public Country _country;
    private void Awake()
    {
        _dropdown=GetComponent<Dropdown>();

        foreach (var country in _countryData.Value)
        {
            _dropdown.options.Add(country.OptionData); 
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
        SetStartCountry();
    }

    public void OnClickDropdown(int index)
    {
        Debug.Log("OnClickDropdown");

        if (index <= _countryData.Value.Count - 1)
        {
            SetLanguage(_dropdown.options[index].text);
            _country = _countryData.Value[index];
            _saverData.SaveData(_country.KeyLanguage.ToString());
        }
        else
            Debug.LogError("Country not found.");
    }

    private Country GetCountry(string lang, out int index)
    {
        int indexDefaultCountry = 0;
        index = indexDefaultCountry;

        for (int i = 0; i < _countryData.Value.Count; i++)
        {
            if (_countryData.Value[i].KeyLanguage.ToString()==lang)
            {
                index = i;
                return _countryData.Value[i];
            }
        }

        Country defaultCountry=_countryData.Value[indexDefaultCountry];
        Debug.LogAssertion("Country not found, set default: " + defaultCountry);
        return defaultCountry;
    }

    private void SetStartCountry()
    {     
        _country=GetCountry(_saverData.KeyLanguage, out int index);
        _dropdown.value = index;
    }

    private void SetLanguage(string valueOption)
    {
        LeanLocalization.SetCurrentLanguageAll(valueOption);
    }
}
