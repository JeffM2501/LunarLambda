![LunarLambda logo](https://raw.githubusercontent.com/JeffM2501/LunarLambda/master/data/assets/ui/LL_logo_768.png)

# LunarLambda
Started as a refactored C# version of [Empty Epsilon](http://daid.github.io/EmptyEpsilon/).

The game is written using [LudicrousElectron](https://github.com/JeffM2501/LudicrousElectron) engine and uses [OpenTK](https://github.com/opentk/opentk) for the underlying platform support.

## Building

The LunarLambda.sln file in the LunarLambda project is the master file for building on all operating systems
and compilers/IDEs.

LunarLambda expects LudicrousElectron to be at the same level as the LunarLambda folder.

=> Working Dir
	|
	-> LunarLambda
		|
		-> LunarLambda.sln
		-> other Lunar Lambda sources
	-> LudicrousElectron
		|
		-> Ludicrous Electron sources

--Runtime Folders--
The results of builds will be put into the _bin_debug and _bin_release folders depending on the build type.
These folders are setup with runtime folder layouts that the system needs.

### Visual Studio 2017 (Windows)
	Open LunarLambda.sln.
	Right click on the LunarLambda soluition item in the solution explorer (it will be at the top) and select
		'Restore Nuget Packages'
	This will get all external dependencies.
	
	Build ether debug or release.
	Run copy_win_libs.bat from the LunarLambda to copy the required dependency libs into the runtime folders.
	
#### Debugging
	Because the executable needs external dependencies it can't just run from the default "target" folder, so if
	you just hit "debug" it will fail with an error. Before you debug you must set the _bin_debug dir as the working target.
	
	Right Click on the LunarLambda project (it will be at the bottom of the solution list, and is the actual exe project).
	Choose Properties.
	From the debug tab, select "Run external program" and pick the LunarLambda.exe that is in the _bin_debug folder.
	Set the working directory to the _bin_debug dir.
	
	You will now be able to debug.
	
### Mono Develop (Windows, Linux, OSX)
	This works exactly the same as Visual Studio, just open the .sln file and build.
	The only difference is that the copy_win_libs.bat step is only for windows.
	Other OSs need to copy the native libraries o the _bin dirs.
	At this time only windows has a bat file, but the other OS libs are in the Libs dir, just copy them manually.
	
	Note that this will probalby need a relatively new version of Mono Develop, one that supports C#6 and the.net framework v4.7.1
	Note also that this process may not work, main development happens in Visual Studio 2017
	
### msbuild(or xbuild) (Windows, Linux, and OSX using Mono command line tools)
	msbuild is the command line tool for building .net sln files. In older versions of mono it is named xbuild. It is included with mono and visual studio.
	It can be used on any OS (See notes in mono develop about native libs on non windows platforms, it appies here too)
	
	Like mono develop this build process requires a version of mono that supports C#6 and the .net 4.7.1 framework
	Note also that this process may not work, main development happens in Visual Studio 2017, but it will be worked out at some point
	
#### Getting Dependencies	
	The nuget command line tool can get the dependencies direclty from the sln file
	'nuget restore LunarLambda.sln'
	
#### Building
	'msbuild LunarLambda.sln /property:Configuration=Release'
	
	
## Running
LunarLambda is a .net Common Language Runtime (CLR) application. It compiles down to a CLR .exe file on every platform. The same binary
file can run on every platform. There is no need to build a new exe for each platform. Only the native libs in the 
Libs dir are needed on each platform and they are provided as part of the repository.

This means that on Windows you just run the .exe file.
On linux and OSX, you must tell mono to run the .exe file so it will be executed with the mono .net runtime.
This is not emulation. This is not wine, it's just a .net file that ends in .exe.
Mono will run the program nativly on the system using just in time compiling. Think Java with less suck.

