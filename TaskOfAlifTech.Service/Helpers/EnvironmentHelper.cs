using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOfAlifTech.Service.Helpers
{
    public class EnvironmentHelper
    {
        public static string WebRootPath { get; set; }
        public static string AttachmentPath => Path.Combine(WebRootPath, "files");
        public static string FilePath { get; set; }
    }
}
