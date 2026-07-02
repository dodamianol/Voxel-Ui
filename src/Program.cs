using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;

namespace VoxelUiDemo;

static class Program {
    private static void Main(string[] args) {

        Raylib.InitWindow(0, 0, "ui prototype");
        //Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);
        
        var screenWidth = Raylib.GetMonitorWidth(0);
        var screenHeight = Raylib.GetMonitorHeight(0);
        
        var rootCanvas = new UiPanel {
            AnchorType = AnchorType.Center,
            Offset = new Vector2(0, 0),
            Size = new Vector2(screenWidth, screenHeight),
            BackgroundColor = Color.DarkGray
        };

        var testButton = new UiButton {
            AnchorType =  AnchorType.BottomRight,
            Offset = new Vector2(0, 0),
            Size = new Vector2(200, 50),
            BackgroundColor = Color.Gold,
            Text = "Click me!",
            OnClick = () => Console.WriteLine("Button was clicked!")
        };

        rootCanvas.AddChild(testButton);

        List<UiRenderCommand> renderCommands = [];

        while (!Raylib.WindowShouldClose()) {
            GameLoop(rootCanvas, renderCommands);
        }

        Raylib.CloseWindow();
    }

    private static void GameLoop(UiPanel rootCanvas, List<UiRenderCommand> renderCommands) {
        var mousePos = Raylib.GetMousePosition();
        var mouseClicked = Raylib.IsMouseButtonPressed(MouseButton.Left);
        rootCanvas.Update(mousePos, mouseClicked);

        renderCommands.Clear();
        rootCanvas.CalculateLayout();
        rootCanvas.CollectRenderCommands(renderCommands);

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var cmd in renderCommands) {
            if (cmd.Type == CommandType.DrawRect) {
                Console.WriteLine(cmd.Position.X + " " + cmd.Position.Y);
                Raylib.DrawRectangle((int)cmd.Position.X, (int)cmd.Position.Y, (int)cmd.Size.X, (int)cmd.Size.Y, cmd.Color);
            }
            else {
                Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, 20, cmd.Color);
            }
        }

        Raylib.EndDrawing();
    }
}