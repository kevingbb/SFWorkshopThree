# SFWorkshopThree
This is the third workshop in a series of 3. This workshop is focused around demonstrating the Actor pattern representing a real world object like a security camera.

If no number is passed in it will return a message stating no identifier found. If a number is passed in it will create that actor and return the status.

API Examples:

api/cameras via GET     Returns "No Camera Identifier Provided."

api/cameras via POST    Will create a new Actor and return all of the attributes (Identifier, Location & Status)

/ (Root of Website)     Will bring up a web page that can be used to invoke the POST request as well

