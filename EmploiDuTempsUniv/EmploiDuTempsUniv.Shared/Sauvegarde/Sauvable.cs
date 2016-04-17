using EmploiDuTempsUniv.LogiqueMetier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmploiDuTempsUniv.Sauvegarde
{
	internal interface Sauvable
	{
		Task readMatieres();

		Task readPlanning();

		Task writeMatieres(List<Matiere> liste);

		Task writePlanning(List<Jour> liste);
	}
}