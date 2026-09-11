using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;
using VoxelUiDemo.ui.elements;
using VoxelUiDemo.voxel_engine;
using Transform = VoxelUiDemo.ui.Transform;

namespace VoxelUiDemo;

static class Program {
    private static Draggable? _activeDragElement = null;
    
    private static void Main(string[] args) {

        Raylib.InitWindow(0, 0, "ui prototype");
        //Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);
        
        var screenWidth = Raylib.GetMonitorWidth(0);
        var screenHeight = Raylib.GetMonitorHeight(0);
        
        var rootCanvas = new UiPanel {
            AnchorType = AnchorType.Center,
            Transform = new Transform() { Offset = new Vector2I(0, 0), Size = new Vector2I(screenWidth, screenHeight) },
            BackgroundColor = Color.DarkGray
        };

        UiTempContent.AddTopLeft(rootCanvas);
        UiTempContent.AddTexture(rootCanvas);
        UiTempContent.AddStackPanel(rootCanvas);
        UiTempContent.AddGridPanel(rootCanvas);
        UiTempContent.AddSlider(rootCanvas);


        List<UiRenderCommand> renderCommands = [];

        while (!Raylib.WindowShouldClose()) {
            GameLoop(rootCanvas, renderCommands);
        }

        Raylib.CloseWindow();
    }

    private static void GameLoop(UiPanel rootCanvas, List<UiRenderCommand> renderCommands) {
        var mousePos = Raylib.GetMousePosition();
        rootCanvas.Update(mousePos);
        rootCanvas.CalculateLayout(); // (instead of GameLoop:) call this when: game start/ui init; window got resized; if dynamic elements are added (inventory opens)

        HandleMouseActions(rootCanvas, mousePos);
        
        renderCommands.Clear();
        rootCanvas.CollectRenderCommands(renderCommands);
        DrawCommands(renderCommands);
    }
    
    private static void HandleMouseActions(UiElement rootCanvas, Vector2 mousePos) {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            HandleMouseClick(rootCanvas, mousePos);
        }
        
        else if (Raylib.IsMouseButtonDown(MouseButton.Left)) {
            _activeDragElement?.OnDrag(mousePos);
        }
        
        else if (Raylib.IsMouseButtonReleased(MouseButton.Left)) {
            if (_activeDragElement == null) return;
            
            _activeDragElement.OnDragEnd();
            _activeDragElement = null;
        }
    }

    private static void DrawCommands(List<UiRenderCommand> renderCommands) {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var cmd in renderCommands) {
            if (cmd.Type == CommandType.DrawRect) {
                Raylib.DrawRectangle((int)cmd.Position.X, (int)cmd.Position.Y, (int)cmd.Size.X, (int)cmd.Size.Y, cmd.Color);
            }
            else if (cmd.Type == CommandType.DrawText) {
                Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, cmd.FontSize, cmd.Color);
            }
            else if (cmd is { Type: CommandType.DrawTexture, Texture: not null }) {
                var sourceRec = new Rectangle(0, 0, cmd.Texture.Value.Width, cmd.Texture.Value.Height);
                var destRec = new Rectangle(cmd.Position.X, cmd.Position.Y, cmd.Size.X, cmd.Size.Y);
                Raylib.DrawTexturePro(cmd.Texture.Value, sourceRec, destRec, Vector2.Zero, 0f, cmd.Color);
            }
        }

        Raylib.EndDrawing();
    }
    
    private static bool HandleMouseClick(UiElement element, Vector2 mousePos) {
        if (!element.IsVisible) return false;
        
        if (!element.ContainsPoint(mousePos)) return false;
        
        for (var i = element.Children.Count - 1; i >= 0; i--) {
            if (HandleMouseClick(element.Children[i], mousePos)) return true; 
        }
        
        switch (element) {
            case Draggable draggable:
                _activeDragElement = draggable;
                draggable.OnDragStart(mousePos);
                return true;
            case Clickable clickable:
                clickable.OnClickReceived();
                return true;
            default:
                return element.BlocksInput;
        }
    }
}
