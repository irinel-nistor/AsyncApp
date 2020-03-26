using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApp
{
    class FileWriter
    {
        public async Task WriteToFileAsync(string file, string content)
        {
          using (StreamWriter writer = File.CreateText(file))
          {
              await Task.Delay(100).ConfigureAwait(false);
              await writer.WriteAsync(content);
          }
        }
    }
}
