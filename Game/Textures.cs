using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    class Textures
    {
        public static Dictionary<string, Image> Collection = new Dictionary<string, Image>()
        {
            ["obstacle1"] = Image.FromFile(@"..\..\res\bomb.png"),
            ["xwing"] = Image.FromFile(@"..\..\res\xwing.png"),
            ["explosion1"] = Image.FromFile(@"..\..\res\explosion.png"),
            ["xwing_destroyed"] = Image.FromFile(@"..\..\res\xwing_destroyed.png"),
            ["blast"] = Image.FromFile(@"..\..\res\blast.png"),
            ["xwing_state1"] = Image.FromFile(@"..\..\res\animated\xwing\state1.png"),
            ["xwing_state2"] = Image.FromFile(@"..\..\res\animated\xwing\state2.png"),
            ["xwing_state1_overheated"] = Image.FromFile(@"..\..\res\animated\xwing\state1_overheated.png"),
            ["xwing_state2_overheated"] = Image.FromFile(@"..\..\res\animated\xwing\state2_overheated.png"),
            ["corusant_state1"] = Image.FromFile(@"..\..\res\animated\background\corusant\spinning\state_1.png"),
            ["corusant_state2"] = Image.FromFile(@"..\..\res\animated\background\corusant\spinning\state_2.png"),
            ["corusant_state3"] = Image.FromFile(@"..\..\res\animated\background\corusant\spinning\state_3.png"),
            ["enemy"] = Image.FromFile(@"..\..\res\enemy.png")
        };
    }
}
