Final blog post

I will talk about how my progress and problems.

I introduced cursor for virtual mouse. This is a bit of a problem because it does not work as intended. I can navigate around the scene, but if a game object has an onClick method, it does not trigger it. Therefore, I had to change the TowerSites, and Towers. 
I added to each of them a button which is not easily visible. This way I can still click with the virtual Mouse. 

Also, events are trigger twice making the gold increase and lives lost to double their loss value. I think it is because somewhere in the code, I duplicate the event, and it registers both of them.
Added sound to play that can be toggled off or on.
Added a main menu. 

The game works, it can be played, but it needs extra features:
- finish the level and go to another level ( right now monster are spawned to infinite)
- create visual upgrades to towers
- add animations for monster movement, monster death, tower shooting, bullets explosion( for AOE tower)
- add sound for on dying monster
- add sound for when the tower shoots.

![image](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/94fdd734-7ff5-402c-81c9-e3ffcc8c775c)
