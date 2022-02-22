using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    public class LevelVM : INotifyPropertyChanged
    {
        private Level level;
        public event PropertyChangedEventHandler? PropertyChanged = (s, a) => { };
        public LevelVM()
        {
            level = new Level();
        }
        private string name;
        public string Name
        {
            get => level.LevelName;
            set { level.LevelName = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); }
        }
        private int width;
        public int Width
        {
            get => level.Width;
            set { level.Width = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Width))); }
        }
        private int height;
        public int Height
        {
            get => level.Height;
            set { level.Height = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Height))); }

        }

        /// <summary>
        /// BUG: file is created but doesn't show up in explorer
        /// </summary>
        internal void SaveAutoSerialized()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // level.bin is the name of the saved file, can be parameterized
            using (FileStream fileStream = new FileStream("level.bin", FileMode.Create))
            {
                binaryFormatter.Serialize(fileStream, level);
            }
        }
        internal void SaveManualSerialized()
        {
            using(var fileStream = new FileStream("level.bin", FileMode.Create))
            {
                using(var binaryWriter = new BinaryWriter(fileStream))
                {
                    //own formatting possible
                    binaryWriter.Write(level.LevelName);
                    binaryWriter.Write(level.Width);
                    binaryWriter.Write(level.Height);
                }
            }
        }
        internal void LoadManualSerialized()
        {
            using (var fileStream = new FileStream("level.bin", FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    level = new Level()
                    //own formatting possible
                    {
                        LevelName = binaryReader.ReadString(),
                        Width = binaryReader.ReadInt32(),
                        Height = binaryReader.ReadInt32()
                    };
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(null));
                }
            }
        }
        internal void LoadAutoSerialized()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var fileStream = new FileStream("level.bin", FileMode.Create))
            {
                binaryFormatter.Deserialize(fileStream);
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
