using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [Serializable]
    public class Level
    {
        public Level()
        {

        }
        private string levelName;
        public string LevelName
        {
            get;
            set;
        }
        private int width;
        public int Width
        {
            get;
            set;
        }
        private int height;
        public int Height
        {
            get;
            set;
        }
    }
}
