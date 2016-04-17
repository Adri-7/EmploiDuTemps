using EmploiDuTempsUniv.LogiqueMetier;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EmploiDuTempsUniv
{
	public class TypeContext
	{
		public Creneau.TypeC RealType
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public TypeContext(Creneau.TypeC type)
		{
			this.Type = type.ToString();
			if (this.Type == "DG")
			{
				this.Type = "Demi-Groupe (DG)";
			}
			else if (this.Type == "CE")
			{
				this.Type = "Classe Entière (CE)";
			}
			this.RealType = type;
		}

		public static List<TypeContext> getSecContext()
		{
			return new List<TypeContext>()
			{
				new TypeContext(Creneau.TypeC.CE),
				new TypeContext(Creneau.TypeC.DG)
			};
		}

		public static List<TypeContext> getSupContext()
		{
			List<TypeContext> typeContexts = new List<TypeContext>()
			{
				new TypeContext(Creneau.TypeC.TP),
				new TypeContext(Creneau.TypeC.TD),
				new TypeContext(Creneau.TypeC.CM)
			};
			return typeContexts;
		}
	}
}