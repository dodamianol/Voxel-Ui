using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public abstract class UiElement {
    public UiElement? Parent { get; private set; }
    public List<UiElement> Children { get; } = [];
    
    public bool IsVisible { get; init; } = true;
    
    public AnchorType AnchorType = AnchorType.TopLeft;

    // Transform
    public Vector2 Offset { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;

    public Vector2 FinalScale { get; private set; } = Vector2.One;
    public Rectangle FinalBounds { get; private set; } 

    public Color BackgroundColor { get; set; } = Color.Gray;
    
    public bool IsHovered;
    public bool BlocksInput { get; set; } = true;
    
    private bool IsLayoutManaged { get; set; } 

    public void AddChild(UiElement child) {
        child.Parent = this;
        Children.Add(child);
    }
    
    public virtual bool ContainsPoint(Vector2 point) {
        return Raylib.CheckCollisionPointRec(point, FinalBounds);
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
            Position = new Vector2(FinalBounds.X, FinalBounds.Y),
            Size = new Vector2(FinalBounds.Width, FinalBounds.Height),
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
        var parentPos = Parent?.FinalBounds.Position ?? Vector2.Zero;
        var parentSize = Parent?.FinalBounds.Size ?? new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        var parentScale = Parent?.FinalScale ?? Vector2.One;
        FinalScale = parentScale * Scale;
        var scaledSize = Size * FinalScale;
        var scaledOffset = Offset * FinalScale;
        
        var factor = GetAnchorFactor(AnchorType);

        var anchorPoint = parentPos + (parentSize - Size) * factor;
        var finalPos = anchorPoint + scaledOffset;
        FinalBounds = new Rectangle(finalPos.X, finalPos.Y, scaledSize.X, scaledSize.Y);
    }
    
    private static Vector2 GetAnchorFactor(AnchorType anchor) => anchor switch {
        AnchorType.TopLeft      => new Vector2(0f, 0f),
        AnchorType.TopCenter    => new Vector2(0.5f, 0f),
        AnchorType.TopRight     => new Vector2(1f, 0f),
        AnchorType.MiddleLeft   => new Vector2(0f, 0.5f),
        AnchorType.Center       => new Vector2(0.5f, 0.5f),
        AnchorType.MiddleRight  => new Vector2(1f, 0.5f),
        AnchorType.BottomLeft   => new Vector2(0f, 1f),
        AnchorType.BottomCenter => new Vector2(0.5f, 1f),
        AnchorType.BottomRight  => new Vector2(1f, 1f),
        _ => throw new ArgumentOutOfRangeException(nameof(anchor))
    };
    
    internal void AssignLayoutBounds(Rectangle bounds) {
        var parentScale = Parent?.FinalScale ?? Vector2.One;
        FinalScale = parentScale * Scale;
        
        FinalBounds = bounds;
        IsLayoutManaged = true;
    }
}
