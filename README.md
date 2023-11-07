# Hyperspherical-Coordinates-transformation
We can represent any n-dimensonal vector as it's angle components multiplied by it's length.

This is how we define a spherical coordinate system in 3 dimensions, see https://en.wikipedia.org/wiki/Spherical_coordinate_system

This piece of code gives you generalized tool to do hyperspherical transformation for any n-dimensonal vector to hyperspherical
coordinates system and reverse.

Very useful tool.

Here angles
$$\phi_1,\phi_2 ... \phi_{n-2} $$
are defined over $[0;\pi]$

meanwhile $\phi_{n-1}$ is over $[0;2 \pi]$

so hyperspherical coordinates is build as following:
$$r, \phi_1, \phi_2, \phi_{n-1}$$

where $r$ is cartesian vector length
