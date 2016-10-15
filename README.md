
# SFWorkshopThree
This is the third workshop in a series of 3. This workshop is focused around demonstrating the Actor pattern representing a real world object like a security camera.

If no number is passed in it will return a message stating no identifier found. If a number is passed in it will create that actor and return the status.

API Examples:

api/cameras via GET Returns "No Camera Identifier Provided."

api/cameras via POST will create a new Actor and return all of the attributes (Identifier, Location & Status)

/ will bring up an index.html page that can be used to invoke the POST request as well

