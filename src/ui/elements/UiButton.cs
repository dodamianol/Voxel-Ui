using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiButton : UiElement, Clickable {
    public Color HighlightColor { get; set; } = Color.LightGray;
    public Action? OnClick { get; set; }
    public void OnClickReceived() => Console.WriteLine("Button was clicked!");//OnClick?.Invoke();
    private UiText? _textElement;
    public UiText? Text {
        get => _textElement;
        set {
            if (_textElement != null) {
                Children.Remove(_textElement);
            }

            _textElement = value;

            if (_textElement != null) {
                AddChild(_textElement);
            }
        }
    }
}
