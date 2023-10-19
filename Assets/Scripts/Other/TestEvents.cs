using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestEvents")]

public class TestEvents : ScriptableObject
{
    [SerializeField] private Dictionary<KeyLanguage, Tester> testers;


}

[Serializable]
public class Tester
{
    [SerializeField] private KeyLanguage keyLanguage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string language;

    public KeyLanguage KeyLanguage => keyLanguage;
    public Sprite Sprite => sprite;
    public string Language => language;

}
