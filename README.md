# gamejam-floatilla

An attempt at https://itch.io/jam/brackeys-5, whose theme is "Better Together".  Slapping this thing together while the baby is asleep, mostly as an attempt to put some of what I've been learning about Unity into practice.

As you can tell from the Projects section, I spent more time daydreaming about what I could do than actually sitting in front of Unity *doing* something.  Oh well, such is life.

## Interesting things?

### Grid alignment and device placement rules

For the MVP I spent probably too much time building out a system whereby a boat could be rigidly defined, because I was trying to make some narrow constraints for boats such as that cannons/engines/etc... had to be aligned north/south/east/west on the boats, and boats had to be made in a rigid grid.  These constraints were so that hopefully the overall floatilla could be treated almost like Tetris rather than chaos, and the concept of "how many engines are contributing to your forward direction" or "which cannons should fire when I shoot in a given direction" were easier questions to answer.

Anyways, if you open a ship prefab in the **/Assets/Ships/Prefabs/** folder you can play with it, I think I got that part working pretty well and feel good about it, as well as its effect on the ship behavior.

### Lazy value updates

It's a little thing, but I like the **LazyValue<T>** class that I made.
  
### Interactables

I've got the start of the Interactables system going, which should be the foundation for all of the pie-in-the-sky crap in the future projects milestones (the various types of encounters, interactions with people, moral dilemmas, all that jazz).  So far that feels alright, we'll see if it holds up.

## Here there be dragons

The PirateShipAiController is pretty terrible, as is the amount of code that is copy-and-paste shared between it and the FloatillaController.  It could use some clean up for sure, and the pirate ships need a wider variety of strategies, as well as strategies that make sense.  Maybe it could use the Unity pathfinding too, right now it's just making a beeline for the player regardless of what's in the way (and often resulting in its own doom)

I had a hell of a time trying to force the Floatilla into both responding correctly to incoming damage and not clipping through walls and opponents, given that I wanted individual ships in the floatilla to take damage but for the floatilla itself to bump into objects in the world correctly and steer the entire set of ships as if it were a single unit.  Sadly, as far as I can tell if you have RigidBodies on a parent and a child, and a collision happens on a collider on the child, then only the child's Rigidbody will be activated, so that stopped some of the things I wanted to do out of the box.  I ended up giving each individual ship in the floatilla its own rigidbody and control over its motion *except* for rotation which works in the current 1 ship case, but there's going to be problems that I will need to resolve once it has more than one ship in it.  My plan is to use Joints to affix the ships to each other so that they don't drift as each provides thrust for the floatilla, and to do *something* to choose an appropriate pivot point for rotating.  Right now that feels like the jankiest, most hand wavy part of all of this.

So far, actually making a floatilla of more than one ship - which was kind of the point originally - isn't implemented yet, but it's next on the docket.  A decent amount is set up that "should work" once I get the attaching and "boat management" screen working.  I'm still unsure whether I want you to freely choose attaching, or whether you end up in a little tetris-like minigame to place the new ship, which is a late idea I had that distracted me.

In general, I paid little attention to efficiency, and in particular I made no attempt at object pooling or otherwise preventing heap allocations.  That's something I'll probably have to work on if I want to make this a real thing.

## Credit where it's due

I cribbed the functionality for health bars pretty much directly from a tutorial by Jason Weimann (https://www.youtube.com/watch?v=kQqqo_9FfsU).
