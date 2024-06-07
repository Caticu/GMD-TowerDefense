In this blog, I will talk about my progress.

I drew the map elements and 2 monsters.

![image](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/d3b7795d-8029-4ec7-856c-0e7efce075ca)

![file (6)](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/6224b716-9a53-4bb2-84bb-634b283cbc1c)
![file](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/8930d6ea-233d-4988-8e35-1045165b8a34)

I imported the map and use sprite editor to slice each element. Some work on this still needs to be done:
-	Perfectly crop each piece 
-	Find a middle size, scale, bc they don t connect properly 

Created 2 objects: 
square = monster
Triangle = tower

![image](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/9fa9b217-fc89-472f-9e08-06360463c285)

Added some empty objects and placed them along the road, as checkpoint.
Added a script for moving the monster move between different checkpoints.
Tower:
-	Rigid body
-	Collider
Monster:
-	rigid body
-	collider

Wrote a script for the tower, that when somebody touches the collider, and if it is a monster, make it take damage of 10. 

![image](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/aa213707-339e-4b3f-8ea0-3c08e6820ce2)

I made the towers shoot bullets to the monsters.
But I have problems with the OnTriggerStay2D event.  It does not want to shoot. After a while it stops shooting.
The problem was with sleeping mode of the tower’s rigidbody. It has to be set to neverSleep. This way it will always trigger OnTriggerEnter2D.


I started reformatting my code:
-	set up interfaces for monster, tower, bullet
-	created bulletInfo telegram 
-	created dictionary for speed and fireRate
-	created enum for DamageType
-	created a custom Monster called wolf
