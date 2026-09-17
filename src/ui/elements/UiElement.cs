using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace VoxelUiDemo.ui.elements;

public abstract class UiElement {
    public UiElement? Parent { get; private set; }
    public List<UiElement> Children { get; } = [];
    
    public LayoutComponent Layout { get; set; }
    public RenderComponent? Renderer { get; set; }
    
    public bool IsVisible { get; init; } = true;
    
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

        Renderer?.CollectRenderCommands(commands, Layout);

        foreach (var child in Children) {
            child.CollectRenderCommands(commands);
        }
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
