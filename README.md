# Netytar

---

Netytar is a Virtual Digital Musical Instrument (Virtual DMI) specially designed to be played without hands, using an eye tracker sensor, and possibly some extra sensors. It's designed for accessibility, especially for people who cannot use/control their hands in order to play music.

The first version was produced as part of my Master Thesis to get the degree in Computer Engineering at University of Pavia (Italy), in 2018, and tested with able-bodied musicians.

## Netytar and The EyeHarp

In February 2018, when I presented Netytar as a Master Thesis, the only comparable alternative to Netytar was [The EyeHarp](http://theeyeharp.org/). Developed by Zacharias Vamvakousis at Pompeu Fabra University (Barcelona, Spain), it's a Virtual DMI, fully operable using an eye tracker or any other sensor able to control a cursor on screen.

A scientific paper was dedicated to The EyeHarp: *Vamvakousis, Zacharias & Ramirez, Rafael. (2016). The EyeHarp: A Gaze-Controlled Digital Musical Instrument. Frontiers in Psychology. 7. DOI: 10.3389/fpsyg.2016.00906.* 

Although The EyeHarp is a great instrument, complete and full of features, Netytar was designed as an alternative. The main differences are:

* **Less control latency**. Due to the way the Netytar interface was designed, it was not necessary to introduce gaze filters, typical of various applications based on gaze interaction (including The EyeHarp). These filters introduce delays, potentially harmful for the musical performance.
* **Different interface**. As like as The EyeHarp Netytar's *note layout* is specifically designed for gaze interaction. It is not inspired by any "conventional" musical instrument. The major peculiarities of the Netytar interface are:
  * **It's isomporphic!** It follows a specific rule that makes invariant to transposition. In other words, any sequence of notes (a song or a musical phrase) has the same "geometric shape" regardless of the key in which it is played. Isomorphic layouts are easy to learn!
  * **The surface's dimension is potentially unlimited.** Its dimension is now limited due to performance factors, but due to the "auto-scrolling" function, which exploits the smooth-pursuit capabilities of our eyes, there could be no limit to the number of keys on its "surface", regardless to the computer screen dimensions. Moreover, adding more keys into the surface does not make them smaller.
  * **More than one "path" for the same sequence**. Since there are multiple keys mapped to the same note, there are multiple ways to play the same sequence: the performer can find the most comfortable for himself. The same happens in stringed instruments, like guitar and bass.
  * **Use of color**. It uses high contrast colors in order to exploit our peripheral vision's capabilities.
* **Hybrid interaction.** Differently from The EyeHarp, which is fully operable using gaze interaction, Netytar is designed to work with (and requires) an additional sensor, other than the eye tracker. This can be a keyboard button (for able bodied musicians) or a breath sensor (instruction to build a simple one with Arduino will be included there!). I'm continuously integrating new sensors as part of my PhD project (EMG, a "bite operated sensor" and head tracking are coming next). Some of these can be used also to control the note dynamics.

A comparison on these and some other aspects between Netytar and The EyeHarp was published as a research paper: 

*Nicola Davanzo, Piercarlo Dondi, Mauro Mosconi, and Marco Porta. 2018. Playing music with the eyes through an isomorphic interface. In Proceedings of the Workshop on Communication by Gaze Interaction (COGAIN '18). ACM, New York, NY, USA, Article 5, 5 pages. DOI: https://doi.org/10.1145/3206343.3206350*

I personally think it's good to have alternatives in the same application field. Netytar is *not* meant to be better than The EyeHarp. It could be the same as saying that guitar is better than piano. I like the fact that a musician can select between alternatives: probably there are some features in which The EyeHarp excels, some other in which Netytar does.

## Hardware Requirements

In order to install and play Netytar, you need the following stuff:

* **A Windows PC**. Unfortunately, at the moment in which I'm writing, Tobii is releasing drivers only for Windows. Netytar is written in C#, part of the .NET Framework. I've tested Netytar only on Windows 10 at the moment.
* **An eye tracker**. At the moment, Netytar has been developed to work with [Tobii's low cost eye trackers](https://gaming.tobii.com/products/peripherals/) (sold at a price range between 100€ and 200€).
* **A button/switch/sensor**. It is required to "activate" notes. It can be operated with a keyboard button, but in order to control dynamics, it's better to have a dynamic button/sensor. I will include the instructions to build a breath sensor with Arduino (which I'm currently using). Otherwise, you can try the experimental "EyePos" function, which allows you to play Netytar using 3D eye position to control dynamics.

Netytar has been developed and tested on a laptop with the following characteristics:

* Intel(R) Core(TM) i7-8550U CPU @ 1.80 GHz (4 physical cores, 4 virtual cores)
* 8 GB RAM
* NVIDIA GeForce MX150 GPU

## Software Requirements

I prepared a package named "Netytar Demo Package", which contains all that's needed to install and play music with Netytar. Please consider that, even if fully working it may contain obsolete versions of the Tobii drivers, as well as an outdated version of Netytar and all the software included in the package. It can be downloaded following the link [https://tinyurl.com/netytardemopackage](https://tinyurl.com/netytardemopackage). 

Otherwise, to get the last version of everything, you need to do the following:

* **Download/clone this repository somewhere in your PC.** Please consider that you need both "NeeqDMIs" and "Netytar".
* **Download and install the latest Tobii driver**. You can find it on [Tobii's official website](https://gaming.tobii.com/getstarted/).
* **Download and install .NET Framework version 4.8**, if not already present in your PC. You only need the *Runtime* version. It can be found on [Microsoft's official website](https://dotnet.microsoft.com/download/dotnet-framework/net48).
* **Download and install the font Digital-7**. It's not mandatory, but if you don't install it, Netytar's interface will not look in the right way. :) You can find it on [Dafont.com](https://www.dafont.com/digital-7.font).

Since Netytar is a MIDI controller, you'll need some sounds in order to play it. There's a MIDI port selector on the interface which you can use to route the messages to an external synthesizer connected to your PC. Otherwise, you can use a software synthesizer. My favourite way is to use free VST plugins (Steinberg's virtual instruments). You can do the following:

* **Download and install [LoopMIDI](https://www.tobias-erichsen.de/software/loopmidi.html)**. It's a nice, free and lightweight software that you can use to create a virtual loopback MIDI cable. Just launch it and create a virtual MIDI port.
* **Download and install [VSThost](http://hermannseib.com/english/vsthost.htm)**. Another free software which can host VSTs. You'll find instructions and documentations in to use it on the website.
* **Download some nice VST plugins**. You can find a lot of free ones on [vst4free.com](http://www.vst4free.com/). My favourites for Netytar are [DVS Saxophone](http://www.vst4free.com/free_vst.php?id=187) (altough it has a limited pitch range) and [Mr Ray Rhodes piano](http://www.vst4free.com/free_vst.php?id=273).

## How to play (instructions)

First of all, launch LoopMIDI and VSThost, and load a VST plugin. Check in the MIDI options that VSThost has the LoopMIDI port as input.

Launch Netytar. You can find the executable in the folder *Netytar/Netytar/bin/debug* as *Netytar.exe*.

Press the "Start" button to draw the surface and start Netytar.

### Interface

![rm_line1](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_line1.png)

![rm_com](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_com.png)

![rm_dynamics](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_dynamics.png)

![rm_eyeposcal](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_eyeposcal.png)

![rm_midiport](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_midiport.png)

![rm_noteinfo](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_noteinfo.png)

![rm_scale](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_scale.png)

![rm_sensor](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_sensor.png)

![rm_soundmods](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_soundmods.png)

![rm_surface](C:\Users\neequ\Documents\GitHub\Netytar\Images\rm_surface.png)