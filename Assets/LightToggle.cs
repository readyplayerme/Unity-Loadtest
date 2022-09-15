using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightToggle : MonoBehaviour
{
    [SerializeField] private GameObject DirectionalLight;

    private Button btnToggle;
    // Start is called before the first frame update
    void Start()
    {
        btnToggle = GetComponent<Button>();
        btnToggle.onClick.AddListener(OnToggleClick);
    }

    private void OnToggleClick()
    {
        DirectionalLight.SetActive(!DirectionalLight.activeSelf);
    }
}
