# rbBeatDetect

![image](https://user-images.githubusercontent.com/20556689/191962038-82b22267-42ae-45bb-b41a-4f2af5236a12.png)

## What is this?
This is a simple program to synchronize lights to rekordbox via OSC messages.

## How does this work?
The program scans the memory of the currently running rekordbox.exe and tracks the Master Deck's beat count.

## How to run this?

Order is important! Currently only rekordbox 6.5.1 and 6.6.4 are supported.

1. Make sure you have[ .NET 4.7.2 ](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472 " .NET 4.7.2 ")installed
2. Simply download the latest release from [this](https://github.com/palmarci/rbBeatDetect/releases "this") page. 
3. Extract the contents to a directory.
4. Run rekordbox and load a track to **every** deck.
5. Set the Master to a deck (any).
6. Run the program, configure your settings.
7. Check the "Running" box.


Note: some OSC clients (like QLC+) neads an ON and an OFF switch to register a keypress. This is exactly what the "Mimic human keypress" switch does. You can specify a delay (in ms) between those messages. I use something like 10.


**If you don't have internet access on your computer:** 

The program won't be able to update the offsets on startup, so it will use the last cached version. I recommend running this for the first time with an internet connection. You can also manually enter the correct values for your rekordbox version. Make sure you note those values from the [ offsets.txt](https://raw.githubusercontent.com/palmarci/rbBeatDetect/main/offsets.txt " offsets.txt") file.  

## How can i find the offsets?
[Guide](https://github.com/palmarci/rbBeatDetect/blob/main/RekordboxMemoryScanning.pdf)
