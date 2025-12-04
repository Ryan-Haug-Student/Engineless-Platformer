# Technical Features

> ## Fixed physics update
On a new thread, using c# tasks, we create a fixed physics update by checking how much time has passed using delta time; Once enough time has passed we run a physics update, if too much frame time has accumulated, 
tracked with the "accumulator", we run multiple updates simultaniously.

> ## Interpolation
For non static objects, instead of simply drawing from top and left, we calculate how much frametime has passed since the last update, and pass it into a lerp function to create smooth movement.

> ## Map Creation and Loading
For map creation we use a 2d array of tiles, each tile type is represented by a enumerated tile type, with a represented brush or image etc. For loading the map there is just one function, maploader.loadmap(int toload), calling this will retreive the 
respective map from the correct file, and loop through each axis of the array to create new tile entities.
The map loader is using the NuGet package "WriteableBitmapEX", a simple opensource library which adds functions to bitmaps so I dont need to draw exact pixels
Im using this primarily in maploader.cs to create a new image of the map for preformance

> ## Input Handling
To handle input, on the mainwindow.xaml file I added onkeydown and onkeyup events which on the mainwindow.cs file will call their respective functions. From there we pass the keyEventArgs into functions where neccesary, currently only the player.

> ## Colision Detection 
This is currently handled in two ways
1. for physical entities to non physical I run two AABB algorithm checks for the x and y axis, and stop velocity and snap position accordingly, I check both seperately so that way walls arent "sticky"
2. for 2 physical entities I just run one AABB check and trigger a onCollisionEnter function for both, allowing the player to take damage, the roomba to swap direction accordingly, and the flag to transition to the next level

> ## Sprite handling
I plan on adding a animation compenent, havent completed that yet. However sprites themselves are either assigned in the creation for sprites that only have one option, pretty much everything but the floor. but for the ground I check adjascent tiles and add values depending on which are present or not, I then map that final value to a matching tile which creates a seemless ground

> ## Camera Movement
The camera following the player was simple to implement, you can just add a transform to the window position, so I just added that transformation to the player position.

> ## Grappling 
For the grapple system to work I created a new list of every point which the player can grapple too, I then loop through that and check for the closest one, and if that one is within range, then the player can grapple, which adds velocity based on distance

> ## Player Grounding
Due to how I handle collisions I can ensure that when the player hits the ground, the Y velocity is always set to zero, and as such I can check the previous velocity to see if the player was moving up or down and if its down I can set grounded to true, creating reliable grounding for the player

> ## World Data
Because I run physics on a seperate thread to ensure a smooth game interface, I have to have a seperate class that is accessable on all threads instead of only the UI thread for data such as a list of entities
