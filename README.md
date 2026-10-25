# Rolling Sky Unleashed

Many moons ago I loved this mobile game called Rolling Sky. However, in recent times it has been infested with ads and in app purchases that make it unbearable to play. So I decided to pull apart the game the game and just have the raw game without all the ads and in app purchases!

You can play it your browser here: [rollingsky.3kh0.net](https://rollingsky.3kh0.net). For the best experience, I recommend using a mobile device.

## How?

The game was originally made in Unity, but newer all the APKs I checked used [FairGuard](https://www.fair-guard.com/index-en.html) which is a funny program aimed at making reverse engineering much harder. Among the versions I tested, 3.5.8 was the latest one that was not protected. It had your standard IL2CPP metadata and 93 recoverable level layouts. [UnityPy](https://github.com/K0lb3/UnityPy) let me extract the assets, [sqdldev’s level decrypter](https://sqdldev.github.io/rolling-sky/level-encryptor/) handled the encoded maps, and [Il2CppDumper](https://github.com/Perfare/Il2CppDumper) recovered type information and native addresses.

To turn those bits into a playable game needs a bit more work, so I built on [Porygon-Axolotl’s community restoration](https://github.com/Porygon-Axolotl/Rolling-Sky-1.0.0-Public), using [ILSpy](https://github.com/icsharpcode/ILSpy) to recover gameplay code from older Mono-based APKs and bringing over their maps, assets, and music. From there, I rebuilt it for browsers with Unity 2017.4, adapted the controls for desktop and mobile, and eventually moved music to native browser streaming to improve performance. The result is a web port with 33 of my favorite levels.

Since this was my first time decompiling a game, and GPT-6-Astra helped me investigate, debug, and put everything together. Very cool

## Legality

Is this legal? I am not sure, but I am not making any money off of this, and I am not distributing the original APKs. I am only distributing the assets and code that I recovered from them. Rolling Sky is such a fun game and it's a shame that the only way I can actually play is via this method (the game no joke crashes on my phone every time I try to play it).
