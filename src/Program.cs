using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;
using VoxelUiDemo.ui.elements;
using VoxelUiDemo.voxel_engine;
using Transform = VoxelUiDemo.ui.Transform;

namespace VoxelUiDemo;

static class Program {
    private static UiInputSystem input = new UiInputSystem();
    
    private static void Main(string[] args) {

        Raylib.InitWindow(0, 0, "ui prototype");
        //Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);
        
        var rootCanvas = UiTempContent.AssembleUI(Raylib.GetMonitorWidth(0), Raylib.GetMonitorHeight(0));

        List<UiRenderCommand> renderCommands = [];
        while (!Raylib.WindowShouldClose()) {
            GameLoop(rootCanvas, renderCommands);
        }

        Raylib.CloseWindow();
    }

    private static void GameLoop(UiElement rootCanvas, List<UiRenderCommand> renderCommands) {
        var mousePos = Raylib.GetMousePosition();
        
        rootCanvas.CalculateLayout(); // (instead of GameLoop:) call this when: game start/ui init; window got resized; if dynamic elements are added (inventory opens)
        input.Update(rootCanvas);
        rootCanvas.Update(mousePos);
        
        renderCommands.Clear();
        rootCanvas.CollectRenderCommands(renderCommands);
        DrawCommands(renderCommands);
    }
    
    
    private static void DrawCommands(List<UiRenderCommand> renderCommands) {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var cmd in renderCommands) {
            if (cmd.Type == CommandType.DrawRect) {
                Raylib.DrawRectangle(cmd.Position.X, cmd.Position.Y, cmd.Size.X, cmd.Size.Y, cmd.Color);
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
}