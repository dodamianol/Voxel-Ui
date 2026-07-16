using System.Net.Mime;
using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo.ui.elements {
    public class Slider : UiElement {
        public Color HighlightColor { get; set; } = Color.LightGray;
        public Action? OnClick { get; set; }
        
        protected override void RenderSelf(List<UiRenderCommand> commands) {
            var renderColor = IsHovered ? HighlightColor : BackgroundColor;
            
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawRect,
                Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y),
                Size = new Vector2(AbsoluteBounds.Width, AbsoluteBounds.Height),
                Color = renderColor
            });

            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawText,
                Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y) + new Vector2(10, 15),
                Size = new  Vector2(100, 15),
                Color = Color.Black
            });
        }
    }
}