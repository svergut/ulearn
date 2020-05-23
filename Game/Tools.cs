using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Game.GameForm;

namespace Game
{
    public static class Tools
    {
        public static void SetCanvasFlickering(Panel canvas, ref int counter, int flickeringInterval)
        {
            if (counter == 1)
                canvas.BackgroundImage = Textures.Collection["corusant_state1"];
            else if (counter == flickeringInterval)
                canvas.BackgroundImage = Textures.Collection["corusant_state2"];
            else if (counter == 2 * flickeringInterval)
                canvas.BackgroundImage = Textures.Collection["corusant_state3"];
            else if (counter == 3 * flickeringInterval)
                counter = 0;

            counter++;
        }

        public static void SetPlaneFlickering(GameModel game, ref int counter, int flickeringInterval)
        {
            if (counter == 1)
                PlayerCurrentStructure = game.Blasts.Count >= game.ShotsLimit ? Textures.Collection["xwing_state1_overheated"]
                    : Textures.Collection["xwing_state1"];
            else if (counter == flickeringInterval)
                PlayerCurrentStructure = game.Blasts.Count >= game.ShotsLimit ? Textures.Collection["xwing_state2_overheated"] 
                    : Textures.Collection["xwing_state2"];
            else if (counter == 3 * flickeringInterval)
                counter = 0;

            counter++;
        }

        public static bool Intersect(int x1, int x2, int x3, int x4, int y1, int y2, int y3, int y4)
        {
            int left = Math.Max(x1, x3);
            int top = Math.Min(y2, y4);
            int right = Math.Min(x2, x4);
            int bottom = Math.Max(y1, y3);

            int width = right - left;
            int height = top - bottom;

            if (width < 0 || height < 0)
                return false;

            return true;
        }
    }
}
