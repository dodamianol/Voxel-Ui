using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public float Spacing { get; set; } = 10;
    
    protected override void CalculateOwnLayout() {
        base.CalculateOwnLayout();

        var cursor = (Orientation == Orientation.Horizontal) ? AbsoluteBounds.X : AbsoluteBounds.Y;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var nextBounds = (Orientation == Orientation.Horizontal) ?
                new Rectangle(cursor, AbsoluteBounds.Y, child.Size.X, child.Size.Y) :
                new Rectangle(AbsoluteBounds.X, cursor, child.Size.X, child.Size.Y);
            child.AssignLayoutBounds(nextBounds);
            
            child.CalculateLayout();

            cursor += Spacing;
        }
    }
}