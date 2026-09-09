using System.Numerics;

namespace VoxelUiDemo.ui;

public enum AnchorType {
    TopLeft,    TopCenter,    TopRight,    
    MiddleLeft, Center,       MiddleRight, 
    BottomLeft, BottomCenter, BottomRight
}

public static class AnchorTypeExtensions {
    public static Vector2 GetFactor(this AnchorType anchor) => anchor switch {
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
}
