using static System.Console;
using CustomTypes;
using System.Drawing;

namespace Crystal_Collector
{
	public class Game
	{

		private Player player = new Player();
		private Level[] levels;
		private int currentLevelIndex;
		private int avatarX, avatarY;
        private bool showAllElements;
        private List<TriviaQuestions> triviaQuestions;

		public void Start()
		{
			MainMenu();
			InitializeGame();

            while(true)
            {
                Clear();
                PrintBoard();
                InformationModule.GameMenu();
                ConsoleKeyInfo keyInfo = ReadKey();
                AvatarMove(keyInfo);

                if(CheckWinCondition())
                {
                    WriteLine("Portal is open!!!!!");
                }
            }
		}

		public void MainMenu()
		{
                //If player has information
                if(player.points > 0)
                {
                    ShowFinalInformation();
                }

                Clear();
                ShowSeparator();
				ShowMessage("Bienvenido a Cristal Collector");
				ShowSeparator();
				ShowMessage("Seleccione la opcion que desea realizar");
				ShowSeparator();

				ShowMessage("1. Iniciar nueva partida");
				ShowMessage("2. Instrucciones");
				ShowMessage("3. Informacion sobre CRYSTAL COLLECTOR");
				ShowMessage("4. Salir del juego");

				int option = int.Parse(ReadLine());

				switch (option)
				{
					case 1:
						SetupPlayer();
						break;
					case 2:
                        InformationModule.ShowInstructions();
						break;
					case 3:
                        InformationModule.ShowGameInformation();
						break;
					case 4:
						ShowMessage("Seguro que desea salir de CRYSTAL COLLECTOR? y/n");
						string decision = ReadLine();
						if (decision == "y")
						{
                            Environment.Exit(0);
                            break;
						}
						else
						{
							break;
						}
					default:
						break;
				}
		}

		private void SetupPlayer()
		{
            Clear();
            ShowSeparator();
            ShowMessage("Para iniciar partida porfavor ingresa la siguiente informacion");
            ShowSeparator();

            ShowMessage("Ingrese el nombre del avatar: ");
            player.name = ReadLine();

            while(true)
            {
                try
                {
                    ShowMessage("Seleccione el genero del avatar (H/M): ");
                    player.genre = ProceduresModule.ValidateGender(ReadLine());
                    break;
                }
                catch (ArgumentException ex)
                {
                    WriteLine(ex.Message);
                }
            }

        }

        private void PrintBoard()
        {
            Level currentLevel = levels[currentLevelIndex];

            // Print column headers
            Write("   ");
            
            WriteLine();

            // Print top border
            Write("  ┌");
            for (int x = 0; x < currentLevel.SizeX; x++)
            {
                Write("──");
            }
            WriteLine("┐");

            for (int y = 0; y < currentLevel.SizeY; y++)
            {
                // Print row header
                Write($"  │");
                for (int x = 0; x < currentLevel.SizeX; x++)
                {
                    if (x == avatarX && y == avatarY)
                    {
                        Write("& "); // Avatar
                    }
                    else if (showAllElements && currentLevel.Board[x, y] == 1)
                    {
                        Write("# "); // Crystal
                    }
                    else if (showAllElements && currentLevel.Board[x, y] == 3)
                    {
                        Write("O "); // 
                    }
                    else
                    {
                        Write(". "); // Empty space
                    }
                }
                WriteLine("│");
            }

            // Print bottom border
            Write("  └");
            for (int x = 0; x < currentLevel.SizeX; x++)
            {
                Write("──");
            }
            WriteLine("┘");

        }

        private void AvatarMove(ConsoleKeyInfo keyInfo)
        {
            switch(keyInfo.Key)
            {
                case ConsoleKey.W:
                    if (avatarY > 0) avatarY--;
                    break;
                case ConsoleKey.S:
                    if (avatarY < levels[currentLevelIndex].SizeY - 1) avatarY++;
                    break;
                case ConsoleKey.A:
                    if (avatarX > 0) avatarX--;
                    break;
                case ConsoleKey.D:
                    if (avatarX < levels[currentLevelIndex].SizeX - 1) avatarX++;
                    break;
                case ConsoleKey.E:
                    InformationModule.ShowCommands();
                    break;
                case ConsoleKey.P:
                    ShowFullBoardTemporarily();
                    break;
                case ConsoleKey.I:
                    ShowAvatarStatus();
                    break;
                case ConsoleKey.Q:
                    EndGame();
                    break;
            }

            CheckInteractions();
        }

        private void ShowFullBoardTemporarily()
        {
            showAllElements = true;
            PrintBoard();
            Thread.Sleep(2000);
            showAllElements = false;
        }

        private void ShowAvatarStatus()
        {

            WriteLine("Estado del avatar: ");
            WriteLine($"Nombre: {player.name}");
            WriteLine($"Genero: {player.genre}");
            WriteLine($"Joyas de vida: {player.jewelryLife}");
            WriteLine($"Punteo: {player.points}");
            WriteLine($"Cristales Recolectados: {player.collectedCrystals}");
            WriteLine($"Posicion actual: ({avatarX}, {avatarY})");
            WriteLine("Presione cualquier tecla para regresar al juego.");
            ReadKey();
        }

        private void EndGame()
        {
            MainMenu();
        }

        private void InitializeGame()
		{
			levels = new Level[]
			{
				new Level(3, 3, 0), 
				new Level(4, 4, 1), 
				new Level(5, 5, 4), 
				new Level(6,6,7),	
				new Level(10, 10, 12)
			};

			currentLevelIndex = 0; // initialize in the first level in the array
            InitializeLevel(); ;
            InitializerTriviaQuestions();
        }

		private void InitializeLevel()
		{
			Level currentLevel = levels[currentLevelIndex];

			avatarX = 0;
			avatarY = 0;
			player.collectedCrystals = 0;
            showAllElements = false;

			PlaceCrystals(currentLevel);
			PlaceTrolls(currentLevel);
		}


        private void PlaceCrystals(Level level)
        {
            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(level.SizeX);
                    y = random.Next(level.SizeY);
                } while (level.Board[x, y] != 0 || (x == avatarX && y == avatarY));
                level.Board[x, y] = 1;
            }
        }

        private void PlaceTrolls(Level level)
        {
            Random random = new Random();
            for (int i = 0; i < level.TrollsCount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(level.SizeX);
                    y = random.Next(level.SizeY);
                } while (level.Board[x, y] != 0 || (x == avatarX && y == avatarY));
                level.Board[x, y] = 2;
            }
        }

        private void PlacePortal(Level level)
        {
            Random random = new Random();

            for(int i = 0; i < 1; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(level.SizeX);
                    y = random.Next(level.SizeY);
                } while (level.Board[x, y] != 0 || ( x == avatarX && y == avatarY));
                level.Board[x, y] = 3;
            }
        }

        private void HandleTroll()
        {
            Level currentLevel = levels[currentLevelIndex];
            Random random = new Random();


            int questionIndex = random.Next(triviaQuestions.Count);
            TriviaQuestions selectedQuestion = triviaQuestions[questionIndex];


            WriteLine("Has encontrado un Troll, Responde la trivia para poder salvarte");
            WriteLine(selectedQuestion.Question);


            foreach(var option in selectedQuestion.Options)
            {
                WriteLine(option);
            }

            Write("Ingrese el numero de su respuesta: ");
            string answer = ReadLine();

            if(answer == selectedQuestion.CorrectAnswer)
            {
                WriteLine("Correcto, has ganado una joya de vida");
                player.jewelryLife++;
                player.points += 10;
                currentLevel.Board[avatarX, avatarY] = 0;
            }
            else
            {
                WriteLine("Incorrecto, has perdido una joya de vida");
                player.jewelryLife--;
                player.points -= 20;
                currentLevel.Board[avatarX, avatarY] = 0;

                if (player.jewelryLife <= 0)
                {
                    WriteLine("Game over!!!!!");
                    Environment.Exit(0);
                }


                if (currentLevelIndex >= 0)
                {
                    currentLevelIndex--;
                    InitializeLevel();
                }
            }
        }



        private bool CheckWinCondition()
        {
            return player.collectedCrystals == 6;
        }


        private void NextLevel()
        {
            if(currentLevelIndex < levels.Length - 1)
            {
                currentLevelIndex++;
                InitializeLevel();
            }
            else
            {
                WriteLine("Felicitaciones has completado todo los niveles");
                WriteLine($"Punteo Final: {player.points}");
                MainMenu();
            }
        }


        private void ShowFinalInformation()
        {
            WriteLine($"Felicitaciones {player.name}");
            WriteLine($"Punteo Final: {player.points}");
            WriteLine("   ");
            WriteLine("   ");
        }


        private void CheckInteractions()
        {
            Level currentLevel = levels[currentLevelIndex];

            if (currentLevel.Board[avatarX, avatarY] == 1)
            {
                player.collectedCrystals++;
                currentLevel.Board[avatarX, avatarY] = 0;
                player.points += 15;
                WriteLine("Cristal Recolectado");


                if(player.collectedCrystals == 6)
                {
                    PlacePortal(currentLevel);
                }
            }
            else if (currentLevel.Board[avatarX, avatarY] == 2)
            {
                HandleTroll(); 
            } else if (currentLevel.Board[avatarX, avatarY] == 3)
            {
                WriteLine("Nivel Completado");
                currentLevel.Board[avatarX, avatarY] = 0;
                NextLevel();
            }
        }


        private void InitializerTriviaQuestions()
        {
            triviaQuestions = new List<TriviaQuestions>
            {
                new TriviaQuestions
                {
                    Question = "¿Quién ganó el Oscar a Mejor Actor en 2020?",
                    Options = new List<string> { "1. Leonardo DiCaprio", "2. Joaquin Phoenix", "3. Brad Pitt", "4. Tom Hanks" },
                    CorrectAnswer = "2"
                },
                new TriviaQuestions
                {
                    Question = "¿Cuál es la película con más premios Oscar ganados?",
                    Options = new List<string> { "1. Titanic", "2. Ben-Hur", "3. El Señor de los Anillos: El Retorno del Rey", "4. Todas las anteriores" },
                    CorrectAnswer = "4"
                },
                new TriviaQuestions
                {
                    Question = "¿Qué película es conocida por la frase 'Yo soy tu padre'?",
                    Options = new List<string> { "1. El Padrino", "2. Star Wars: El Imperio Contraataca", "3. El Rey León", "4. Harry Potter y la Piedra Filosofal" },
                    CorrectAnswer = "2"
                },
                new TriviaQuestions
                {
                    Question = "¿Quién dirigió la película 'Inception'?",
                    Options = new List<string> { "1. Steven Spielberg", "2. Quentin Tarantino", "3. Christopher Nolan", "4. Martin Scorsese" },
                    CorrectAnswer = "3"
                },
                new TriviaQuestions
                {
                    Question = "¿Cuál es la película animada más taquillera de todos los tiempos?",
                    Options = new List<string> { "1. Frozen", "2. Toy Story 4", "3. El Rey León (2019)", "4. Los Increíbles 2" },
                    CorrectAnswer = "3"
                }
            };
        }



        public static void ShowSeparator()
		{
			WriteLine("---------------------------------------------------");
        }

		public static void ShowMessage(string message)
		{
			WriteLine(message);
		}
    }
}

