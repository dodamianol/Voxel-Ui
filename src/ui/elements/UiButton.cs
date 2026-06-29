using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui;

public class UiButton : UiElement {
    public string Text { get; set; }
    public Action OnClick { get; set; }
    private bool _isHovered;

    public override void Update(Vector2 mousePos, bool mouseClicked) {
        _isHovered = mousePos.X >= Position.X && mousePos.X <= Position.X + Size.X &&
                     mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + Size.Y;

        if (_isHovered && mouseClicked) {
            OnClick?.Invoke();
        }

        base.Update(mousePos, mouseClicked);
    }

    public override void CollectRenderCommands(List<UiRenderCommand> commands) {
        var renderColor = _isHovered ? Color.LightGray : BackgroundColor;

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = this.Position,
            Size = this.Size,
            Color = renderColor
        });

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawText,
            Position = this.Position + new Vector2(10, 15),
            Text = this.Text,
            Color = Color.Black
        });

        foreach (var child in Children) {
            child.CollectRenderCommands(commands);
        }
    }
}