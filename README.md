## UExcelReader

[Tabtoy](https://github.com/davyxu/tabtoy) 是一个广受欢迎的跨平台Excel导出工具，支持导出多种格式的数据，并且可以在配置表中定义对象。于是我将其移植到Unity项目中，并且增加了可视化编辑，以及加载方法，使其在Unity中更方便使用。

### 使用方法

可以参考Example，用Unity打开。

1. 将**UnityPackage.unitypackage**导入项目中。

2. 点击**UExcelReader/Open Edit Window**打开编辑页面，如下：

   ![](https://github.com/imagicbell/UExcelReader/blob/master/README/editor_window.png)



### 功能

1. 数据导出支持：bytes, json, lua；

2. 支持多个具有相同格式的配置表share同一个类，见Example中的ParentFAQConfig。

3. 数据加载在`ConfigManager.cs`，使用方法：

   ```c#
     //1. 游戏开始时，在下载完assetbundle后，加载
     ConfigManager.Instance.Load();

     //2. 应用层获取配置数据
     WordSleepConfig cfg = ConfigManager.Instance.GetConfig<WordSleepConfig>();
     Debug.Log(">>>>>> config: " + cfg.WordSleep.Count);
   ```

### 限制
暂时只支持Mac，后续会支持windows.


### 特别感谢

[Tabtoy](https://github.com/davyxu/tabtoy) 
