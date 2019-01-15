using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebService.Utilities
{
    public static class FileManager
    {
        public static void DeleteFiles(string application_id)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            List<Models.ApplicationFiles> files = db.GetApplicationFiles(application_id);

            for(int i=0; i<files.Count; i++)
            {
                try
                {
                    if (File.Exists(files[i].path))
                    {
                        File.Delete(files[i].path);
                        db.DeleteFileReference(files[i].file_id);
                    }
                }
                catch (Exception e)
                {
                    //do logging here...
                }
            }
        }
    }
}