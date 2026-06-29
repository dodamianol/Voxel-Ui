using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace VoxelUiDemo {
    // --- 1. DIE ABSTRAKTION (Vulkan-Ready) ---
    public enum CommandType { DrawRect, DrawText }

    public struct UiRenderCommand {
        public CommandType Type;
        public Vector2 Position;
        public Vector2 Size;
        public Color Color;
        public string Text;
    }

    // --- 2. RETAINED MODE: DIE UI-BASISKLASSE ---
    public abstract class UiElement {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color BackgroundColor { get; set; } = Color.Gray;
        public List<UiElement> Children { get; set; } = new();

        public virtual void Update(Vector2 mousePos, bool mouseClicked) {
            foreach (var child in Children) {
                child.Update(mousePos, mouseClicked);
            }
        }

        public virtual void CollectRenderCommands(List<UiRenderCommand> commands) {
            // Erst mich selbst zeichnen
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawRect,
                Position = this.Position,
                Size = this.Size,
                Color = this.BackgroundColor
            });

            // Dann die Kinder zeichnen
            foreach (var child in Children) {
                child.CollectRenderCommands(commands);
            }
        }
    }

    // EINFACHES PANEL (Nutzt einfach die Basis-Logik als Container)
    public class UiPanel : UiElement {
        // Keine Änderungen zur Basisklasse nötig, verhält sich wie ein Container
    }

    // DER BUTTON
    public class UiButton : UiElement {
        public string Text { get; set; }
        public Action OnClick { get; set; }
        private bool _isHovered;

        public override void Update(Vector2 mousePos, bool mouseClicked) {
            // Prüfen, ob Maus über dem Button ist
            _isHovered = mousePos.X >= Position.X && mousePos.X <= Position.X + Size.X &&
                         mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + Size.Y;

            if (_isHovered && mouseClicked) {
                OnClick?.Invoke();
            }

            // WICHTIG: Kinder-Updates aufrufen
            base.Update(mousePos, mouseClicked);
        }

        public override void CollectRenderCommands(List<UiRenderCommand> commands) {
            var renderColor = _isHovered ? Color.LightGray : BackgroundColor;

            // 1. Button-Fläche
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawRect,
                Position = this.Position,
                Size = this.Size,
                Color = renderColor
            });

            // 2. Button-Text
            commands.Add(new UiRenderCommand {
                Type = CommandType.DrawText,
                Position = this.Position + new Vector2(10, 15), // Leicht versetzt fürs Zentrieren
                Text = this.Text,
                Color = Color.Black
            });

            // 3. WICHTIG: Falls der Button Kinder hat, diese AUCH zeichnen
            base.CollectRenderCommands(commands);
        }
    }

    // --- 3. DAS HAUPTPROGRAMM ---
    class Program {
        static void Main(string[] args) {
            const int screenWidth = 800;
            const int screenHeight = 600;

            Raylib.InitWindow(screenWidth, screenHeight, "ui prototype");
            Raylib.SetTargetFPS(60);

            // UI-Baum aufbauen: Jetzt korrekt mit einem UiPanel als Root!
            var rootCanvas = new UiPanel {
                Position = new Vector2(0, 0),
                Size = new Vector2(screenWidth, screenHeight),
                BackgroundColor = Color.DarkGray
            };

            var testButton = new UiButton {
                Position = new Vector2(300, 250),
                Size = new Vector2(200, 50),
                BackgroundColor = Color.Gold,
                Text = "Klick mich!",
                OnClick = () => Console.WriteLine("Button wurde geklickt!")
            };

            rootCanvas.Children.Add(testButton);

            List<UiRenderCommand> renderCommands = new();

            // Game Loop
            while (!Raylib.WindowShouldClose()) {
                // 1. Input & Update
                Vector2 mousePos = Raylib.GetMousePosition();
                bool mouseClicked = Raylib.IsMouseButtonPressed(MouseButton.Left);

                rootCanvas.Update(mousePos, mouseClicked);

                // 2. Logik: Commands sammeln
                renderCommands.Clear();
                rootCanvas.CollectRenderCommands(renderCommands);

                // 3. Renderer (Der Übersetzer)
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                foreach (var cmd in renderCommands) {
                    if (cmd.Type == CommandType.DrawRect) {
                        Raylib.DrawRectangle((int)cmd.Position.X, (int)cmd.Position.Y, (int)cmd.Size.X, (int)cmd.Size.Y, cmd.Color);
                    }
                    else if (cmd.Type == CommandType.DrawText) {
                        // Raylib DrawText nimmt standardmäßig die eingebaute Schriftart
                        Raylib.DrawText(cmd.Text, (int)cmd.Position.X, (int)cmd.Position.Y, 20, cmd.Color);
                    }
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}