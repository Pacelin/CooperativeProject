using UnityEngine;

public class InteractableExample : Interactable
{
    [SerializeField] private Material _sphereMaterial;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _interactColor;

    private void Awake() =>
        SetMaterialColor(_defaultColor);

    public override void OnInteractorEnter(Interactor interactor)
    {
        base.OnInteractorEnter(interactor);
        SetMaterialColor(GetLighter(_defaultColor));
    }

    public override void OnInteractorExit(Interactor interactor)
    {
        base.OnInteractorExit(interactor);
        SetMaterialColor(_defaultColor);
    }

    public override void OnInteractDown(Interactor interactor) =>
        SetMaterialColor(GetLighter(_interactColor));

    public override void OnInteractUp(Interactor interactor)  =>
        SetMaterialColor(_interactColor);

    public override void OnTryInteract(Interactor interactor) { }


    private Color GetLighter(Color color) =>
        Color.Lerp(color, Color.white, 0.3f);    
    private void SetMaterialColor(Color color) =>
        _sphereMaterial.color = color;
}