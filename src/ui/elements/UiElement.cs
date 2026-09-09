using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.voxel_engine;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public abstract class UiElement {
    public UiElement? Parent { get; private set; }
    public List<UiElement> Children { get; } = [];
    
    public bool IsVisible { get; init; } = true;
    
    public AnchorType AnchorType = AnchorType.TopLeft;

    public Transform Transform { get; set; }
    public Transform FinalTransform { get; set; }

    public Color BackgroundColor { get; set; } = Color.Gray;
    
    public bool IsHovered;
    public bool BlocksInput { get; set; } = true;
    
    private bool IsLayoutManaged { get; set; } 

    public void AddChild(UiElement child) {
        child.Parent = this;
        Children.Add(child);
    }
    
    public virtual bool ContainsPoint(Vector2 point) {
        return Raylib.CheckCollisionPointRec(point, FinalTransform.ToRectangle());
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
            Position = FinalTransform.Offset,
            Size = FinalTransform.Size,
            Color = BackgroundColor
        });
    }

    public void CalculateLayout() {
        if (!IsVisible) return;

        if (!IsLayoutManaged) {
            CalculateOwnLayout();
        }

        foreach (var child in Children) {
            child.CalculateLayout();
        }
    }

    protected virtual void CalculateOwnLayout() {
        var parentPos = Parent?.FinalTransform.Offset ?? Vector2I.Zero;
        var parentSize = Parent?.FinalTransform.Size ?? new Vector2I(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        var parentScale = Parent?.FinalTransform.Scale ?? Vector2.One;
        var finalScale = parentScale * Transform.Scale;        
        var scaledSize = Vector2I.Round(Transform.Size * finalScale);
        var scaledOffset = Vector2I.Round(Transform.Offset * finalScale);
        
        var factor = AnchorType.GetFactor();

        var anchorPoint = parentPos + Vector2I.Round((parentSize - Transform.Size) * factor);
        var finalPos = anchorPoint + scaledOffset;
        FinalTransform = new Transform{Offset = finalPos, Size = scaledSize, Scale = finalScale};
    }
       
    internal void AssignTransform(Transform transform) {
        var parentScale = Parent?.FinalTransform.Scale ?? Vector2.One;
        FinalTransform = FinalTransform.WithScale(parentScale * Transform.Scale);
        
        FinalTransform = transform;
        IsLayoutManaged = true;
    }
}
