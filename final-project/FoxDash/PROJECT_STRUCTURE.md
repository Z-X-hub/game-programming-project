# Fox Dash 项目结构

这个项目是一个横版跑酷游戏，核心玩法集中在单个运行场景中，关卡内容由地形生成器在运行时连续生成。

## 主要目录

- `Assets/Scenes`
  - `Play.unity`：游戏主场景，包含摄像机、UI、角色、音频和地形生成入口。

- `Assets/Scripts/FoxDash`
  - `GameManager.cs`：游戏流程入口，负责开始、暂停、死亡结算、重生、分数和存档。
  - `Characters`：角色基类、角色选择状态、快速角色、复活角色和二段跳角色能力逻辑。
  - `TerrainGeneration`：地形块、背景块和运行时生成规则。
  - `Collectables`：金币、宝箱等可收集物逻辑。
  - `Enemies`：障碍和敌人行为。
  - `UI`：开始、暂停、结算、分数、金币、菜单教程和死亡反馈等界面逻辑。
  - `Utilities`：摄像机、地面检测、路径跟随等通用工具。

- `Assets/Prefabs`
  - 角色、平台块、背景块、敌人、金币、粒子和音频预制体。

- `Assets/Sprites/FoxDash`
  - 游戏内使用的角色、UI、背景、地形、金币、敌人等图片素材。

- `Assets/Sounds`
  - 角色动作、收集、敌人、环境和 UI 音效。

- `Assets/SaveGameFree`
  - 本地存档插件，用于保存金币、最高分和音频开关。

- `Packages`
  - Unity 包依赖配置。

- `ProjectSettings`
  - Unity 项目设置。游戏展示名由 `Assets/Editor/FoxDashProjectBranding.cs` 统一写入。

- `THIRD_PARTY_NOTICES.md`
  - RedRunner、素材、存档系统、字体等引用说明和许可证保留。

- `AI_DECLARATION.md`
  - 说明本项目只有部分较难代码和调试点使用 AI 辅助，不代表整个项目由 AI 完成。

## 运行逻辑

1. `GameManager` 进入 `Play.unity` 后初始化 UI、读取存档、监听角色死亡状态。
2. 玩家点击开始后，`GameManager.StartGame()` 恢复时间流动。
3. 开始界面按 `1/2/3` 选择 `PLAYER`、`SOLDIER` 或 `ADVENTURER`，然后点击 Play 按钮进入游戏。
4. `RedCharacter` 每帧读取输入，控制水平移动、跳跃、翻滚和死亡表现；`PLAYER` 速度更快，`SOLDIER` 掉落或落水后可自动复活一次，`ADVENTURER` 可连续两次按空格二段跳。
5. `TerrainGenerator` 根据角色位置持续生成前方地形和背景，清理已经远离视野的块。
6. 角色死亡后，`GameManager` 记录本局分数、更新最高分，然后打开结算界面。
7. 结算界面显示死亡原因、本局分数、最高分、新纪录状态、本局金币和总金币；暂停界面提供继续、重开本局和返回主页。

## 清理规则

根目录只保留可维护项目文件。Unity 自动生成的 IDE 文件、临时缓存和本地日志不属于源码结构；关闭 Unity 后可以删除 `Library`、`Temp`、`Logs`、`UserSettings`，Unity 下次打开会自动重建。
