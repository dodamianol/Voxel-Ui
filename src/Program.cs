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

        var midCanvas = new UiPanel() {
            AnchorType = AnchorType.TopLeft,
            Size = new Vector2(500, 500),
            BackgroundColor = Color.Gray,
        };
        
        rootCanvas.AddChild(midCanvas);

        var testButton = new UiButton {
            AnchorType =  AnchorType.BottomRight,
            IsVisible = false,
            Offset = new Vector2(0, 0),
            Size = new Vector2(200, 50),
            BackgroundColor = Color.Gold,
            Text = "Click me!",
            OnClick = () => Console.WriteLine("Button was clicked!")
        };

        midCanvas.AddChild(testButton);

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

        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            HandleMouseClick(rootCanvas, mousePos);
        }

        renderCommands.Clear();
        rootCanvas.CollectRenderCommands(renderCommands);

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        foreach (var cmd in renderCommands) {
            if (cmd.Type == CommandType.DrawRect) {
                Raylib.DrawRectangle((int)cmd.Position.X, (int)cmd.Position.Y, (int)cmd.Size.X, (int)cmd.Size.Y, cmd.Color);
            }
            else {
                Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, 20, cmd.Color);
            }
        }

        Raylib.EndDrawing();
    }
    
    private static bool HandleMouseClick(UiElement element, Vector2 mousePos) {
        
        if (!element.IsVisible) return false;
        if (!Raylib.CheckCollisionPointRec(mousePos, element.AbsoluteBounds)) return false;
        
        for (var i = element.Children.Count - 1; i >= 0; i--) {
            if (HandleMouseClick(element.Children[i], mousePos)) {
                return true; 
            }
        }

        if (element is not UiButton button) return element.BlocksInput;
        
        button.OnClick?.Invoke();
        return true;
    }
}