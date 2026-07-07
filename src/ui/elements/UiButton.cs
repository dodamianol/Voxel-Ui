using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui;

public class UiButton : UiElement {
    public string Text { get; set; }
    public Action OnClick { get; set; }
    
    public override void CollectRenderCommands(List<UiRenderCommand> commands) {
        var renderColor = IsHovered ? Color.LightGray : BackgroundColor;

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y),
            Size = new Vector2(AbsoluteBounds.Width, AbsoluteBounds.Height),
            Color = renderColor
        });

        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawText,
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y) + new Vector2(10, 15),
            Text = Text,
            Color = Color.Black
        });
        
        foreach (var child in Children) {
            child.CollectRenderCommands(commands); 
        }
    }
}