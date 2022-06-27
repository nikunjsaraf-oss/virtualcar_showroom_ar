using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private List<Renderer> bodyColorRenderers;
    [SerializeField] private Material bodyColor1;
    [SerializeField] private Material bodyColor2;
    [SerializeField] private Button bodyColor1Button;
    [SerializeField] private Button bodyColor2Button;

    private void Awake()
    {
        bodyColor1Button.onClick.AddListener(SetBodyColor1);
        bodyColor2Button.onClick.AddListener(SetBodyColor2);
    }

    private void SetBodyColor1()
    {
        foreach (var bodyColorRenderer in bodyColorRenderers)
        {
            bodyColorRenderer.sharedMaterial = bodyColor1;
        }
    }
    
    private void SetBodyColor2()
    {
        foreach (var bodyColorRenderer in bodyColorRenderers)
        {
            bodyColorRenderer.sharedMaterial = bodyColor2;
        }
    }
}