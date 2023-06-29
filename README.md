# PPNM
This is my repository used to store solutions to exercises and homeworks for the course "Praktisk programmering og numeriske metoder".

EXAMSCORE: 9 points

| #  | homework      | A | B | C | Σ   |
 ======================================
| 1  | LinEq         | 6 | 3 | - |  9  |
---------------------------------------
| 2  | EVD           | 6 | - | - |  6  |
---------------------------------------
| 3  | LeastSquares  | 6 | 3 | - |  9  |
---------------------------------------
| 4  | Splines       | 6 | - | - |  6  |
---------------------------------------
| 5  | ODE           | 6 | - | - |  6  |
---------------------------------------
| 6  | Integration   | 6 | - | - |  6  |
---------------------------------------
| 7  | MonteCarlo    | 6 | - | - |  6  |
---------------------------------------
| 8  | Rootfinding   | 6 | - | - |  6  |
---------------------------------------
| 9  | Minimum       | 6 | - | - |  6  |
---------------------------------------
| 10 | Neural Net    | 6 | - | - |  6  |
 ======================================
|                    total points:  66 |

EXAM (9 points):
Bi-linear interpolation on a rectilinear grid in two dimensions

Task A (6 points)
I have implemented a bi-linear interpolation method that works for rectilinear grids and not only cartesian grids.

The implementation works on grids of random sizes and spacings.

The implementation is tested on a test case with known values, and it finds the expected value (see test.txt).

Task B (3 points)
I have plotted the interpolation along a gridline specified by a constant y-value to show that it works for points that lie on the grid-lines (see line.svg).

I have recreated a surface-plot from https://en.wikipedia.org/wiki/Bilinear_interpolation (see wiki.svg).

I have plotted the interpolation on a grid as a surfaceplot together with a plot of the gridpoints for comparison (see interpolate.svg).

Task C would have been to test implementation on a real world example and to make statistics on running time.


