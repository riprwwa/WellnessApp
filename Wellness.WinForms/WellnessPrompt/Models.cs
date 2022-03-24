using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.WinForms.WellnessPrompt

{
    internal class Category
    {
        public List<string> Items { get; set; }
        public Color UiColor => System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2]);
        public List<byte> Color { get; set; }
        public string Name { get; set; }
    }
}