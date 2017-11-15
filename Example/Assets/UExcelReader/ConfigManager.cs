using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace UExcelReader
{
	public class ConfigManager
    {
		private ConfigManager() {}

        private List<object> configList = new List<object>();

        public T GetConfig<T>() where T : class
        {
            return configList.Find(c => c.GetType() == typeof(T)) as T;
        }

        public void Load()
        {
			//todo: 自定义加载二进制数据方法，下面注释掉的代码都是要自定义的
			//ResLoader resLoader = ResLoader.Allocate ();

            foreach (Type byteClassType in ConfigCollection.ConfigClassType)
            {
                string fileName = byteClassType.Name;
				TextAsset t = null; /*resLoader.LoadSync(fileName) as TextAsset;*/

                using (MemoryStream stream = new MemoryStream(t.bytes))
                {
                    stream.Position = 0;
                    var reader = new tabtoy.DataReader(stream, stream.Length);
                    if (!reader.ReadHeader())
                    {
                        Debug.LogErrorFormat(">>>>tabtoy: {0}, combine file crack!", fileName);
                        continue;
                    }

                    var data = Activator.CreateInstance(byteClassType);

                    var mInfos = byteClassType.GetMethods();
                    foreach (System.Reflection.MethodInfo info in mInfos)
                    {
                        if (info.Name == "Deserialize")
                        {
                            bool found = false;
                            ParameterInfo[] pars = info.GetParameters();
                            for (int i = 0; i < pars.Length; i++)
                            {
                                if (pars[i].ParameterType == byteClassType)
                                {
                                    info.Invoke(data, new object[] {data, reader});
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                                break;
                        }
                    }

                    Debug.LogFormat(">>>>tabtoy: parse {0} successfully!", fileName);
                    configList.Add(data);
                }
            }
			/*resLoader.ReleaseAllRes ();
			resLoader.Recycle2Cache ();
			resLoader = null;*/
        }

        /// <summary>
        /// 使用示例
        /// </summary>
        void Example()
        {
            //1. 游戏开始时，在下载完assetbundle后，加载
            //ConfigManager.Instance.Load();

            //2. 应用层获取配置数据
            /*I18nConfig cfg = ConfigManager.Instance.GetConfig<I18nConfig>();
            Debug.Log(">>>>>> config: " + cfg.I18n.Count);*/
        }
    }
}