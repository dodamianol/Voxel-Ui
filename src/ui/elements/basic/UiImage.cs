using Raylib_cs;

namespace VoxelUiDemo.ui.elements.basic;

public class UiImage : UiElement {
    private readonly ImageRenderer _imageRenderer;

    public Texture2D Texture { get => _imageRenderer.Texture; set => _imageRenderer.Texture = value; }
    public Color Tint { get => _imageRenderer.Tint; set => _imageRenderer.Tint = value; }

    public UiImage() {
        _imageRenderer = new ImageRenderer();
        Renderer = _imageRenderer; 
    }
    
    public UiImage(Texture2D texture) : this() {
        Texture = texture;
    }
}
