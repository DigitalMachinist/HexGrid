HexGrid
=======

A logical hex-grid implementation based on Amit Patel's examples at http://www.redblobgames.com/grids/hexagons/.

## Overview

This is a library designed to make computing hexagonal grid geometry straightforward and to enable a broad category of operations to get collections of hexes in various configurations surrounding or in relating to a given hex. Furthermore, it provides functionality to round a 2D point (i.e. cursor screen position or 2D world position) to the nearest hex.

This being said, this is a *logical* hex grid library. It does not actually lay hexes out in memory. It is purely a matchemtical model to make that easier, should you need to do it. This, of course, means that this library will not consume large amounts of memory unless you use it to do that yourself.

## Documentation

Doxygen HTML docs are provided and are relatively complete.

## Tests

A NUnit test project is included --with full unit test coverage, excluding a couple of low-level functions that are private in release build anyway-- that will have full text coverage soon, but is currently just a stub for tests to be created.

If I missed anything, please let me know or submit a PR.

## Developer

Written by Jeff Rose (jrose0@gmail.com)

## License

Licensed under the MIT License. See the LICENCE file for more information.
