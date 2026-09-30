using Raylib_cs;

namespace VoxelUiDemo.ui.elements.basic;

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