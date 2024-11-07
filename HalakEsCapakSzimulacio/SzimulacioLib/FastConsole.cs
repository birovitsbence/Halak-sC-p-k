using System;
using System.Text;

namespace SzimulacioLib
{
    public class FastConsole
    {
        private readonly int width;
        private readonly int height;
        private readonly (char character, ConsoleColor color)[,] buffer;
        private readonly (char character, ConsoleColor color)[,] previousBuffer;

        public FastConsole(int width, int height)
        {
            this.width = width;
            this.height = height;
            buffer = new (char, ConsoleColor)[height, width];
            previousBuffer = new (char, ConsoleColor)[height, width];
            Clear(); // Buffer inicializálása üres karakterekkel
        }

        public void WriteAt(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            // Szöveg karaktereit hozzáadjuk a bufferhez
            for (int i = 0; i < text.Length && x + i < width; i++)
            {
                buffer[y, x + i] = (text[i], color);
            }
        }

        public void Render()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < height && y < Console.WindowHeight; y++)
            {
                StringBuilder lineBuilder = new StringBuilder();
                ConsoleColor? currentColor = null;

                for (int x = 0; x < width && x < Console.WindowWidth; x++)
                {
                    var (character, color) = buffer[y, x];
                    var (prevCharacter, prevColor) = previousBuffer[y, x];

                    if (character != prevCharacter || color != prevColor)
                    {
                        if (currentColor != color)
                        {
                            if (lineBuilder.Length > 0)
                            {
                                Console.Write(lineBuilder.ToString());
                                lineBuilder.Clear();
                            }
                            Console.ForegroundColor = color;
                            currentColor = color;
                        }

                        lineBuilder.Append(character);
                        previousBuffer[y, x] = buffer[y, x];
                    }
                    else
                    {
                        lineBuilder.Append(' ');
                    }
                }

                if (lineBuilder.Length > 0)
                {
                    Console.Write(lineBuilder.ToString());
                }

                Console.WriteLine(); // Új sor a következő sor megjelenítéséhez
            }

            Console.ResetColor();
            Console.CursorVisible = true;
        }

        public void Clear()
        {
            // Törli a buffer tartalmát, hogy üres legyen a következő frissítéshez
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    buffer[y, x] = (' ', ConsoleColor.Black);
                    previousBuffer[y, x] = ('\0', ConsoleColor.Black);
                }
            }
        }
    }
}