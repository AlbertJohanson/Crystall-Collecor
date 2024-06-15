using static System.Console;
namespace Crystal_Collector
{
	public class InformationModule
	{
        public static void ShowCommands()
        {
            WriteLine("Comandos:");
            WriteLine("W - Arriba");
            WriteLine("A - Izquierda");
            WriteLine("S - Abajo");
            WriteLine("D - Derecha");
            WriteLine("Presione cualquier tecla para regresar al juego.");
            ReadKey();
        }


        public static void ShowInstructions()
        {
            WriteLine("Instrucciones:");
            WriteLine("Usa las teclas W/A/S/D para mover el avatar.");
            WriteLine("Recoge todos los cristales para avanzar al siguiente nivel.");
            WriteLine("Evita los trolls o responde correctamente a sus preguntas de trivia.");
            WriteLine("Presiona cualquier tecla para volver al menú.");
            ReadKey();
        }

        public static void ShowGameInformation()
        {
            WriteLine("Información del Juego:");
            WriteLine("Crystal Collector es un juego donde navegas un avatar");
            WriteLine("a través de diferentes niveles, recogiendo cristales y evitando trolls.");
            WriteLine("Desarrollado por Albert Johanson.");
            WriteLine("Presiona cualquier tecla para volver al menú.");
            ReadKey();
        }

        public static void GameMenu()
        {
            WriteLine("E - Comandos");
            WriteLine("P - Imprimir Tablero");
            WriteLine("I - Estado de Avatar");
            WriteLine("Q - Terminar Partida");
        }

    }
}

