# About This Project
Since i've started software / game development, I really have enjoyed system design and low level projects. This was my intro to low level systems regarding game development; With things such as rendering, physics, collision detection, and much more. The game itself is rather simple, with 1 "tutorial" and 3 gameplay levels as the focus was more on the systems behind it all. I really enjoyed making this and learning all about these systems, I hope you enjoy aswell.

# How to build / play
* ## To Build
    * Ensure you have .NET version 8 installed
    * Then build from visual studio and a window should appear with the main menu

* ## To Play
    * The controls to play the game are, "A" and "D" to walk forward and backward
    * Space or "W" to Jump
    * And to grapple the keys are either Left Shift or "E"
    * To load into the game simply press the play button on the main menu, pressing esc will return you to the main menu, and pressing "P" will pause the physics

# WriteableBitmapEX
- I used this nuGet package to build my map visuals, its a simple  package that allows for drawing to bitmaps far easier with features such as fillRect, Bilt (drawing a bitmap over another); Simply put, this package allows for easy writeableBitmap usage instead of drawing pixel by pixel.
<a>Please Check out their GitHub [here](https://github.com/reneschulte/WriteableBitmapEx)</a>

---

# Technical Features
* ## Fixed physics updates
Using c# tasks I asynchronously run a fixed physics update to ensure that the game runs consistently and smoothly. I acheived this through using a accumulator, which tracks how much time has passed and then runs updates until the time passed is less than the fixed step amount of time. Ex - 10sec has passed, physics steps are 1 sec, 10 updates are ran until there is no "time debt".

* ## Collision Detection
I handle collision detection with Axis Alligned Bounding Box (AABB) algorithm, I keep 2 lists of entities, one static, and one dynamic. each dynamic entity checks against all static for collisions and stops velocity if a collision is detected, and each dynamic entity checks against other dynamic entities, this check will run the "OnCollisionEntered" function triggering different events depending on the entity.

* ## Interpolation
For dynamic objects to ensure the game runs smooth, I draw the entities through a interpolated position to make sure they never "jump" between two spots. I take the current position vs the future position, and take the alpha (amount of time passed) and lerp between the points.

* ## Map Creation and Loading
To make sprite loading simple for the map I decided to use a tile system. This involves me creating a large 2d-array of a tile system consisting of the ground and enemy spawns. Then the map loader will read from that array and create a new WriteableBitmap, using WriteableBitmapEx I am able to use the bilt method to draw bitmaps of sprites over the exsisting bitmap, drawing the map visual. Proceeding that I add a appropriate entity to the appropriate list for collisions.

* ## World Data
Because dynamic entities run on a seperate thread for physics, and needing to access certain data that exsists on another thread, I use a world data class to store some data, allowing it to be accessed from any thread.

* ## Sprites and Animation
I made a animator for some physical sprites,
    - General sprites without animation use a bitMapImage / CroppedBitMapImage to store the sprite, the sprite is then drawn by setting the shape fill to a new imageBrush of the bitmap.
    - Animated objects create a animator component, and take in a bitmapimage, aswell as other general parameters, the animator will then iterate through the bitmapimage taking a cropped bitmapimage for the sprite based on the iteration.
    (I also use the animator play as a ASYNC function for accurate timing, and a CancellationTokenSource to be able to stop the animation part way through)

---

# Game Design Choices
* ## Game Overview
While the focus of this project wasnt on the game itself, it still is a major component. the game is a fairly simple platformer with a grappling hook mechanic. featuring 3 levels and 1 "tutorial"

* ## Story and Setting
The story and lore behind everything is that you, the player, are a wisp in a fantasy world. A warlock / evil wizard, broke the fabric of time and came from the future, bringing fragments of time, and objects from the future to your reality. The goal is to bring those fragments back to where they belong by collecting fragments of time and avoiding the roombas.

* ## Art and UI
 - I made all of the art in my freetime, I really like the fantasy pixel art genre which inspiried all the art in my project. my only regret is that I made the player first at the beginning of the school year in 32x, and all the other sprites are 16x...
 - The UI isnt meant to look amazing or be super immersive, just fucntional, hence why everything is just the default buttons or the basic text 
