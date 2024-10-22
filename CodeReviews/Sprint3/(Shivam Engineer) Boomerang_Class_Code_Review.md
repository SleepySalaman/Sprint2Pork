## Code Review by Shivam Engineer
Date: 10/21

Sprint Number: 3

File Reviewed: Boomerang.cs

Author of File: Prapul Arumilli

Time Spent: 15 minutes

# Readability Review
Few methods, everything was organized so it was clear what each part of the code was doing. Many parts of the code are simplified to just one line, which makes it slightly harder to read, though more concise.

# Code Quality Review
Code quality seems pretty good, not too much code throughout the whole class, so not much could really be improved. For the different states, uses seemingly arbitrary integers to update startX and startY.

# Hypothetical Change:
Could change direction string to an enum. Update function could use a switch case instead of several if else statements for checking direction. Also could declare sourceRects in the constructor and add the individual rectangles there.
