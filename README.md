# Ready Player Me Avatars Loadtest

This sample project shows the performance of different spec'd Ready Player Me avatars in a Web-GL environment. It is possible to spawn up to 200 different avatars into the same scene to see how it affects the performance of the scene. 

This can also be embedded in your game or app, to see the performance of multiple avatars your game-scene.

Try it out on itch.
https://bernhardfercher.itch.io/ready-player-me-load-test

![2022-12-07 17 06 28](https://user-images.githubusercontent.com/42868289/206230405-377e1b66-75b5-4ec0-9926-1cf72c444063.gif)


## How to use the avatar loader

### Avatar Loader

1. On the left side-bar you can find the avatar loader
2. First you select the amount of avatars you want to spawn in the scene (1-200).
3. Second you choose a avatar-configuration (Low, Medium, High). When you select a config, you can see it's values in the box below. Currently it is not possible to change each individual parameter.
4. When you are ready, you can hit the "Load Avatars" button.
5. Avatar loading starts. You can see a white avatar silhuette in the scene. This marks the next avatar spawning, once it's loaded.

### Stats

On the right side bar you can find the stats window which should be pretty self explanatory.

### Control the camera

To check the quality or render-stats of single avatars, you can take control over the camera. Hit the "Space" key on the keyboard and then control the camera with the keys "W", "A", "S", "D" to move forward, backward and to the sides and use the mouse to look around. Hit "Space" again to exit the camera-control-mode.

### Turn on/off the light

To check the influence of the lighting on the performance, you can turn off the directional light. You can do so by hitting the light bulb icon in the bottom right corner.

