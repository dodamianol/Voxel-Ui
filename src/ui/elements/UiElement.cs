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

        var currentLayout = Layout;
        currentLayout.Calculate(Parent?.Layout.FinalTransform, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        Layout = currentLayout;

        ArrangeChildren();
        
        foreach (var child in Children) {
            child.CalculateLayout();
        }
    }
    
    protected virtual void ArrangeChildren() { }
    
    internal void AssignTransform(Transform transform) {
        var currentLayout = Layout;
        currentLayout.Transform = transform;
        Layout = currentLayout;
    }
}
