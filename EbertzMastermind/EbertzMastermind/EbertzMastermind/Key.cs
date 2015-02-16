using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbertzMastermind
{
    class Key
    {
        public readonly static Color[] colors = { Color.Black, Color.Red, Color.Empty };
        private Color[] pattern;

		public Key() {
			pattern = new Color[5];
		}

        public Color this[int i]
        {
            get
            {
                return pattern[i];
            }

            set
            {
                pattern[i] = value;
            }
        }

		public override string ToString() {
			string returnText = "";

			for (int i = 0; i < 5; ++i) {
				returnText += pattern[i].ToString() + " ";
			}

			return returnText;
		}
	}
}
