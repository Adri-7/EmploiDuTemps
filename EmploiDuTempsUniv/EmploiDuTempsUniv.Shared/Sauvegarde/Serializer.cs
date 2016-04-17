using EmploiDuTempsUniv.LogiqueMetier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace EmploiDuTempsUniv.Sauvegarde
{
	internal class Serializer : Sauvable
	{
		private const string MATIERESFILE = "listeMatieres.xml";
		private const string PLANNINGFILE = "listeCours.xml";

		private DataContractSerializer sauveMatieres = new DataContractSerializer(typeof(List<Matiere>));
		private DataContractSerializer sauvePlanning = new DataContractSerializer(typeof(List<Jour>));

		public Serializer()
		{
		}

		public async Task readMatieres()
		{
			try
			{
                
				Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForReadAsync(ApplicationData.Current.LocalFolder, MATIERESFILE).ConfigureAwait(false);
				using (Stream stream1 = stream)
				{
					Matieres.getInstance().ListeMatieres = this.sauveMatieres.ReadObject(stream1) as List<Matiere>;
				}
			}
			catch (Exception)
			{
                return;
			}
		}

		public async Task readPlanning()
		{
			try
			{
				Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForReadAsync(ApplicationData.Current.LocalFolder, PLANNINGFILE).ConfigureAwait(false);
                using (Stream stream1 = stream)
				{
					Semaine.getInstance().Jours = this.sauvePlanning.ReadObject(stream1) as List<Jour>;
				}
			}
			catch (Exception)
			{
                return;
			}
		}

		public async Task writeMatieres(List<Matiere> liste)
		{
			Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync(ApplicationData.Current.LocalFolder, MATIERESFILE, CreationCollisionOption.ReplaceExisting).ConfigureAwait(false);
            using (Stream stream1 = stream)
			{
				this.sauveMatieres.WriteObject(stream1, liste);
			}
		}

		public async Task writePlanning(List<Jour> liste)
		{
			Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync(ApplicationData.Current.LocalFolder, PLANNINGFILE, CreationCollisionOption.ReplaceExisting).ConfigureAwait(false);
            using (Stream stream1 = stream)
			{
				this.sauvePlanning.WriteObject(stream1, liste);
			}
		}
	}
}