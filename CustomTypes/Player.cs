namespace CustomTypes
{
	public class Player
	{

		public string name { get; set; }
		public string genre { get; set; }

        public int jewelryLife { get; set; }
        public int collectedCrystals { get; set; }

        public int points { get; set; }




        public Player()
		{
            name = "";
            genre = "";
            jewelryLife = 3;
            collectedCrystals = 0;
			points = 0;
		}


	}

}

