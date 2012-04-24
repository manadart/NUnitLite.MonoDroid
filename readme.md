# NUnitLite.MonoDroid #

This solution is a compilation of NUnitLite targeting Mono for Android. 

At present, there is a very basic runner - If you include this library in an android application, the tests can be run on an emulator. I have included an example test application.

There is a boolean return from the test fixture run, so that the calling application can determine if the all the tests passed or not. The example simply changes the colour of the output based on the return.

![Failing Fixture](https://github.com/SpiritMachine/NUnitLite.MonoDroid/raw/master/Images/NUnitLiteDroidPass.jpg "Failing Fixture")
![Passing Fixture](https://github.com/SpiritMachine/NUnitLite.MonoDroid/raw/master/Images/NUnitLiteDroidFail.jpg "Passing Fixture")

The original NUnitLite readme is in [NUnitLite.README.txt](https://github.com/SpiritMachine/NUnitLite.MonoDroid/blob/master/NUnitLite.README.txt). It has notes on features and usage. Despite the text there, the version of NUnitLite used is 0.6.1.
