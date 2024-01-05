# DynamicDeadline
Dynamic Deadline gives you the ability to extend out your deadline by dividing your quota by the average of your daily scrap attained or by a value that you set.

# Installation
1. Install BepInEx.
2. Run your game once with BepInEx installed to generate the necessary folders and files.
3. Move the BepInEx folder from the DynamicDeadline folder into your game directory, select replace files if prompted. 

       Alternatively, just move the DynamicDeadlines.dll from the DynamicDeadline/BepInEx/plugins folder to your game directory's BepInEx/plugins folder. If you do so, run the game once so you can generate the config.
5. Modify the Config Haha.DynamicDeadline.cfg within your game directory's BepInEx/config folder and change the settings to what you prefer.
6. You're done!

### The Dynamic Method
- Dynamic Deadline gives you the ability to extend out your deadline by dividing your quota by the average of your daily scrap attained.
- The Dynamic Method works by calculating what you sold at the end of your quota, and dividing that by the amount of days you had to complete the quota.
   - This value is saved and then averaged over the length of your run, which is used to determine 

### The Legacy Method
- The Legacy Method is what Dynamic Deadline used before the Dynamic Method was introduced.
- Has to be enabled in the config under the __[Customizeable Values - Legacy]__ section.
- You'll need to assign a value to __[Daily Scrap Value]__ based on your preferences, or you can use the default value of 200.
- Your deadline will be determined by your new profit quota divided by __[Daily Scrap Value]__.

# Common Questions / Concerns:
- When my deadline increases too much, the company sells at a negative rate!!!

       A. Dynamic Deadline was designed to give you just enough time to acquire the scrap you need to finish the impossible quotas for the late game. As this would require selling at 100%, I have no intention of fixing this, and intead might remove the ability to sell before the due date.
- Why did my deadline decrease / increase at a lower rate?

        A. You may of acquired enough scrap to increase your daily scrap average a significant amount, and as a result the mod calculated that you need less days to still succeed.
- Help! My deadline is getting too long!

        A. This is due to the exponential growth of Lethal Company's profit quota. Dynamic Deadline tries to give you just enough time to finish every single quota, and as such, your deadline can increase infinitely. If you just want a longer game, enable the __[use Custom Deadline Range]__ and change your __[Maximum Deadline]__ to the longest deadline you want to achieve.
# Current Plans for the future updates:
- A calculation for when players leave your game so you don't get stuck with an impossible quota.
   - ^ include a calculation for when you rehost and have more players.
- A GUI that the host can use so that you don't need to restart your game to change the config.
- Save which calculation was used for each save slot so you can use a different method for each save slot.
- Easter Eggs

## Special thanks to:
- Discord user Mama Llama for answering the majority of my questions in regards to learning how to make mods for Lethal Company.
- Discord user Sov for helping me fix the errors I encountered with such a simple mod and teaching me how to create the config option. I was able to learn a lot from them aswell.
- Everyone who've downloaded the mod! If it wasn't for requests / bug reports I probably wouldn't of updated the mod past the initial release.

# Patch Notes
### Version 1.0.0:
- Initial release
### Version 1.1.0:
- Made the mod recognize the host of the game, and will utilize their config settings for the next feature.
- Proper syncing! Now, when you get your new quota, players will be able to see the proper deadline immediately as opposed to waiting until they finish their next run.
### Version 1.2.0:
- Added the Dynamic Method, which is now the default calculation method.
   - Saves between different save files, so you can have multiple runs using the Dynamic method.
- Added an option to use the old calculation method.
- By popular request, you can now decide whether you want to use the default values for your minimum and maximum deadline, or set your own.


