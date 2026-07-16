using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements;

public class UiButton : UiElement {
    public Color HighlightColor { get; set; } = Color.LightGray;
    public Action? OnClick { get; set; }
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
    
    protected override void RenderSelf(List<UiRenderCommand> commands) {
        var renderColor = IsHovered ? HighlightColor : BackgroundColor;

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y),
            Size = new Vector2(AbsoluteBounds.Width, AbsoluteBounds.Height),
            Color = renderColor
        });
    }
}