Blog 1 - Roll a ball

For this exercise I followed the tutorial step by step and because of it I got a good understanding of Unity and how scripts are attached to game objects, how ui works and what happens when things don't go as planned.
I made the ball move around with speed. I also made the ground bigger because the ball was just falling into an abyss.
I played with the camera as a child of the sphere but it was doing something weird as it was just spinning around.

When attaching the object to the camera to follow the sphere, without this line of code in the LateUpdate() method it will not work:
transform.position = player.transform.position + offset;  
Also created a moving script according to the tutorial
By the end, I was able to disable the cubes, implement a score system and display a finish game message

![UI01](https://github.com/Caticu/GMD-TowerDefense/assets/36474546/6d3a4bfa-5ba0-4dd6-ab0c-df1e08cd9684)
