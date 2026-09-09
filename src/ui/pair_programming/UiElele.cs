using VoxelUiDemo.voxel_engine;

namespace VoxelUiDemo.ui.pair_programming;

public class UiElele {
    public UiElele? Parent;
    public List<UiElele> Children = [];

    public LayoutComponent? Layout;
    public RenderComponent? Renderer;
    //public InteractableComponent? Interaction;

    public void Update(Vector2I mousePos) {
        //Interaction?.Update(mousePos);
        foreach (var child in Children) child.Update(mousePos);
    }
}
