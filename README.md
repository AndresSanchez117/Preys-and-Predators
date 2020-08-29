# Preys and Predators

This is a simulation of prey and predator entities in an environment which is represented
by a graph.

Preys (bones) will try to get to the objective vertex using Dijkstra's shortest path algorithm but prioritizing their safety avoiding being eaten by a predator (dog).

Predators follow the closest prey in range in a greedy manner. If a prey knows it's in danger it tries to scape and find another way to the objective.

## Usage
Load one of the images in the "ImagenesEscenarios" folder, select an objective vertex and place preys and predators where desired, then run the simulation.

The simulation ends when all preys have been eaten or when a prey gets to the objective.

Experiment with different environments and prey-predator placements.