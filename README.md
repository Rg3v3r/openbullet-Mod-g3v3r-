# OpenBullet
OpenBullet 1 has reached the end of its life, no more support will be provided for it. Please consider switching to OpenBullet 2 as it will be kept up to date and offers a lot more features.

[![Build status](https://ci.appveyor.com/api/projects/status/ubdcnn38uanaoqic?svg=true)](https://ci.appveyor.com/project/openbullet/openbullet)


Link to the [Official Forum](https://forum.openbullet.dev/)

**NEW!** The Plugin System was released with version 1.2.0. You can find a [sample Plugin](https://github.com/openbullet/openbullet-plugin) with fully documented code that you can use as a basis to develop your own plugins!

- - - -

OpenBullet is a webtesting suite that allows to perform requests towards a target webapp and offers a lot of tools to work with the results. This software can be used for **scraping** and **parsing data**, automated **pentesting**, unit testing through **selenium** and much more.

**IMPORTANT!** Performing (D)DoS attacks or credential stuffing on sites you do not own (or you do not have permission to test) is **illegal!** The developer will not be held responsible for improper use of this software.

![Runner](https://i.ibb.co/L5psd79/Openbullet.png)

[Here](https://openbullet.github.io) you can find the complete documentation for **usage**, **config making** and the **RuriLib API**.

The **OpenBullet API** was released. It can be used to host configs remotely on your server and have OpenBullet download them upon Rescan. It's very useful to always have updated configs between different computers or people. You can learn more about it [here](https://openbullet.github.io/remote.html).



# How to build
0. Make sure you have installed the .NET framework (dev) 4.7.2.
1. **Clone this repository** and open the solution file with Visual Studio.
2. Switch to **Release** mode for a much cleaner output.
3. **Build** the solution (Visual Studio will fetch all the missing nuget packages).
4. You can find the executables inside the folders OpenBullet/bin/Release and OpenBulletCLI/bin/Release.

# License
This software is licensed under the MIT License.


# Credits
I want to thank all the community for their inputs that shaped OpenBullet.
