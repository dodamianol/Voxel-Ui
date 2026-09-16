using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.voxel_engine;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public abstract class UiElement {
    public UiElement? Parent { get; private set; }
    public List<UiElement> Children { get; } = [];
    
    public LayoutComponent Layout { get; set; }
    public bool IsVisible { get; init; } = true;

    public Color BackgroundColor { get; set; } = Color.Gray;
    
    public bool IsHovered;
    public bool BlocksInput { get; set; } = true;   
    
    public void AddChild(UiElement child) {
        child.Parent = this;
        Children.Add(child);
    }
    
    public virtual bool ContainsPoint(Vector2 point) {
        return Raylib.CheckCollisionPointRec(point, Layout.FinalTransform.ToRectangle());
    }

    public virtual void Update(Vector2 mousePos) {
        IsHovered = ContainsPoint(mousePos);
        //_isHovered = mousePos.X >= AbsoluteBounds.X && mousePos.X <= AbsoluteBounds.X + AbsoluteBounds.Width && mousePos.Y >= AbsoluteBounds.Y && mousePos.Y <= AbsoluteBounds.Y + AbsoluteBounds.Height;
        
        foreach (var child in Children) {
            child.Update(mousePos);
        }
    }

    public void CollectRenderCommands(List<UiRenderCommand> commands) {
        if (!IsVisible) return;
        
        RenderSelf(commands);
        
        foreach (var child in Children) {
            child.CollectRenderCommands(commands);
        }
    }

    protected virtual void RenderSelf(List<UiRenderCommand> commands) {
        commands.Add(new UiRenderCommand {
            Type = CommandType.DrawRect,
            Position = Layout.FinalTransform.Offset,
            Size = Layout.FinalTransform.Size,
            Color = BackgroundColor
        });
    }

    public void CalculateLayout() {
        if (!IsVisible) return;

        CalculateOwnLayout();

        foreach (var child in Children) {
            child.CalculateLayout();
        }
    }

    protected virtual void CalculateOwnLayout() {
        var parentPos = Parent?.Layout.FinalTransform.Offset ?? Vector2I.Zero;
        var parentSize = Parent?.Layout.FinalTransform.Size ?? new Vector2I(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        var parentScale = Parent?.Layout.FinalTransform.Scale ?? Vector2.One;
        var finalScale = parentScale * Layout.Transform.Scale;        
    
        var scaledSize = Vector2I.Round(Layout.Transform.Size * finalScale);
        var scaledOffset = Vector2I.Round(Layout.Transform.Offset * parentScale);
    
        var factor = Layout.Anchor.GetFactor();

        var anchorPoint = parentPos + Vector2I.Round((parentSize - scaledSize) * factor);
        var finalPos = anchorPoint + scaledOffset;
    
        var currentLayout = Layout;
        currentLayout.FinalTransform = new Transform { Offset = finalPos, Size = scaledSize, Scale = finalScale };
        Layout = currentLayout;
    }
       
    internal void AssignTransform(Transform transform) {
        var parentScale = Parent?.Layout.FinalTransform.Scale ?? Vector2.One;
        
        var currentLayout = Layout;
        currentLayout.Transform = transform.WithScale(parentScale * transform.Scale);
        Layout = currentLayout;
    }
}
