using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RippleIcon : MonoBehaviour
{
    private List<Image> _icons=new List<Image>();
    private int minCountIcons;

    private void Update()
    {
        if (_icons.Count < minCountIcons)
            return;

        Pulsate();
    }

    private void SetStartAlha()
    {
        Color color;
        float maxAlha = 225f;
        float minAlha = 0f;
        float normalizedStepAlha = (maxAlha/_icons.Count)/maxAlha;

        color = _icons[0].color;
        color.a = minAlha;
        _icons[0].color = color;

        for (int i = 1; i < _icons.Count; i++)
        {
            color = _icons[i].color;
            color.a = normalizedStepAlha;
            _icons[i].color = color;

            normalizedStepAlha += normalizedStepAlha;
        }
    }

    public void AddIcon(Image icon)
    {
        _icons.Add(icon);
    }

    private void Pulsate()
    {    
        Color color=new Color();

        for (int i = 0; i < _icons.Count; i++)
        {
            if(i==0)
                color = _icons[i].color;

            if (i < _icons.Count - 1)
                _icons[i].color = _icons[i + 1].color;
            else
                _icons[i].color = color;
        }
    }
}
