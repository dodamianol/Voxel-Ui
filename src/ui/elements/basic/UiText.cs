using Raylib_cs;

namespace VoxelUiDemo.ui.elements.basic;

public class UiText : UiElement {
    private readonly TextRenderer _textRenderer;

    public string Text { get => _textRenderer.Text; set => _textRenderer.Text = value; }
    public int FontSize { get => _textRenderer.FontSize; set => _textRenderer.FontSize = value; }
    public Color TextColor { get => _textRenderer.TextColor; set => _textRenderer.TextColor = value; }
    public TextAlignmentHorizontal AlignH { get => _textRenderer.AlignH; set => _textRenderer.AlignH = value; }
    public TextAlignmentVertical AlignV { get => _textRenderer.AlignV; set => _textRenderer.AlignV = value; }

    public UiText() {
        _textRenderer = new TextRenderer();
        Renderer = _textRenderer;
    }
    
    public UiText(string text) : this() {
        Text = text;
    }
    public UiText(string text, int fontSize) : this(text) {
        FontSize = fontSize;
    }
}