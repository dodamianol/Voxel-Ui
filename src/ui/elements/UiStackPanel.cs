using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public float Spacing { get; set; } = 10;
    
    protected override void CalculateOwnLayout() {
        base.CalculateOwnLayout();

        var cursor = (Orientation == Orientation.Horizontal) ? FinalBounds.X : FinalBounds.Y;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var nextBounds = (Orientation == Orientation.Horizontal) ?
                new Rectangle(cursor, FinalBounds.Y, child.Size.X, child.Size.Y) :
                new Rectangle(FinalBounds.X, cursor, child.Size.X, child.Size.Y);
            child.AssignLayoutBounds(nextBounds);
            
            child.CalculateLayout();

            cursor += Spacing;
        }
    }
}
