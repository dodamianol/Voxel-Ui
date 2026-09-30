using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;
using VoxelUiDemo.ui.elements.basic;

namespace VoxelUiDemo;

static class Program {
    public static int uiPixelSize = 1;

    private static readonly UiInputSystem Input = new UiInputSystem();
    
    private static void Main() {

        Raylib.InitWindow(0, 0, "ui prototype");
        //Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);
        
        var rootCanvas = UiTempContent.AssembleUi(Raylib.GetMonitorWidth(0), Raylib.GetMonitorHeight(0));

        List<UiRenderCommand> renderCommands = [];
        while (!Raylib.WindowShouldClose()) {
            GameLoop(rootCanvas, renderCommands);
        }

        Raylib.CloseWindow();
    }

    private static void GameLoop(UiElement rootCanvas, List<UiRenderCommand> renderCommands) {
        var mousePos = Raylib.GetMousePosition();
        
        rootCanvas.CalculateLayout(); // (instead of GameLoop:) call this when: game start/ui init; window got resized; if dynamic elements are added (inventory opens)
        Input.Update(rootCanvas);
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
                Raylib.DrawRectangleV(cmd.Position, cmd.Size, cmd.Color);
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