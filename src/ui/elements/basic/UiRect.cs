using Raylib_cs;
using VoxelUiDemo.ui.elements.basic;

namespace VoxelUiDemo.ui.elements;

public class UiRect : UiElement {
    private readonly RectRenderer _rectRenderer;

    public Color BackgroundColor {
        get => _rectRenderer.Color; 
        set => _rectRenderer.Color = value;
    }

    public UiRect() {
        _rectRenderer = new RectRenderer();
        Renderer = _rectRenderer;
    }

    public UiRect(Color color) : this() {
        BackgroundColor = color;
    }
}