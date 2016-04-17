using System;
using System.Runtime.Serialization;
using Windows.UI;

namespace EmploiDuTempsUniv.LogiqueMetier
{
	[DataContract]
	public class Matiere
	{
		private string intitule;

		private Color couleurFond;

		private Color couleurPolice;

		private string prof;

		[DataMember]
		public Color CouleurFond
		{
			get
			{
				return this.couleurFond;
			}
			set
			{
				this.couleurFond = value;
			}
		}

		[DataMember]
		public Color CouleurPolice
		{
			get
			{
				return this.couleurPolice;
			}
			set
			{
				this.couleurPolice = value;
			}
		}

		[DataMember]
		public string Intitule
		{
			get
			{
				return this.intitule;
			}
			set
			{
				this.intitule = value;
			}
		}

		[DataMember]
		public string Prof
		{
			get
			{
				return this.prof;
			}
			set
			{
				this.prof = value;
			}
		}

		public Matiere(string nom, Color cF, Color cP)
		{
			this.intitule = nom;
			this.couleurFond = cF;
			this.couleurPolice = cP;
		}

		public Matiere(string nom, Color cF, Color cP, string p)
		{
			this.intitule = nom;
			this.couleurFond = cF;
			this.couleurPolice = cP;
			this.prof = p;
		}

		public string toString()
		{
			return this.intitule;
		}

		public void updateMatiere(string i, Color fond, Color police)
		{
			this.intitule = i;
			this.couleurFond = fond;
			this.couleurPolice = police;
			Matieres.getInstance().save();
			Semaine.getInstance().save();
		}

		public void updateMatiere(string i, Color fond, Color police, string p)
		{
			this.intitule = i;
			this.couleurFond = fond;
			this.couleurPolice = police;
			this.prof = p;
		}
	}
}