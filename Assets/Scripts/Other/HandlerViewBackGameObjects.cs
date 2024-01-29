using UnityEngine;

public class HandlerViewBackGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private GameObject _activeGameObject;

    public void OnClickOpenPanelButton()
    {
        if (_gameObject.activeSelf)
        {
            _activeGameObject = _gameObject;
        }
        else
        {
            _activeGameObject = null;
        }

        if (_activeGameObject != null)
            _activeGameObject.SetActive(false);
    }
    public void OnClickClosePanelButton()
    {
        if (_activeGameObject != null)
        {
            _activeGameObject.SetActive(true);
        }
    }
}
