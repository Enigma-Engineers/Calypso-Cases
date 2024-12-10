INCLUDE ../../../globals/globals.ink

{investigationComplete: -> InvestigationComplete | -> main}

=== main ===
This is the body, it appears to be mangled beyond all recognition #speaker:Mercury

+ [Investigate the body]
There seems to be claw markings and a burnt hole right through his chest... peculiar
~ checkedBody = true 
{checkedHand: {~ investigationComplete = true} ->InvestigationComplete | -> main}

+ [Check the Hand]
The hands seemed to be very bruised... I wonder if he fought back?
~ checkedHand = true
{checkedBody: ->InvestigationComplete | ->main}



=== InvestigationComplete ===
~ investigationComplete = true
There is nothing else to see here...#speaker:Mercury
->END