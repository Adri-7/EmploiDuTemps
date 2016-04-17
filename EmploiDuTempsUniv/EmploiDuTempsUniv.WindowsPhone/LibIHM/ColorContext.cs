using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace EmploiDuTempsUniv
{
	public class ColorContext
	{
		public SolidColorBrush Color
		{
			get;
			set;
		}

		public ColorContext(Windows.UI.Color _color)
		{
			this.Color = new SolidColorBrush(_color);
		}

		public ColorContext(SolidColorBrush brush)
		{
			this.Color = brush;
		}

		public static List<ColorContext> getContext()
		{
			List<ColorContext> colorContexts = new List<ColorContext>()
			{
				new ColorContext(Colors.Blue),
				new ColorContext(Colors.DodgerBlue),
				new ColorContext(Colors.DeepSkyBlue),
				new ColorContext(Colors.LightSkyBlue),
				new ColorContext(Colors.Lime),
				new ColorContext(Colors.LimeGreen),
				new ColorContext(Colors.SpringGreen),
				new ColorContext(Colors.OliveDrab),
				new ColorContext(Colors.Purple),
				new ColorContext(Colors.HotPink),
				new ColorContext(Colors.Indigo),
				new ColorContext(Colors.MediumSlateBlue),
				new ColorContext(Colors.Red),
				new ColorContext(Colors.Crimson),
				new ColorContext(Colors.DarkRed),
				new ColorContext(Colors.Firebrick),
				new ColorContext(Colors.OrangeRed),
				new ColorContext(Colors.DarkOrange),
				new ColorContext(Colors.Orange),
				new ColorContext(Colors.Gold),
				new ColorContext(Colors.Black),
				new ColorContext(Colors.SaddleBrown),
				new ColorContext(Colors.Gray),
				new ColorContext(Colors.Silver),
				new ColorContext(Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush),
				new ColorContext(Colors.WhiteSmoke),
				new ColorContext(Colors.White)
			};
			return colorContexts;
		}

		public static List<ColorContext> getContext(List<Windows.UI.Color> couleurs)
		{
			List<ColorContext> colorContexts = new List<ColorContext>();
			foreach (Windows.UI.Color couleur in couleurs)
			{
				colorContexts.Add(new ColorContext(couleur));
			}
			return colorContexts;
		}
	}
}