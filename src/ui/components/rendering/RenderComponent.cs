namespace VoxelUiDemo.ui.elements {
    public interface RenderComponent {
        void CollectRenderCommands(List<UiRenderCommand> commands, LayoutComponent layout);
    }
}