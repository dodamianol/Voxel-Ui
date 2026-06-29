using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;

namespace VoxelUiDemo;

static class Program {
    private static void Main(string[] args) {
        const int screenWidth = 800;
        const int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "ui prototype");
        Raylib.SetTargetFPS(60);

        var rootCanvas = new UiPanel {
            Position = new Vector2(0, 0),
            Size = new Vector2(screenWidth, screenHeight),
            BackgroundColor = Color.DarkGray
        };

        var testButton = new UiButton {
            Position = new Vector2(300, 250),
            Size = new Vector2(200, 50),
            BackgroundColor = Color.Gold,
            Text = "Click me!",
            OnClick = () => Console.WriteLine("Button was clicked!")
        };

        rootCanvas.Children.Add(testButton);

        List<UiRenderCommand> renderCommands = [];

        // Game Loop
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
        rootCanvas.CollectRenderCommands(renderCommands);

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var cmd in renderCommands) {
            if (cmd.Type == CommandType.DrawRect) {
                Raylib.DrawRectangle((int)cmd.Position.X, (int)cmd.Position.Y, (int)cmd.Size.X, (int)cmd.Size.Y, cmd.Color);
            }
            else if (cmd.Type == CommandType.DrawText) {
                Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, 20, cmd.Color);
            }
        }

        Raylib.EndDrawing();
    }
}