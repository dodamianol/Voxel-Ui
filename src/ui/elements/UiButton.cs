using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.components;
using VoxelUiDemo.ui.elements.basic;

namespace VoxelUiDemo.ui.elements;

public class UiButton : UiElement {
    private readonly RectRenderer _rectRenderer = new(Color.Gray);
    private readonly InteractableComponent _interactable = new();

    public Color NormalColor { get; init; } = new(35, 35, 35, 255);
    public Color HighlightColor { get; init; } = new(45, 45, 45, 255);
    public Color PressedColor { get; init; } = new(65, 65, 65, 255);

    public Action<Vector2>? OnClick {
        get => _interactable.OnClick;
        set => _interactable.OnClick = value;
    }

    public UiText? Text {
        set {
            if (value == null) return;
            Children.RemoveAll(c => c is UiText);
            AddChild(value);
        }
    }

    public UiButton() {
        Renderer = _rectRenderer;
        Interactable = _interactable;
    }

    public override void Update(Vector2 mousePos) {
        base.Update(mousePos);

        if (_interactable == null) return;

        if (_interactable.IsPressed) {
            _rectRenderer.Color = PressedColor;
        } else if (_interactable.IsHovered) {
            _rectRenderer.Color = HighlightColor;
        } else {
            _rectRenderer.Color = NormalColor;
        }
    }
}