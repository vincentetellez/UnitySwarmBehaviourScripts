# UnitySwarmBehaviourScripts
This project contains C# scripts for the Unity game engine which simulate swarm behaviour among multiple independent agents navigating a 3D space. While the original intent of the project was to demonstrate swarm behaviour, the scripts have grown to begin implementation of a modular 3D navigation capability which can be used for general purposes. This is achieved through a Navigation.cs script which contains a single Vector3 object representing an agent's desired direction of travel. Modular behaviour scripts add to this vector with customizable weights, modifying the agent's desires according to its environment.

Currently implemented behaviours include:
* SwarmBasic.cs - Implements basic swarm behaviour, with support for multiple swarms. Agents take into acount all members of their swarm and adhere to the three basic rules of boids: separation, alignment, and cohesion.
* SeekConstant.cs - Seek an object or array of objects with a constant weight, regardless of distance. An optional toggle can be enabled so that the agent automatically updates the array with objects of a specified tag.
* SeekExponential.cs - Seek an object or array of objects with a weight that grows exponentially the closer the agent is, regardless of distance. An optional toggle can be enabled so that the agent automatically updates the array with objects of a specified tag.
* SeekCurve.cs - Seek an object or array of objects with a weight specified by a user-defined function of distance. An optional toggle can be enabled so that the agent automatically updates the array with objects of a specified tag.
* AvoidExponential.cs - Avoid an object or array of objects with exponentially increasing weight based on how close the agent is to the object.
* AvoidCurve.cs - Avoid an object or array of objects with exponentially increasing weight based on a user-defined function of distance.
* Orbit.cs - Approach a particular distance from a specific object, and orbit around that object along some orbital plane.
