using UnityEngine;

public class InteractableExample : Interactable
{
    [SerializeField] private Material _sphereMaterial;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _interactColor;

    private void Awake() =>
        SetMaterialColor(_defaultColor);

    public override void OnInteractorEnter() =>
        SetMaterialColor(GetLighter(_defaultColor));

    public override void OnInteractDown() =>
        SetMaterialColor(GetLighter(_interactColor));

    public override void OnInteractUp()  =>
        SetMaterialColor(_interactColor);

    public override void OnInteractorExit() =>
        SetMaterialColor(_defaultColor);


    private Color GetLighter(Color color) =>
        Color.Lerp(color, Color.white, 0.3f);    
    private void SetMaterialColor(Color color) =>
        _sphereMaterial.color = color;

}