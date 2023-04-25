using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlockLauncher.json
{
    public class LauncherData
    {
        public string lastDownloadedExtras = ""; // The last link used to download the extra assets. Ex. https://launchermeta.mojang.com/v1/packages/3d8e55480977e32acd9844e545177e69a52f594b/pre-1.6.json
        public string lastDownloadedJar = ""; // Same as above, but for the jar. The jar file for whatever version we're targetting usually contains a majority of the actual assets.
                                              // The extras that are downloaded from mojang's servers are mainly ogg files or other stuff that I assume mojang wants to be able to change
                                              // without a new version of Minecraft.
    }
}
