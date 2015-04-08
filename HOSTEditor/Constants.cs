using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOSTEditor
{
    public static class Constants
    {
        public static string Path { get { return @"C:\Windows\System32\drivers\etc\hosts"; } }
        public static string BackupPath { get { return @"C:\hosts\host_backup\host_"; } }
        public static string SaveTemplatePath { get { return @"C:\hosts\host_templates\"; } }
    }
}
