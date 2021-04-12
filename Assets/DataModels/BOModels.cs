using UnityEngine;

namespace DataModels
{
    #region Brick
    public abstract class Brick
    {
        private Color color;
        private int points;
        public Color Color { get => color;}
        public int Points { get => points;}

        public Brick(Color brickColor, int brickPoints)
        {
            this.color = brickColor;
            this.points = brickPoints;
        }
    }

    public class RedBrick : Brick
    {
        public RedBrick():base(Color.red,30)
        {
        }
    }

    public class YellowBrick : Brick
    {
        public YellowBrick():base(Color.yellow, 20)
        {
        }
    }

    public class GreenBrick : Brick
    {
        public GreenBrick():base(Color.green, 10)
        {

        }
    }
    #endregion

    #region
    public class Level
    {
        private int levelNumber;
        private int rows;
        private int columns;

        public int LevelNumber { get => levelNumber;}
        public int BricksCount { get => columns * rows;}
        public int Rows { get => rows; }
        public int Columns { get => columns;}

        public Level(int level, int columns, int rows)
        {
            this.levelNumber = level;
            this.columns = columns;
            this.rows = rows;
        }

    }
    #endregion

}
