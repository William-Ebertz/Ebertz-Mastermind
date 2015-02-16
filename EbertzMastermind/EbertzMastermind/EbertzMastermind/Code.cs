using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EbertzMastermind
{
    class Code
    {
        Random rand;
        public readonly static Color[] colors = {Color.Blue, Color.Cyan, Color.Green, Color.Pink, Color.Red, Color.Silver, Color.Yellow};
        private List<Color> pattern = new List<Color>(5);

        public Code()
        {
            for (int i = 0; i < 5; ++i)
            {
                pattern.Add(Color.Empty);
            }
        }

        public void createPattern()
        {
            rand = new Random();

            for (int i = 0; i < 5; ++i)
            {
                int index = rand.Next(0, 6);
                pattern[i] = colors[index];
            }
        }

        public Color this[int i]
        {
            set
            {
                pattern[i] = value;
            }
            get
            {
                return pattern[i];
            }
        }

        public Key compareCodes(Code comparator)
        {
            Key theKey = new Key();

            bool[] same = new bool[5];
            Dictionary<Color, int> leftColorCounter = new Dictionary<Color, int>();
            Dictionary<Color, int> rightColorCounter = new Dictionary<Color, int>();

            for (int i = 0; i < 7; ++i)
            {
                leftColorCounter.Add(colors[i], 0);
                rightColorCounter.Add(colors[i], 0);
            }

            for (int i = 0; i < 5; ++i)
            {
                if (pattern[i] == comparator[i])
                    same[i] = true;
                else
                    same[i] = false;
                ++leftColorCounter[pattern[i]];
                ++rightColorCounter[comparator[i]];
            }

            for (int i = 0; i < 5; ++i)
            {
                if (same[i])
                    theKey[i] = Color.Black;
                else if (leftColorCounter[pattern[i]] == rightColorCounter[pattern[i]])
                    theKey[i] = Color.Red;
                else
                    theKey[i] = Color.Empty;
            }
			System.Diagnostics.Debug.Write(theKey.ToString());
            return theKey;
        }
    }
}
