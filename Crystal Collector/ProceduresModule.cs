using System;
namespace Crystal_Collector
{
	public class ProceduresModule
	{
		public static string ValidateGender(string input)
		{
			if(input.ToUpper() == "M")
			{
				return "Masculino";
			}

            if (input.ToUpper() == "F")
            {
                return "Femenino";
            }

			throw new ArgumentException("Ingreso un dato invalido, Ingrese 'M' o 'F' ");
        }
	}
}

