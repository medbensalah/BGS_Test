# Input
**WASD:** For movement\
**Space**: Press space near the shopkeeper (as prompted) to open the shop\
**E**: To open Equip menu\

# Process
-  I was not sure about what type of animation is required for clothing (spritesheet / skeletal / skinned) so I took a look at Little Sim World and seemed like it uses skeletal animation so I proceeded with that.
-  I started by making a simple player controller.
-  I thought putting one of your team in the game would be a fun addition so I picked one andproceeded to skinning the sprite and animating it.
-  I made the shop trigger
-  I made the shop menu and then added scripts to it, I thought filters are required in such a game (from a game design view point)
-  I used events to communicate between buttons and the shop (as the buttons were instantiated at runtime so the events were not se in the inspector)
-  I added the items using a scriptable object that gets parsed to populate the shop.
-  I added player inventory.
-  I added the equip menu which is mostly a copy of the shop except for the event handling
-  I added the background and the audio.

# Personal assessment
Ovverall I think this was pretty good compared to the time given, of course there were some cut outs here and there because the scope was small (for example I didn't use efficient asset loading as there are not that many items, etc...)
