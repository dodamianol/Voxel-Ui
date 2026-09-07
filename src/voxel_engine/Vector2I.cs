using System.Runtime.CompilerServices;

namespace VoxelUiDemo.voxel_engine;

public readonly struct Vector2I(int x, int y) : IEquatable<Vector2I> {
    public readonly int X = x;
    public readonly int Y = y;

    public static Vector2I Zero  => new(0, 0);
    public static Vector2I One   => new(1, 1);
    public static Vector2I Up    => new(0, 1);
    public static Vector2I Down  => new(0, -1);
    public static Vector2I Left  => new(-1, 0);
    public static Vector2I Right => new(1, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator +(Vector2I a, Vector2I b) => new(a.X + b.X, a.Y + b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator -(Vector2I a, Vector2I b) => new(a.X - b.X, a.Y - b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator *(Vector2I a, int b) => new(a.X * b, a.Y * b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator *(int a, Vector2I b) => new(b.X * a, b.Y * a);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator /(Vector2I a, int b) => new(a.X / b, a.Y / b);
    
    public static bool operator ==(Vector2I left, Vector2I right) => left.Equals(right);
    public static bool operator !=(Vector2I left, Vector2I right) => !left.Equals(right);

    public override bool Equals(object? obj) => obj is Vector2I other && Equals(other);
    public bool Equals(Vector2I other) => X == other.X && Y == other.Y;
    
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"({X}, {Y})";
}
