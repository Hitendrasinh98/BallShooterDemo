# BallShooterDemo
This is Ball Shooter Demo Game made in unity v2019.4.22f lts URP.

The Tools and Plugins Used:
-DoTween engine (for Animations)
-Default URP Example Assets

GameFlow Inforamtion:
-as soon as Game Start, jsonFile data is loaded and spawning random shape at random location with random distance around player.
-Player can move around and look around and can shoot balls.
-Each shape got lot of cubes and each cubes got collider and rigidbody so if player shoot ball ,it can intract like real world collision.
-shooted ball will move at high speed and  wherever collide it will give certain force to contacted objects to make it more fun.
-once ball collide with any cube of shape then that shape get destroy in 8 seconds and new shape is gerated.


Development side Information:


ShapeCreation Flow:
-Created prefabs of cube.
-Used that Cube prefab to build sampleWall.
-used that sampleWall to make various shapes.

-Seprate Scene is there under Scene/ShapeDesigner. which has all the basic setup to use that ShapeGeenrator Tool to rapidlly create new shapes.
-once shape is done then assingn selectedCubes in Scprit ShapeColorAssigner and  make this as prefabVarient.
-Create one Scriptable Object under "GameData/ "for this new Shape. set Type and colors that we want o apply on this shape and aslo assign that prefab.
-Assign this scriptabl object to ShapeManager Scriptable object.
-Custom inspecto tool is designed to support and show custom feature on this scirptable object ShapeManager.
-oninspecto ,with the help of buton genrate we will have one jsonData in textarea of inspector with custom RichText to show which color are defined on the shapes.
-Copy that textarea jsonData and paste into jsonData.txt file to save the data.
-once this json file is updated with this new shape data. then our new shape is added successfully into game.

with help of CutomeToll and ScriptableObjects, we dont have to rely on code or scene gameObjects to modify or add new Shapes.


Enviorment/Rendering Flow:
-One Ground with some obstacles with nice skybox are implemented.
-Fully baked lgiht and shadow and with the help of SRP and other rednering optimization techniks our entire game render in just 4 Pass (1-Skybox,1-BakedObjects,1-DynamicObjects,1-Canvas)
-no realtime light is there.all light baked into lighProb and Reflectionprobe and Static objects with lightmap.
- Visually improved for 4 type of Graphics setting (low,mid,high,veryHigh).


Gameplay Flow:
-Simple yet effective player Controller Imlemented.
-Balls Pool is implemented with the help of Queue to reduce cpu load.
-Ball and Each cube(more than 100's) have collider and rigidbody in optimized way to support low end device yet give optimum visual physics effects.
-Player can shoot as many ball he want to shoot, objectpool is dynamic so if need it will add new balls into pool.
-On ball Collision with cubes, it will gave some extra force to collided objects and inform that shape to activate Self Destruction in 8 seconds.
-After 8 seconds The Old Shape is destroyed and one event is fired which is listened by gamemanger and it will spawn new Shape imediatlly.

GameManager is handling the flow of the game.
-first it will load the jsonFile and fill data into respective Scriptbaobjects.
-Then Spawn a random Shape with the help of ShapeManager ScriptbaleObject.
-Then this Spawn Shape will intialize there data and apply random color to their cubes whitch are making the final shape.

Other Stuff :
-implemnted fully dynamic Animation with the help of dotween package for pauseMenu.
-Implemetd some sounds with mixer to controll the effects of all SFX type o sounds.
-Optimized in visual size and cpu size to have stable 60FPS.
-Implemented CustomEditor Tool to support prototyping fast and more productive way.
