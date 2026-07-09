using Rectangle = Raylib_cs.Rectangle;

namespace VoxelUiDemo.ui.elements;

public class UiStackPanel :  UiElement {
    public Orientation Orientation = Orientation.Horizontal;
    public float Spacing { get; set; } = 10;
    
    public override void CalculateLayout() {
        base.CalculateLayout();

        var i = (Orientation == Orientation.Horizontal) ? AbsoluteBounds.X : AbsoluteBounds.Y;

        foreach (var child in Children) {
            if (!child.IsVisible) continue;

            var nextBounds = (Orientation == Orientation.Horizontal) ?
                new Rectangle(i, AbsoluteBounds.Y, child.Size.X, child.Size.Y) :
                new Rectangle(AbsoluteBounds.X, i, child.Size.X, child.Size.Y);
            child.AssignLayoutBounds(nextBounds);
            
            child.CalculateLayout();

            i += (Orientation == Orientation.Horizontal) ? 
                child.Size.X + Spacing :
                child.Size.Y + Spacing;
        }
    }
}