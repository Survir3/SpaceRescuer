using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

[RequireComponent(typeof(Dropdown))]
public class ViewCountries : MonoBehaviour
{
    [SerializeField] private CountriesData _countries;

    private Dropdown _dropdown;
    private List<OptionData> _optionDataList=new List<OptionData>();

    private void Awake()
    {
        _dropdown= GetComponent<Dropdown>();

        foreach (var country in _countries.Value)
        {
            //_optionDataList.Add(new OptionData(country.KeyLanguage.ToString(),country.Flag));
        }

        _dropdown.AddOptions(_optionDataList);
    }
}
