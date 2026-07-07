using System.Numerics;
using Raylib_cs;
using VoxelUiDemo.ui;
using VoxelUiDemo.ui.elements;

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
            Offset = new Vector2(0, 0),
            Size = new Vector2(200, 50),
            BackgroundColor = Color.Gold,
            Text = "Click me!",
            OnClick = () => Console.WriteLine("Button was clicked!")
        };
        midCanvas.AddChild(testButton);

        var testTexture = new UiImage() {
            AnchorType = AnchorType.Center,
            Size = new Vector2(50, 50),
            Texture = Raylib.LoadTexture("assets/textures/grass0.png")
        };
        rootCanvas.AddChild(testTexture);

        var transparentPanel = new UiPanel() {
            AnchorType = AnchorType.Center,
            Offset = new Vector2(100, 0),
            Size = new Vector2(200, 200),
            BackgroundColor = new Color(0, 0, 0, 200),
        };
        rootCanvas.AddChild(transparentPanel);


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
            else if (cmd.Type == CommandType.DrawText) {
                Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, 20, cmd.Color);
            }
            else if (cmd is { Type: CommandType.DrawTexture, Texture: not null }) {
                // SourceRec = Der Ausschnitt aus dem Originalbild (hier das ganze Bild)
                var sourceRec = new Rectangle(0, 0, cmd.Texture.Value.Width, cmd.Texture.Value.Height);
        
                // DestRec = Wohin auf den Bildschirm soll es? (Unsere AbsoluteBounds)
                var destRec = new Rectangle(cmd.Position.X, cmd.Position.Y, cmd.Size.X, cmd.Size.Y);
        
                // Zeichnet das Bild passgenau!
                Raylib.DrawTexturePro(cmd.Texture.Value, sourceRec, destRec, Vector2.Zero, 0f, cmd.Color);
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