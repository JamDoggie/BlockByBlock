This is Minecraft 1.2.5 in its entirety ported to C#. This project was literally just done for fun in a week or two, there are definitely some issues and the game may not be totally beatable.

This repo was never in a state where I wanted to release it, but it's been sitting idle for a while, so here you go.

# If you want a more stable experience, I would recommend checking out the legacy renderer branch.
The legacy renderer branch is more of a 1:1 port, whereas master has some experimental renderer changes from when I was working on this as a hobby project.

To build this, you will need to have both bidisharp and nvorbis as sibling folders to this repo, all built in the corresponding paths that the project looks for the binaries. You also need to download OpenAL Soft and include the windows x64 dll next to the exe and name it "openal32.dll"
