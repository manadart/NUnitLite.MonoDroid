# NUnitLite.MonoDroid #

This solution is a compilation of NUnitLite targeting Mono for Android. 

At present, there is no runner - If you include this library in an android application, the tests can be run on an emulator. I have included an example test application.

There is a boolean return from the test fixture run, so that the calling application can determine if the all the tests passed or not. The example simply chnges the colour of the output based on the return.

![Failing Fixture](http://cloud.github.com/downloads/SpiritMachine/NUnitLite.MonoDroid/NUnitLiteDroidPass.jpg "Failing Fixture")
![Passing Fixture](http://cloud.github.com/downloads/SpiritMachine/NUnitLite.MonoDroid/NUnitLiteDroidFail.jpg "Passing Fixture")

The original NUnitLite readme is in [NUnitLite.README.txt](https://github.com/SpiritMachine/NUnitLite.MonoDroid/blob/master/NUnitLite.README.txt). It has notes on features and usage. Despite the text there, the version of NUnitLite used is 0.6.1.
