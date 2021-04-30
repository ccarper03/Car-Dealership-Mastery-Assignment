using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace GuildCars.Data.Repositories.Mock
{
    public class ColorRepositoryMock : IColorRepository
    {
        private static List<Color> _colors = new List<Color>();

        private static Color Black = new Color
        {
            ColorId = 1,
            ColorName = "Black"
        };

        private static Color Silver = new Color
        {
            ColorId = 2,
            ColorName = "Silver"
        };

        private static Color Gray = new Color
        {
            ColorId = 3,
            ColorName = "Gray"
        };

        private static Color Tan = new Color
        {
            ColorId = 4,
            ColorName = "Tan"
        };

        private static Color White = new Color
        {
            ColorId = 5,
            ColorName = "White"
        };
        public ColorRepositoryMock()
        {
            if (_colors.Count() == 0)
            {
                _colors.Add(Black);
                _colors.Add(Silver);
                _colors.Add(Gray);
                _colors.Add(Tan);
                _colors.Add(White);
            }
        }

        public IEnumerable<Color> GetAll()
        {
            return _colors;
        }

        public Color GetColorById(int ColorId)
        {
            return _colors.FirstOrDefault(c => c.ColorId == ColorId);
        }
    }
}
