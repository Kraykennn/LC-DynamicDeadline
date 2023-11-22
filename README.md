# DynamicDeadline
Just a simple mod that increases the deadline along with the quota increase.

### Installation
1. Install BepInEx.
2. Run your game once with BepInEx installed to generate the necessary folders and files.
3. Move the BepInEx folder from the DynamicDeadline folder into your game directory, select replace files if prompted (This will only add the files to your folders, it's "replacing" the folder to the same exact folder.) Alternatively, just move the DynamicDeadlines.dll from the DynamicDeadline/BepInEx/plugins folder to your game directory's BepInEx/plugins folder. 
4. If you moved the DynamicDeadline.dll to your plugins folder, run the game once to generate the config file for the mod. If you moved the BepInEx folder, move on to step 5.
5. Modify the Config Haha.DynamicDeadline.cfg within your game directory's BepInEx/config folder and change the setting DailyScrapValue to an amount of scrap you can achieve per day.
6. You're done!

### Special Notice
This is my first attempt at making a mod for the game Lethal Company. I plan for this to be a stepping stone on to more complicated and feature rich mods.

### Special thanks to:
- Discord user Mama Llama for answering the majority of my questions in regards to learning how to make mods for Lethal Company. 
- Discord user Sov for helping me fix the errors I encountered with such a simple mod and teaching me how to create the config option. I was able to learn a lot from them aswell.

### Conflicts/Incompatability

---

This mod postfixes the TimeOfDay.SetNewProfitQuota() method. Any mod that also interacts or replaces this method could potentially clash and cause problems.
