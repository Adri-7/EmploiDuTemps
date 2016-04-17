using System;
using Windows.ApplicationModel;

namespace EmploiDuTempsUniv
{
	public class versionContext
	{
		public string version
		{
			get
			{
				object[] str = new object[] { "Version ", default(object), default(object), default(object), default(object), default(object), default(object), default(object) };
				ushort major = Package.Current.Id.Version.Major;
				str[1] = major.ToString();
				str[2] = ".";
				ushort minor = Package.Current.Id.Version.Minor;
				str[3] = minor.ToString();
				str[4] = ".";
				str[5] = Package.Current.Id.Version.Build;
				str[6] = ".";
				str[7] = Package.Current.Id.Version.Revision;
				return string.Concat(str);
			}
		}

		public versionContext()
		{
		}
	}
}