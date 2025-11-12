# Technical Features

> ## Fixed physics update
On a new thread, using c# tasks, we create a fixed physics update by checking how much time has passed using delta time; Once enough time has passed we run a physics update, if too much frame time has accumulated, 
tracked with the "accumulator", we run multiple updates simultaniously.

> ## Interpolation
For non static objects, instead of simply drawing from top and left, we calculate how much frametime has passed since the last update, and pass it into a lerp function to create smooth movement.

> ## Map Creation and Loading
For map creation we use a 2d array of tiles, each tile type is represented by a enumerated tile type, with a represented brush or image etc. For loading the map there is just one function, maploader.loadmap(int toload), calling this will retreive the 
respective map from the correct file, and loop through each axis of the array to create new tile entities.

> ## Input Handling
To handle input, on the mainwindow.xaml file I added onkeydown and onkeyup events which on the mainwindow.cs file will call their respective functions. From there we pass the keyEventArgs into functions where neccesary, currently only the player.

> ## Colision Detection - NOT IMPLEMENTED YET
I plan to add collision detection through AABB colision detection, if there proves to be to many objects for this algorithm I will add spatial partitioning which seperates all objects into a grid to allow for less overhead when calculating
due to less objects being computed. AABB works by checking against every object if any object is close enough to the position + width, and if it is it will be colliding.
