# 物理版象棋2·时空回忆之旅

基于 Unity 6 (Universal 2D) 开发的物理版象棋原型。融合传统中国象棋规则与特殊的“世界主题”机制。

## 📌 当前进度 (v0.2)

- [x] 搭建 Unity 6.3 LTS (6000.3.24f1) 项目工程
- [x] 使用 **Universal 2D** 模板 (URP 2D Renderer)
- [x] 使用 UGUI 构建 9x5 隐形网格棋盘（基于草坪背景图）
- [x] 通过 Resources 动态加载 16 种高清红/绿棋子素材
- [x] 实现开局 9x5 暗棋随机填满（红绿各一半，共45个棋子）
- [x] 实现点击翻棋、前5个翻棋定阵营逻辑
- [x] 支持 ESC 键退出、成功构建 PC 端独立 exe
- [x] 代码成功推送至 GitHub

## 🚧 开发中 (v0.3 计划)

- [ ] 实现棋子的移动与吃子规则（传统象棋走法：车走直、马走日等）
- [ ] 实现“机械物”吃子爆炸（3x3 范围）
- [ ] 实现特殊机制（自杀、自爆、盲吃等）
- [ ] 实现不同世界主题（神秘埃及、海盗港湾等）

## 🛠️ 技术栈

- **引擎**：Unity 6.3 LTS (6000.3.24f1)
- **渲染**：Universal Render Pipeline (URP) 2D
- **UI 系统**：UGUI (Canvas + Image + Text)
- **语言**：C#
- **资源加载**：`Resources.Load<Sprite>`（原型阶段）
- **版本控制**：Git / GitHub

## 🚀 如何构建

1. 使用 Unity Hub 打开项目（确保已安装 6000.3.24f1 版本）。
2. 打开 `Assets/场景/主场景`。
3. 顶部菜单 `File -> Build Profiles`，选择 `Windows` 平台。
4. 确保 `主场景` 已加入 `Scene List`，点击 `Build`。
5. 建议勾选 `Development Build` 并切换 `Scripting Backend` 为 `Mono` 以加快打包速度。

## 📁 目录结构

```text
Assets/
├── Resources/
│   └── 棋子/            # 16 张红绿棋子 PNG 素材 (red_*, green_*)
├── 场景/
│   └── 主场景.unity
├── 脚本/
│   ├── 棋盘/            # BoardCoord.cs, BoardView.cs
│   ├── 棋子/            # PieceView.cs
│   └── 启动/            # XiangqiDemo.cs
└── 图片/
    └── bg               # 草坪背景图
```

## 📝 开发笔记

- 棋盘采用“隐形逻辑网格”，屏幕上不绘制任何网格线，通过 `BoardCoord.ToLocal()` 计算棋子的锚点坐标。
- 由于背景图存在透视变形，逻辑网格无法完美贴合视觉格线，需要通过调整 `BoardView` 的 `CellSize` 和 `BoardOffset` 进行视觉对齐。
- 采用 9x5 随机填满机制，45个棋子暗置，点击翻开前5个决定阵营。

---
*本项目为个人开发原型，正在持续迭代中。*
```

### 🚀 推送到 GitHub：

打开 PowerShell，把这份新的说明文件同步上去：

```powershell
cd E:\PCE2
git add README.md
git commit -m "更新 README 至 v0.2，明确 Universal 2D 模板"
git push
```
