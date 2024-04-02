# Task Tracking Doc

I'm going to use this file to track outstanding features/overall project progress outside of actual ticket creation.


# The entire game up to the first puzzle:

-   title screen, click start
-   Police Station cinematic plays
    -   Sophie walks into frame (HUD: her face is in the bottom left corner)
    -   she walks up to the desk where the cop is, HUD switches to COP's face, his dialogue starts
    -   HUD changes who's face it is based on who is talking, we need a script from the film team
-   cut to main game screen.
    -   Sophie can move WASD and jump, training graphic pops up to describe controls (press e to interact, wasd to move etc)
    -   Maya is on the left side of the screen, making some sort of crying sound (needs to be inviting though not too spooky so you want to run away), the HUD icon is a crying Maya
    -   collider so sophie can only walk towards Maya, a box or something so that the jump functionality is actually being used lol
    -   crying sound gets louder as you walk towards maya, prompted to press e to interact
    -   there should be paralax technically as you walk towards maya...
-   Meeting maya (after you press e)
    -   maya talks (need script from film team)
    -   once she's done yapping, the whole map opens... BUT the puzzle events are locked
 > note -> I think the open world stuff is cool, but if we want to finish this game we need to make it linear. I propose making the whole map NOT actually open, and the game will force you to talk to people in the right order, complete puzzles in the right order, dialogue will be the same, scenes will load in order.

# things to add for first puzzle
1. hit space to skip dialogue
2. sophie walks into frame for police station cinematic
3. character idle (need stuff from design team for that, not a high priority)
4. explain controls screen (need graphic from design)
5. HUD -> <img width="706" alt="Screenshot 2024-04-01 at 12 34 43 PM" src="https://github.com/dwesty23/golden_hours/assets/120140940/ffac7d61-e116-4218-a970-7f224e638ea6"> will need assets from design team, the face changes based on who is talking, default to sophie
6. prompting press e to enter for interactions
7. 2 customers @ diner who you can press e to talk to

# Puzzle 1

**Popup:**
- Glass on the left
- Puzzle on the right
- Empty glass slides on the left before puzzle completion
- Full glass on puzzle completion
- Full glass slides to the right after completion and **REPEAT**

**Customer and Dialogue in the HUD - Automatic**
- Dialogue - press space to continue

**Sophie's icon has a hat**

~~The whole map is accessible~~

~~The arcade is locked - dialogue saying not accessible~~

# georges notes
- Press start
- Police station cinematic
- Sophie walks up to the desk where a cop is standing
- Dialogue runs
- Keyboard does not work except for being able to click through dialogue
- Once dialogue ends camera view goes to normal gameplay
- Sophie standing outside the police station
- A pop up appears showing the controls (WASD, E to inspect, etc.)
- Press escape to leave
- Maya over on the left
- Dialogue box shows Maya’s icon
- Walk up to her
- Press E and dialogue begins
- Now you can move anywhere in the map
- Diner becomes inspectable
- Press E and front of the buildings goes away (once you “open up” a building it will stay that way forever)
- You can now see the inside of the building
- Dialogue with the manager appears and tells you to go to work
- Overlay pop up appears 
- City and regular game screen in the background
- Customers dialogue appears on the bottom asking for orders
- Tiles appear mixed up
- An empty glass slides into the left side of the frame
- Player must complete the puzzle
- Once complete, a new customer appears asking for a new milkshake, tiles appear, glass slides in
- Repeat playing the puzzle
- Repeat for third final puzzle
- Once the puzzle is completed the first memory appears
- Click through each slide/dialogue 
- Once you go through all of the slides the memory pop up closes and you are back in the diner
- Mentions something about an arcade
- Cues player's next steps
- NPCs in the diner that you can interact with and they will say they haven’t seen your sister (Cemetery worker, Mom, Other random)
- Exploring the map
- Arcade
  - In the next screen there is an arcade
  - Player walks up but it is locked
  - The manager is standing outside of the building
  - Press E to talk to them
  - They say that the power is out so the machines don’t work, but if you want to go in you can
  - The front side of the arcade disappears
  - Once the front disappears the music starts playing that is the clue to the puzzle
  - Arcade machines have black screens and aren’t working
  - Player can walk up to the power box and interact with it
  - Press E and a pop up appears with the broken arcade machine and 8 switches and a musical scale
  - When a switch is clicked with the mouse one of the 8 musical notes plays its sound
  - Each switch corresponds to a note
  - Once a correct note is pressed the arcade machine “fixes” itself slightly (like the ship in space dogger) 
  - If a player selects the incorrect switch then the machine will go back to its original broken image
  - If all the switches are pressed in the right order the arcade will be fully fixed and all of the machines will light up
  - The music will continue playing as the machines light up
  - Memory 2 will fade in
  - Click through each slide/dialogue 
  - Mentions something about a cemetery
  - Exploring the map
- Cemetery
  - The next section of the map is a cemetery with a large gate that is locked
  - Press E to interact with the gate 
  - A popup appears with a combination lock with three rings
  - The rings can be rotated to change the selected number
  - The outside of the gate has three hidden numbers on it
  - Those three numbers are the combination to open the lock
  - Press E to go back into the lock pop up
  - Adjust the rings to get the numbers in the correct order
  - Once this happens the popup will go away and the gate will disappear revealing the cemetery
  - Three ghost kids are standing together 
  - Press E to talk to them
  - They say they are playing hide and seek
  - Maya wants to join in and be the seeker
  - They all leave and go hide somewhere in the map
  - Three gravestones become interactable
  - Press E and each gravestone pops up together
  - They each have a riddle that corresponds to the object they are hiding inside
  - Player must explore the map and find these items
  - Press E on each of them and the ghost kid appears 
  - Once you find all three nothing happens like the last two puzzles
  - No memory pops up automatically
  - Go back to the cemetery
  - A new grave has become interactable 
  - Press E on it, a popup shows that it is Maya’s gravestone
  - Memory 3 fades in
  - Click through each slide/dialogue 
  - Reveals who the kidnapper is
  - Quick dialogue between Maya and Sophie
  - Decide to go to the police station and report it
  - Once player gets there the door is interactable
  - Press E on the door and one final “cutscene” occurs
  - Shows Maya’s mom and Sophie’s sister being found by police
  - Sophie and her sister embrace
- End 



