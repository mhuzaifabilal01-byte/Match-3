# Match-3 Unity Game

A classic, polished Match-3 puzzle game built using Unity and C#. Switch adjacent gems to create matches of three or more, clear board obstacles, score points, and complete level goals.

## 🔗 Demo Link
Watch the game demo/gameplay video here:  
👉 **[Google Drive Demo Video](https://drive.google.com/file/d/16kDriPqqM97ytUIu5Z33XTTdSIUNgyND/view?usp=sharing)**

---

## 🎮 Key Features
- **Dynamic Board Grid System**: Dynamically instantiates and manages the grid of gems based on customizable board layouts.
- **Match Detection & Finding**: Algorithms that search horizontally and vertically for matching gems of three or more, supporting advanced match combos and cascades.
- **Cascading Gems Refill**: Gems fall down to fill empty spaces created by matched lines, and new gems spawn from the top.
- **Level & Round Management**: Moves/time limits, score tracking, target goals, and automated game win/loss checking.
- **Interactive UI & Visual Polish**: Professional menus including main menu, level selection screens, and game HUD showing score, goals, and transitions.
- **Sound Effects**: Audio integration for match combos, explosions, level completions, and background music.
- **Image Unlocking System**: Progress progression feature where achievements or completion unlock custom images.

---

## 📁 Key File Structure
- [Board.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/Board.cs): Sets up the board grid, handles gem swapping, checking board stability, and refilling empty columns.
- [MatchFinder.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/MatchFinder.cs): Implements the algorithm for finding matched gems, creating bombs, and flagging elements to be destroyed.
- [Gems.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/Gems.cs): Controls gem selection, mouse interaction, movement interpolation, and gem properties (colors, types).
- [RoundManager.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/RoundManager.cs): Manages levels, score tracking, win/loss states, and round times/moves.
- [BoardLayout.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/BoardLayout.cs): Definer configuration helper for setting custom grid dimensions and obstacles directly in the Unity Inspector.
- [ImageUnlockManager.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/ImageUnlockManager.cs): Manages game progress and gallery content unlocking.
- [UIManager.cs](file:///d:/Local%20Disk%20D%20data/Unity%20Projects/Match%203/Assets/Scripts/UIManager.cs): Directs UI updates for round stats, screen transitions, and popup panels.

---

## 🛠️ How to Run & Build
1. Open the project folder in **Unity Hub** (recommended version: Unity 2021.3 LTS or newer).
2. Open the main menu scene found in `Assets/Scenes/Main Menu.unity` or `Level Select.unity`.
3. Press **Play** in the editor, or build for your desired platform (PC, Mac, WebGL, Android) via `File > Build Settings`.
