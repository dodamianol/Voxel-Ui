using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui.components;

namespace VoxelUiDemo.ui.elements.basic;

public abstract class UiElement {
    public UiElement? Parent { get; private set; }
    public List<UiElement> Children { get; } = [];
    
    public LayoutComponent Layout { get; set; }
    public RenderComponent? Renderer { get; set; }
    public InteractableComponent? Interactable { get; set; }
    
    public bool IsVisible { get; init; } = true;
    
    // public bool IsHovered;
    // public bool BlocksInput { get; set; } = true;   
    
    public void AddChild(UiElement child) {
        child.Parent = this;
        Children.Add(child);
    }
    
    public virtual bool ContainsPoint(Vector2 point) {
        // TODO: If children can be outside of the parents bounds: If the the point is not contained, children have to be checked
        return Raylib.CheckCollisionPointRec(point, Layout.FinalTransform.ToRectangle());
    }

    public virtual void Update(Vector2 mousePos) {
        // IsHovered = ContainsPoint(mousePos);
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
        if (!IsVisible) return; // TODO: Maybe unnecessary since layout is independent of rendering?

        var currentLayout = Layout;
        currentLayout.Calculate(Parent?.Layout.FinalTransform, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        Layout = currentLayout;
        
        ArrangeChildren();
        
        foreach (var child in Children) {
            child.CalculateLayout();
        }
    }
    
    protected virtual void ArrangeChildren() { }
    
    internal void AssignTransform(Transform transform, AnchorType? anchor = null) {
        var currentLayout = Layout;
        currentLayout.Transform = transform;
        if (anchor.HasValue) currentLayout.Anchor = anchor.Value;
        Layout = currentLayout;
    }
}
