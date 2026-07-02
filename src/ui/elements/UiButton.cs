using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui;

public class UiButton : UiElement {
    public string Text { get; set; }
    public Action OnClick { get; set; }
    private bool _isHovered;

    public override void Update(Vector2 mousePos, bool mouseClicked) {
        _isHovered = mousePos.X >= AbsoluteBounds.X && mousePos.X <= AbsoluteBounds.X + AbsoluteBounds.Width &&
                     mousePos.Y >= AbsoluteBounds.Y && mousePos.Y <= AbsoluteBounds.Y + AbsoluteBounds.Height;

        if (_isHovered && mouseClicked) {
            OnClick?.Invoke();
        }

        base.Update(mousePos, mouseClicked);
    }

    public override void CollectRenderCommands(List<UiRenderCommand> commands) {
        var renderColor = _isHovered ? Color.LightGray : BackgroundColor;

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y),
            Size = new Vector2(AbsoluteBounds.Width, AbsoluteBounds.Height),
            Color = renderColor
        });

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawText,
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y) + new Vector2(10, 15),
            Text = this.Text,
            Color = Color.Black
        });
        
        Console.WriteLine(GetType() + " | Position is" + new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y));

        foreach (var child in Children) {
            child.CollectRenderCommands(commands);
        }
    }
}