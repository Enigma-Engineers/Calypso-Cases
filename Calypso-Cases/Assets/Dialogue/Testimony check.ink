INCLUDE globals/globals.ink

VAR exhaustedDialogue = false
VAR treeOneComplete = false
VAR treeTwoComplete = false

{exhaustedDialogue: I have nothing else to tell you -> END | -> main }

=== main ====
I have something to tell you

+ [Option 1]
    This is dookie fart
    ~ treeOneComplete = true
    {treeTwoComplete:  ->exhaustDialogue| -> main}
    
    +[Option 2]
    This is PeePee
    ~ treeTwoComplete = true
    {treeOneComplete: ->exhaustDialogue | ->main}

    
=== exhaustDialogue ===
That's all i need to tell you
~ canReceiveTestimony = true
-> END
