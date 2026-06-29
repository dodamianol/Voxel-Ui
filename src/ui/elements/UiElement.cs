using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui;

public abstract class UiElement {
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Color BackgroundColor { get; set; } = Color.Gray;
    public List<UiElement> Children { get; set; } = [];

    public virtual void Update(Vector2 mousePos, bool mouseClicked) {
        foreach (var child in Children) {
            child.Update(mousePos, mouseClicked);
        }
    }

    public virtual void CollectRenderCommands(List<UiRenderCommand> commands) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = this.Position,
            Size = this.Size,
            Color = this.BackgroundColor
        });

        foreach (var child in Children) {
            child.CollectRenderCommands(commands);
        }
    }
}