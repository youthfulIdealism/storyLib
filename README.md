EmergentStoryLib is a library to support the generation and maintenence of procedural and emergent stories, especially in games. The library takes the shape of a scripting language purpose-built for the task, with a focus on ease of use for the non-technical members of your team.

Scripting language example:
```
#person SPOUSE
	tag player_spouse

#text
The <adj_hot intense> sun beats down upon the <adj_naked> earth, leeching <adj_dusty> soil of any moisture. Yellow aspens stand with <adj_drooping> <n_leaf -plural>. Tall brown grass grows in <adj_unhappy> <n_blob -plural> alongside thin, rectangular crystals of cyan. The caravan behind you kicks an enourmous cloud of dust into the <adj_hot> air.

You turn and accept a waterskin from $SPOUSE.NAME$, who <v_smile -present> at you. "We've hit the trees, now. Only a few more miles to the well."

The wellwater will be cool and refreshing. The water in the skins gets hot and tastes like leather after a while. You turn to look over your shoulder at the caravan. "You know, since we're so close, we could relax the water rationing. Morale is important."

You have $RESOURCE.WATER$ water, $RESOURCE.FOOD$ food, and $RESOURCE.MORALE$ morale.


#script
	resource_set WATER 50
	resource_set FOOD 50
	resource_set MORALE 50
	resource_set YELLOWSTONE 50

#filter
	#person GROUCH
		tag grumpy
	#text
	\n\nOverhearing the conversation, $GROUCH.NAME$ interrupts. "We don't know for sure that the well will be there still. It would be better to conserve water until we know we can restock."
#end

#option Relax the water rationing
	resource_subtract WATER 5
	resource_add MORALE 1
	continue_story story_start/story_relaxed_water


#option Make everyone wait until you've reached the well
	resource_subtract MORALE 1
	continue_story story_start/story_kept_water
```

Tutorials:
https://github.com/youthfulIdealism/storyLib/wiki

