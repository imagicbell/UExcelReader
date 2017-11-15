using System;



namespace UExcelReader
{
	public class ConfigCollection
	{
		public static string ConfigBundleName = "ConfigData";

		public static Type[] ConfigClassType =
		{
			//start
			 typeof(WordSleepConfig),
			 typeof(ParentFAQConfig),
			//end
		};
	}
}