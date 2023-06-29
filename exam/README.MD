EXAM (9 points):
Bi-linear interpolation on a rectilinear grid in two dimensions (last digits of study-number: 29)

DESCRIPTION:
The implementation finds the four grid points closes to the point that needs to be interpolated using binary search
from earlier homework. The implementation solves a system of 4 linear equations using the grid points as contraints.

SCORING:
Task A (6 points)
I have implemented a bi-linear interpolation method that works for rectilinear grids and not only cartesian grids.

The implementation works on grids of random sizes and spacings.

The implementation is tested on a test case with known values, and it finds the expected value (see test.txt).

Task B (3 points)
I have plotted the interpolation along a gridline specified by a constant y-value to show that it works for points that lie on the grid-lines (see line.svg).

I have recreated a surface-plot from https://en.wikipedia.org/wiki/Bilinear_interpolation (see wiki.svg).

I have plotted the interpolation on a grid as a surfaceplot together with a plot of the gridpoints for comparison (see interpolate.svg).

Task C would have been to test implementation on a real world example and to make statistics on running time.
