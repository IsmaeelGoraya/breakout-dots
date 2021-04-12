using DataModels;
using System.Collections.Generic;

namespace Factories
{
    public class BrickFactory
    {
        public static List<Brick> CreateBricks(Level level)
        {
            List<Brick> retBricks = new List<Brick>(level.BricksCount);
            int bricksPerColor = level.BricksCount/3;
            //TODO: Will address validation in future hard coded for now.
            //level.BricksCount / 3 each color
            retBricks.AddRange(CreatRedBricks(bricksPerColor));
            retBricks.AddRange(CreatYellowBricks(bricksPerColor));
            retBricks.AddRange(CreatGreenBricks(bricksPerColor));
            return retBricks;
        }

        private static List<Brick> CreatRedBricks(int numberOfRedBricks)
        {
            List<Brick> retBricks = new List<Brick>(numberOfRedBricks);
            for (int i = 0; i < numberOfRedBricks; i++)
            {
                retBricks.Add(new RedBrick());
            }
            return retBricks;
        }

        private static List<Brick> CreatYellowBricks(int numberOfRedBricks)
        {
            List<Brick> retBricks = new List<Brick>(numberOfRedBricks);
            for (int i = 0; i < numberOfRedBricks; i++)
            {
                retBricks.Add(new YellowBrick());
            }
            return retBricks;
        }

        private static List<Brick> CreatGreenBricks(int numberOfRedBricks)
        {
            List<Brick> retBricks = new List<Brick>(numberOfRedBricks);
            for (int i = 0; i < numberOfRedBricks; i++)
            {
                retBricks.Add(new GreenBrick());
            }
            return retBricks;
        }
    }
}