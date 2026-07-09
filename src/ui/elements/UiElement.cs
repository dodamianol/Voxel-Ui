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
    public Vector2 Offset { get; init; }
    public Vector2 Size { get; init; }
    public Rectangle AbsoluteBounds { get; private set; }
    
    public Color BackgroundColor { get; init; } = Color.Gray;
    
    public bool IsHovered;
    public bool BlocksInput { get; set; } = true;

    
    private bool IsLayoutManaged { get; set; } = false;

    public void AddChild(UiElement child) {
        child.Parent = this;
        Children.Add(child);
    }

    public virtual void Update(Vector2 mousePos) {
        IsHovered = Raylib.CheckCollisionPointRec(mousePos, AbsoluteBounds);
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
            Position = new Vector2(AbsoluteBounds.X, AbsoluteBounds.Y),
            Size = new Vector2(AbsoluteBounds.Width, AbsoluteBounds.Height),
            Color = this.BackgroundColor
        });

    }

    public virtual void CalculateLayout() {
        if (!IsVisible) return;

        if (!IsLayoutManaged) {
            var parentPos = Parent?.AbsoluteBounds.Position ?? Vector2.Zero;
            var parentSize = Parent?.AbsoluteBounds.Size ?? new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            var factor = GetAnchorFactor(AnchorType);

            var anchorPoint = parentPos + (parentSize - Size) * factor;
            var finalPos = anchorPoint + Offset;
            AbsoluteBounds = new Rectangle(finalPos.X, finalPos.Y, Size.X, Size.Y);
        }

        foreach (var child in Children) {
            child.CalculateLayout();
        }
    }
    
    protected static Vector2 GetAnchorFactor(AnchorType anchor) => anchor switch {
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
        this.AbsoluteBounds = bounds;
        this.IsLayoutManaged = true;
    }
}