namespace CustomTypes
{
	public class Level
	{
		public int[,] Board { get; set; }
		public int TrollsCount { get; set; }
		public int SizeX { get; set; }
		public int SizeY { get; set; }


        public Level(int sizeX, int sizeY, int trollsCount)
        {
			SizeX = sizeX;
			SizeY = sizeY;
			TrollsCount = trollsCount;
			Board = new int[sizeX, sizeY];
        }


    }
}

