using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UITagPanel : MonoBehaviour
{
    [SerializeField]
    public List<Toggle> _Tags = new List<Toggle>();

    [SerializeField]
    public List<GameObject> _TagPanels = new List<GameObject>();

    public ToggleGroup _ToggleGroup;

    public delegate void ToggleOnCallBack(int page);
    public ToggleOnCallBack _ToggleOnCallBack;

    public void OnToggleOn(int page)
    {
        for (int i = 0; i < _TagPanels.Count; ++i)
        {
            if (i == page)
            {
                _TagPanels[i].SetActive(true);
            }
            else
            {
                _TagPanels[i].SetActive(false);
            }
        }

        if (_ToggleOnCallBack != null)
        {
            _ToggleOnCallBack(page);
        }
    }

    public void ShowPage(int page)
    {
        if (_Tags.Count > page && page >= 0)
        {
            _Tags[page].isOn = true;
        }
    }
}
