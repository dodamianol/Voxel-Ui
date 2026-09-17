using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiPanel : UiElement {
    public Color BackgroundColor {
        get => (Renderer as RectRenderer)?.Color ?? Color.Blank;
        set {
            if (Renderer is RectRenderer rectRenderer) rectRenderer.Color = value;
            else Renderer = new RectRenderer(value);
        }
    }

    public UiPanel() {
        Renderer = new RectRenderer(Color.Gray);
    }

    public UiPanel(Color color) {
        Renderer = new RectRenderer(color);
    }
}