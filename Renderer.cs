using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Renderer
{
    const int CONSOLE_UPDATE_RATE = 200;
    const int FIELD_SIZE_X = 60;
    const int FIELD_SIZE_Y = 30;
    static bool IsConnected = false;
    static DateTime nextRender = DateTime.Now.AddMilliseconds(CONSOLE_UPDATE_RATE);

    // Render loop
    private static void Render()
    {
        while (true)
        {
            if (DateTime.Now > nextRender)
            {
                nextRender = DateTime.Now.AddMilliseconds(CONSOLE_UPDATE_RATE);
                DrawGameBoard();
            }
            else
            {
                Thread.Sleep(nextRender - DateTime.Now);
            }
        }

    }

    // draws a 60x30 field
    private static void DrawGameBoard()
    {
        for (int x = 0; x < FIELD_SIZE_X; x++)
        {
            for (int y = 0; y < FIELD_SIZE_Y; y++)
            {
                Console.SetCursorPosition(x, y);
                if (x == 0 || x == FIELD_SIZE_X - 1 || y == 0 || y == FIELD_SIZE_Y - 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }

            }
        }
    }

}
