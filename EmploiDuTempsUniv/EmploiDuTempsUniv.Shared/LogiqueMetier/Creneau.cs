using System;
using System.Runtime.Serialization;

namespace EmploiDuTempsUniv.LogiqueMetier
{
    /// <summary>
    /// Représente un créneau
    /// </summary>
	[DataContract]
	public class Creneau
	{
        /// <summary>
        /// Obtient ou définit le début du créneau
        /// </summary>
		[DataMember]
		public TimeSpan Debut
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient la durée d'un créneau
        /// </summary>
		public TimeSpan Duree
		{
			get
			{
				return this.Fin.Subtract(this.Debut).Duration();
			}
			private set
			{
			}
		}

        /// <summary>
        /// Obtient ou définit la fin d'un créneau
        /// </summary>
		[DataMember]
		public TimeSpan Fin
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit le jour d'un créneau
        /// </summary>
        /// <remarks>
        /// Attention, ne replace pas le créneau dans le bon jour!
        /// </remarks>
		[DataMember]
		public Jour.day Jour
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit la matiere du créneau
        /// </summary>
		[DataMember]
		public Matiere Matiere
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit la parité du créneau
        /// </summary>
		[DataMember]
		public Creneau.parite Parite
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit le prof du créneau
        /// </summary>
		[DataMember]
		public string Prof
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit la salle du créneau
        /// </summary>
		[DataMember]
		public string Salle
		{
			get;
			set;
		}

        /// <summary>
        /// Obtient ou définit le type du créneau
        /// </summary>
		[DataMember]
		public Creneau.TypeC Type
		{
			get;
			set;
		}

        /// <summary>
        /// Construit un créneau
        /// </summary>
        /// <param name="m">la matiere</param>
        /// <param name="d">le début</param>
        /// <param name="f">la fin</param>
        /// <param name="t">le type</param>
        /// <param name="j">le jour</param>
        /// <param name="s">la salle</param>
        /// <param name="_parite">la parité</param>
		public Creneau(Matiere m, TimeSpan d, TimeSpan f, Creneau.TypeC t, Jour.day j, string s, Creneau.parite _parite)
		{
			this.Parite = parite.PaireImpaire;
			this.Matiere = m;
			this.Debut = d;
			this.Fin = f;
			this.Type = t;
			this.Jour = j;
			this.Salle = s;
		}

        /// <summary>
        /// Construit un créneau
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        /// <param name="t"></param>
        /// <param name="j"></param>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <param name="_parite"></param>
		public Creneau(Matiere m, TimeSpan d, TimeSpan f, Creneau.TypeC t, Jour.day j, string s, string p, Creneau.parite _parite)
		{
			this.Parite = _parite;
			this.Matiere = m;
			this.Debut = d;
			this.Fin = f;
			this.Type = t;
			this.Jour = j;
			this.Salle = s;
			this.Prof = p;
		}

        /// <summary>
        /// Teste si il y a un conflit entre le créneau courant et le créneau passé en paramètre
        /// </summary>
        /// <param name="c">le second créneau à tester</param>
        /// <returns>Le résultat du test (vrai en cas de conflit)</returns>
		public bool Conflit(Creneau c)
		{
            //Si la parité est différente, pas de conflit
            if(Parite != c.Parite && Parite != parite.PaireImpaire && c.Parite != parite.PaireImpaire)
            {
                return false;
            }

            //Si la parité est identique, on vérifie
            return Conflit(c.Debut, c.Fin);
		}

        /// <summary>
        /// Teste si il y a un conflit entre le créneaux et les heures passées en paramètre
        /// </summary>
        /// <param name="d">l'heure de début</param>
        /// <param name="f">l'heure de fin</param>
        /// <returns>Le résultat du test (vrai en cas de conflit)</returns>
		public bool Conflit(TimeSpan d, TimeSpan f)
		{
			bool flag = false;
			if (this.Debut >= d && this.Debut < f || this.Fin <= f && this.Fin > d || d >= this.Debut && d < this.Fin || f <= this.Fin && f > this.Debut)
			{
				flag = true;
			}
			return flag;
		}

        /// <summary>
        /// Retourne une représentation textuelle du type
        /// </summary>
        /// <returns>la chaine</returns>
		public string typeComplet()
		{
			if (this.Type == Creneau.TypeC.CE)
			{
				return "Classe Entière";
			}
			if (this.Type == Creneau.TypeC.DG)
			{
				return "Demi-Groupe";
			}
			return this.Type.ToString();
		}

        /// <summary>
        /// Représente la parité d'un créneau
        /// </summary>
		[Flags]
		public enum parite
		{
            PaireImpaire,
            Paire,
			Impaire
		}

        /// <summary>
        /// Représente le type d'un créneau
        /// </summary>
		[Flags]
		public enum TypeC
		{
			CM,
			TD,
			TP,
			CE,
			DG
		}
	}
}